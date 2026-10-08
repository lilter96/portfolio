import { test, expect } from "@playwright/test";

test("game showcase waits for playback intent and switches real clips", async ({ page }) => {
  const requests: string[] = [];
  page.on("request", (request) => {
    if (request.url().endsWith(".mp4")) requests.push(request.url());
  });
  await page.goto("./");
  const section = page.locator("#showreel");
  await section.scrollIntoViewIfNeeded();
  const player = section.locator("video");
  await expect(player).toHaveAttribute("preload", "none");
  await expect(player).toHaveAttribute("controls", "");
  await expect(player).toHaveAttribute("playsinline", "");
  expect(await player.getAttribute("autoplay")).toBeNull();
  expect(requests).toHaveLength(0);
  for (const game of [
    "Nine Lives",
    "X7 Club",
    "Le Militare",
    "Woodland Whisper",
    "Ancient Dragon",
  ]) {
    await section.getByRole("button", { name: game, exact: true }).click();
    await expect(section.getByRole("heading", { name: game })).toBeVisible();
    await player.evaluate(async (video: HTMLVideoElement) => {
      video.muted = true;
      await video.play();
    });
    await expect
      .poll(() => player.evaluate((video: HTMLVideoElement) => video.currentTime))
      .toBeGreaterThan(0.2);
    await expect
      .poll(() => player.evaluate((video: HTMLVideoElement) => video.videoWidth))
      .toBe(game === "Nine Lives" ? 2560 : 1280);
    if (game === "Nine Lives") {
      await expect(section.locator("source")).toHaveAttribute(
        "src",
        /nine-lives-promo-clean\.mp4$/,
      );
      await expect(section.locator("track")).toHaveAttribute(
        "src",
        /nine-lives-promo-clean-en\.vtt$/,
      );
      await expect(section.getByRole("button", { name: "Full capture", exact: true })).toHaveCount(
        0,
      );
      continue;
    }
    await section.getByRole("button", { name: "Promo edit", exact: true }).click();
    await expect(section.locator("source")).toHaveAttribute("src", /-promo(?:-clean)?\.mp4$/);
    await section.getByRole("button", { name: "Full capture", exact: true }).click();
  }
  await section.getByRole("button", { name: "TGSlots · Gameplay reel", exact: true }).click();
  await expect(section.locator("source")).toHaveAttribute("src", /tgslots-reel\.mp4$/);
  expect(await section.locator("video").count()).toBe(1);
  await page.setViewportSize({ width: 390, height: 844 });
  await expect(section.getByRole("button", { name: "Le Militare", exact: true })).toBeVisible();
  expect(
    await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth),
  ).toBeTruthy();
});
