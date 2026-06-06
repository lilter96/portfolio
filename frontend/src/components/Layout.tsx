import type { ReactNode } from "react";
import { useTranslation } from "react-i18next";
import { LanguageSwitcher } from "@/components/LanguageSwitcher";

interface LayoutProps {
  children: ReactNode;
}

export function Layout({ children }: LayoutProps) {
  const { t } = useTranslation();

  return (
    <div className="layout">
      {/* ── Skip link ────────────────────────────────────── */}
      <a href="#main-content" className="skip-link">
        {t("a11y.skipToContent")}
      </a>

      {/* ── Nav ────────────────────────────────────────── */}
      <nav className="top-nav" role="navigation" aria-label="Main navigation">
        <div className="nav-inner">
          <a href="/" className="nav-brand mono" aria-label="TG — Home">
            TG<span className="nav-brand-dot">.</span>
          </a>
          <div className="nav-controls">
            <LanguageSwitcher />
          </div>
        </div>
      </nav>

      {/* ── Main ───────────────────────────────────────── */}
      <main id="main-content" className="main-content" tabIndex={-1}>
        {children}
      </main>

      {/* ── Footer ─────────────────────────────────────── */}
      <footer className="site-footer" role="contentinfo">
        <div className="footer-inner">
          <p className="mono" style={{ fontSize: "var(--text-sm)", color: "var(--color-text-muted)" }}>
            {t("footer.name")} &nbsp;·&nbsp; {t("footer.role")} &nbsp;·&nbsp; {t("footer.specialty")}
          </p>
          <p style={{ fontSize: "var(--text-xs)", color: "var(--color-text-muted)", marginTop: "var(--space-2)" }}>
            {t("footer.builtWith")}
          </p>
        </div>
      </footer>
    </div>
  );
}
