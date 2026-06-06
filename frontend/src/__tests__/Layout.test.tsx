import { describe, it, expect, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { Layout } from "@/components/Layout";

// ── Mock react-i18next ──────────────────────────────────────
vi.mock("react-i18next", () => ({
  useTranslation: () => ({
    t: (key: string) => {
      const en: Record<string, string> = {
        "a11y.skipToContent": "Skip to content",
        "footer.name": "Terentiy Gatsukov",
        "footer.role": "Senior .NET Backend Developer",
        "footer.specialty": "iGaming × .NET × Real-Time",
        "footer.builtWith": "Built with love and code.",
      };
      return en[key] ?? key;
    },
    i18n: { language: "en", changeLanguage: vi.fn() },
  }),
}));

describe("Layout", () => {
  it("renders a skip-to-content link", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    expect(screen.getByText("Skip to content")).toBeInTheDocument();
    expect(screen.getByText("Skip to content").closest("a")).toHaveAttribute(
      "href",
      "#main-content",
    );
  });

  it("renders the brand in navigation", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    const brand = screen.getByLabelText("TG — Home");
    expect(brand).toBeInTheDocument();
    expect(brand).toHaveAttribute("href", "/");
  });

  it("renders the language switcher", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    expect(screen.getByRole("button", { name: /RU — Switch to Russian/i })).toBeInTheDocument();
  });

  it("renders children in main content area", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    const main = document.getElementById("main-content");
    expect(main).toBeInTheDocument();
    expect(main).toContainElement(screen.getByText("Page content"));
  });

  it("main has negative tabindex for focus management", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    const main = document.getElementById("main-content");
    expect(main).toHaveAttribute("tabindex", "-1");
  });

  it("renders footer with name and role", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    expect(screen.getByText(/Terentiy Gatsukov/)).toBeInTheDocument();
    expect(screen.getByText(/Senior .NET Backend Developer/)).toBeInTheDocument();
  });

  it("nav has aria-label for main navigation", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    expect(screen.getByRole("navigation")).toHaveAttribute(
      "aria-label",
      "Main navigation",
    );
  });

  it("footer has contentinfo role", () => {
    render(
      <Layout>
        <p>Page content</p>
      </Layout>,
    );
    expect(screen.getByRole("contentinfo")).toBeInTheDocument();
  });
});
