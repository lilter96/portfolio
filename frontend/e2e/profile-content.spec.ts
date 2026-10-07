import { test, expect } from "@playwright/test";
import { showcaseExperience, showcaseProjects, showcaseSkills } from "../src/lib/showcase-content";

for (const language of ["en", "ru"]) {
  test(`current CV contacts and employment dates survive in ${language}`, async ({ page }) => {
    await page.addInitScript(lang => localStorage.setItem("i18nextLng", lang), language);
    for (const [path, data] of [
      ["experience", showcaseExperience], ["projects", showcaseProjects],
      ["skills", showcaseSkills], ["github/repos", []],
    ] as const) {
      await page.route(`**/api/v1/${path}`, route => route.fulfill({ json: data }));
    }
    await page.goto("./");
    const contact = page.locator("#contact");
    await expect(contact.locator('a[href="mailto:lilter96dotnet@gmail.com"]')).toBeVisible();
    await expect(contact.locator('a[href="https://www.linkedin.com/in/terentiy-gatsukov/"]')).toBeVisible();
    await expect(contact.locator('a[href="https://t.me/lilter96"]')).toBeVisible();
    await expect(contact.locator('a[href="tel:+375333854432"]')).toBeVisible();
    await expect(page.locator(".exp-company")).toHaveText(["Custom Games Studio", "Solvintech", "Elgrow"]);
    const dates = await page.locator("#experience time").evaluateAll(nodes => nodes.map(n => n.getAttribute("datetime")));
    expect(dates).toEqual(["2023-11", "2026-07", "2022-07", "2023-10", "2021-01", "2022-06"]);
    await expect(page.locator("body")).not.toContainText("terentiy.gatsukov@gmail.com");
    await expect(page.locator("body")).not.toContainText("Softeq");
    await expect(page.locator("body")).not.toContainText("Syberry");
    for (const name of ["terentiy-gatsukov-slot-games-en.pdf", "terentiy-gatsukov-dotnet-ru.pdf"]) {
      const link = page.locator(`a[download="${name}"]`);
      const href = await link.getAttribute("href");
      expect(href).toBeTruthy();
      const response = await page.request.get(new URL(href!, page.url()).href);
      expect(response.status()).toBe(200);
      expect((await response.body()).subarray(0, 5).toString()).toBe("%PDF-");
    }
    await page.setViewportSize({ width: 390, height: 844 });
    expect(await page.evaluate(() => document.documentElement.scrollWidth <= window.innerWidth)).toBe(true);
  });
}
