import { test, expect } from "@playwright/test";

// Mock project data so the TGSlots hero renders
const MOCK_PROJECTS = [
  {
    id: "tgslots-1",
    title: "TGSlots — Casino Game Aggregator",
    description:
      "A Telegram Mini App casino aggregator with 40+ slot games, real-money pipelines, and provably fair verification.",
    url: "https://tgslots-marketing-production.up.railway.app/",
    sourceUrl: null,
    technologies: [".NET", "ASP.NET Core", "SignalR", "TON", "PostgreSQL", "Redis"],
    sortOrder: 1,
    status: "Live",
    role: "Senior Backend Developer",
    evidenceUrl: "https://tgslots-marketing-production.up.railway.app/",
    domain: "IGaming",
  },
];

const MOCK_REPOS: never[] = [];

test.describe("TGSlots embed", () => {
  test.beforeEach(async ({ page }) => {
    // Intercept API calls and return mock data
    await page.route("**/api/v1/projects", (route) =>
      route.fulfill({
        status: 200,
        contentType: "application/json",
        body: JSON.stringify(MOCK_PROJECTS),
      }),
    );
    await page.route("**/api/v1/github/repos", (route) =>
      route.fulfill({
        status: 200,
        contentType: "application/json",
        body: JSON.stringify(MOCK_REPOS),
      }),
    );

    await page.goto("/");
    // Wait for the TGSlots hero to appear
    await page.waitForSelector(".tgslots-hero", { timeout: 10_000 });
  });

  test("renders TGSlots hero section with title", async ({ page }) => {
    await expect(page.locator(".tgslots-hero-title")).toBeVisible();
    expect(await page.locator(".tgslots-hero-title").textContent()).toBeTruthy();
  });

  test("renders TGSlots iframe", async ({ page }) => {
    const iframe = page.locator(".tgslots-iframe");
    await expect(iframe).toBeVisible();
  });

  test("iframe has correct sandbox attributes", async ({ page }) => {
    const iframe = page.locator(".tgslots-iframe");
    await expect(iframe).toHaveAttribute(
      "sandbox",
      "allow-scripts allow-same-origin allow-forms",
    );
  });

  test("iframe loads with lazy loading", async ({ page }) => {
    const iframe = page.locator(".tgslots-iframe");
    await expect(iframe).toHaveAttribute("loading", "lazy");
  });

  test("iframe src points to TGSlots marketing URL", async ({ page }) => {
    const iframe = page.locator(".tgslots-iframe");
    await expect(iframe).toHaveAttribute(
      "src",
      "https://tgslots-marketing-production.up.railway.app/",
    );
  });

  test("TGSlots hero has live play link", async ({ page }) => {
    const playLink = page.locator(".tgslots-play-link");
    await expect(playLink).toBeVisible();
    await expect(playLink).toHaveAttribute(
      "href",
      "https://tgslots-marketing-production.up.railway.app/",
    );
    await expect(playLink).toHaveAttribute("target", "_blank");
  });

  test("TGSlots hero has status badge", async ({ page }) => {
    const badge = page.locator(".tgslots-hero-header .status-badge");
    await expect(badge).toBeVisible();
  });

  test("TGSlots hero has description text", async ({ page }) => {
    const desc = page.locator(".tgslots-hero-desc");
    await expect(desc).toBeVisible();
    expect(await desc.textContent()).toBeTruthy();
  });

  test("TGSlots fallback note is visible", async ({ page }) => {
    // The fallback note is always shown below the embed
    await expect(page.locator(".tgslots-fallback-note")).toBeVisible();
  });
});
