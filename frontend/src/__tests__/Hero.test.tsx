import { describe, it, expect, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { Hero } from "@/pages/Hero";

// ── Mock react-i18next ──────────────────────────────────────
vi.mock("react-i18next", () => ({
  useTranslation: () => ({
    t: (key: string) => {
      const en: Record<string, string> = {
        "hero.greeting": "Hello, I'm",
        "hero.name": "Terentiy Gatsukov",
        "hero.role": "Senior .NET Backend Developer",
        "hero.tagline": "I build slot engines, real-money pipelines, and crypto trading systems.",
        "hero.cta": "Explore my work",
        "hero.contact": "Get in touch",
        "hero.downloadCv": "Download CV",
      };
      return en[key] ?? key;
    },
    i18n: { language: "en", changeLanguage: vi.fn() },
  }),
}));

describe("Hero", () => {
  it("renders the greeting text", () => {
    render(<Hero />);
    expect(screen.getByText("Hello, I'm")).toBeInTheDocument();
  });

  it("renders the full name", () => {
    render(<Hero />);
    expect(screen.getByText("Terentiy")).toBeInTheDocument();
    expect(screen.getByText("Gatsukov")).toBeInTheDocument();
  });

  it("renders the role", () => {
    render(<Hero />);
    expect(screen.getByText("Senior .NET Backend Developer")).toBeInTheDocument();
  });

  it("renders the tagline with slot emoji", () => {
    render(<Hero />);
    expect(
      screen.getByText(/I build slot engines, real-money pipelines/i),
    ).toBeInTheDocument();
  });

  it("renders CTA button", () => {
    render(<Hero />);
    expect(screen.getByRole("button", { name: "Explore my work" })).toBeInTheDocument();
  });

  it("renders contact link", () => {
    render(<Hero />);
    const contactLink = screen.getByText("Get in touch");
    expect(contactLink.closest("a")).toHaveAttribute(
      "href",
      "mailto:lilter96dotnet@gmail.com",
    );
  });

  it("renders CV download link for English locale", () => {
    render(<Hero />);
    const cvLink = screen.getByText("Download CV");
    expect(cvLink.closest("a")).toHaveAttribute(
      "href",
      "/cv/terentiy-gatsukov-slot-games-en.pdf",
    );
    expect(cvLink.closest("a")).toHaveAttribute(
      "download",
      "terentiy-gatsukov-slot-games-en.pdf",
    );
  });

  it("has decorative background elements hidden from accessibility tree", () => {
    render(<Hero />);
    const bg = document.querySelector(".hero-bg");
    expect(bg).toHaveAttribute("aria-hidden", "true");
  });

  it("section has aria-label matching greeting", () => {
    render(<Hero />);
    expect(
      document.querySelector("section[aria-label=\"Hello, I'm\"]"),
    ).toBeInTheDocument();
  });
});
