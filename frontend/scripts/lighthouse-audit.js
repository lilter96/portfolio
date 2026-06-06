/**
 * Run Lighthouse audit against the built site using Playwright's Chromium.
 */
import lighthouse from "lighthouse";
import { chromium } from "@playwright/test";

const PORT = 3000;
const URL = `http://localhost:${PORT}`;

async function main() {
  // Launch Playwright's Chromium with remote debugging port
  const browser = await chromium.launch({
    args: [`--remote-debugging-port=9222`],
    headless: true,
  });

  // Navigate to warm the browser
  const page = await browser.newPage();
  await page.goto(URL, { waitUntil: "networkidle" });
  await page.close();

  // Run Lighthouse
  const result = await lighthouse(URL, {
    port: 9222,
    output: ["html", "json"],
    onlyCategories: ["performance", "accessibility", "best-practices", "seo"],
    chromeFlags: ["--headless", "--no-sandbox"],
  });

  await browser.close();

  if (!result) {
    console.error("Lighthouse failed to produce a result.");
    process.exit(1);
  }

  // Save reports
  const fs = await import("node:fs");
  fs.writeFileSync("./lighthouse-report.html", /** @type {string} */ (result.report[0]));
  fs.writeFileSync("./lighthouse-report.json", /** @type {string} */ (result.report[1]));

  // Print scores
  const scores = result.lhr.categories;
  console.log("\n── Lighthouse Scores ──");
  let allAbove95 = true;
  for (const [, cat] of Object.entries(scores)) {
    const pct = Math.round((cat.score ?? 0) * 100);
    const icon = pct >= 95 ? "✅" : pct >= 90 ? "⚠️" : "❌";
    console.log(`${icon} ${cat.title}: ${pct}`);
    if (pct < 95) allAbove95 = false;
  }

  console.log(allAbove95 ? "\n🏆 All scores ≥ 95!" : "\n⚠️  Some scores below 95 — see lighthouse-report.html");

  // Print actionable diagnostics
  const audits = result.lhr.audits;
  const diagnostics = [
    "render-blocking-resources",
    "unused-css-rules",
    "unused-javascript",
    "offscreen-images",
    "total-byte-weight",
    "dom-size",
    "font-display",
    "uses-text-compression",
    "modern-image-formats",
    "uses-responsive-images",
    "image-aspect-ratio",
  ];

  console.log("\n── Diagnostics ──");
  for (const diag of diagnostics) {
    const audit = audits[diag];
    if (audit && audit.score !== null && audit.score < 0.9) {
      console.log(`⚠️  ${audit.title}: ${audit.displayValue ?? "needs attention"}`);
    }
  }
}

main().catch((err) => {
  console.error(err);
  process.exit(1);
});
