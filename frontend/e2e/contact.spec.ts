import { test, expect } from "@playwright/test";

test.describe("Contact form", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto("/");
    // Scroll to contact section
    await page.locator("#contact").scrollIntoViewIfNeeded();
    await page.waitForTimeout(500);
  });

  test("renders all form fields", async ({ page }) => {
    await expect(page.locator("#contact-name")).toBeVisible();
    await expect(page.locator("#contact-email")).toBeVisible();
    await expect(page.locator("#contact-message")).toBeVisible();
    await expect(page.locator(".contact-submit")).toBeVisible();
  });

  test("shows validation errors when submitting empty form", async ({ page }) => {
    await page.locator(".contact-submit").click();

    await expect(page.locator("text=Name is required.")).toBeVisible();
    await expect(page.locator("text=Email is required.")).toBeVisible();
    await expect(page.locator("text=Message is required.")).toBeVisible();
  });

  test("shows email format error for invalid email", async ({ page }) => {
    await page.locator("#contact-name").fill("Test User");
    await page.locator("#contact-email").fill("not-an-email");
    await page.locator("#contact-message").fill("This is a valid test message.");

    await page.locator(".contact-submit").click();

    await expect(page.locator("text=A valid email address is required.")).toBeVisible();
  });

  test("shows message too short error", async ({ page }) => {
    await page.locator("#contact-name").fill("Test User");
    await page.locator("#contact-email").fill("test@example.com");
    await page.locator("#contact-message").fill("Short");

    await page.locator(".contact-submit").click();

    await expect(
      page.locator("text=Message must be at least 10 characters."),
    ).toBeVisible();
  });

  test("clears validation error when user types in a touched field", async ({ page }) => {
    // Submit empty first
    await page.locator(".contact-submit").click();
    await expect(page.locator("text=Name is required.")).toBeVisible();

    // Type in the name field — error should clear
    await page.locator("#contact-name").fill("John Doe");
    await expect(page.locator("text=Name is required.")).not.toBeVisible();
  });

  test("character count updates as user types", async ({ page }) => {
    await page.locator("#contact-message").fill("Hello World");
    await expect(page.locator("#contact-message-charcount")).toHaveText("11/5000");
  });

  test("honeypot field is hidden from user interaction", async ({ page }) => {
    // Honeypot uses sr-only class + aria-hidden parent + tabIndex=-1
    const honeypot = page.locator("#website");
    await expect(honeypot).toHaveAttribute("tabindex", "-1");

    // The parent wrapper should have aria-hidden
    const honeypotWrapper = page.locator(".sr-only[aria-hidden='true']");
    await expect(honeypotWrapper.first()).toBeAttached();
  });

  test("submit button is not disabled initially", async ({ page }) => {
    const btn = page.locator(".contact-submit");
    await expect(btn).toBeEnabled();
  });

  test("has contact links in the info section", async ({ page }) => {
    // Scope to the contact info section to avoid duplicates (Hero also has a mailto link)
    const contactSection = page.locator("#contact");

    // Email link inside contact section
    await expect(
      contactSection.locator("a[href='mailto:lilter96dotnet@gmail.com']"),
    ).toBeVisible();

    // GitHub link
    await expect(
      contactSection.locator("a[href='https://github.com/lilter96']"),
    ).toBeVisible();

    // LinkedIn link
    await expect(
      contactSection.locator(
        "a[href='https://www.linkedin.com/in/terentiy-gatsukov/']",
      ),
    ).toBeVisible();
  });

  test("form fields have correct autocomplete attributes", async ({ page }) => {
    await expect(page.locator("#contact-name")).toHaveAttribute(
      "autocomplete",
      "name",
    );
    await expect(page.locator("#contact-email")).toHaveAttribute(
      "autocomplete",
      "email",
    );
  });

  test("message field has character limit info", async ({ page }) => {
    await expect(page.locator("#contact-message")).toHaveAttribute(
      "maxlength",
      "5000",
    );
  });
});
