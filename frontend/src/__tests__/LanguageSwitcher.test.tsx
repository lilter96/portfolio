import { describe, it, expect, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { LanguageSwitcher } from "@/components/LanguageSwitcher";

// ── Mock react-i18next ──────────────────────────────────────
const changeLanguage = vi.fn();

vi.mock("react-i18next", () => ({
  useTranslation: () => ({
    t: (key: string) => key,
    i18n: {
      language: "en",
      changeLanguage,
    },
  }),
}));

describe("LanguageSwitcher", () => {
  beforeEach(() => {
    changeLanguage.mockClear();
  });

  it("renders RU button when language is English", () => {
    render(<LanguageSwitcher />);
    expect(screen.getByRole("button", { name: /RU — Switch to Russian/i })).toBeInTheDocument();
    expect(screen.getByText("RU")).toBeInTheDocument();
  });

  it("calls changeLanguage with 'ru' when clicked in English mode", async () => {
    const user = userEvent.setup();
    render(<LanguageSwitcher />);

    await user.click(screen.getByRole("button", { name: /RU — Switch to Russian/i }));
    expect(changeLanguage).toHaveBeenCalledWith("ru");
  });
});
