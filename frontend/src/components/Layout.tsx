import type { ReactNode } from "react";
import { useTranslation } from "react-i18next";
import { LanguageSwitcher } from "@/components/LanguageSwitcher";

interface LayoutProps {
  children: ReactNode;
  theme?: "dark" | "light";
  onToggleTheme?: () => void;
}

export function Layout({ children, theme, onToggleTheme }: LayoutProps) {
  const { t } = useTranslation();
  const homeUrl = import.meta.env.BASE_URL;

  return (
    <div className="layout">
      {/* ── Skip link ────────────────────────────────────── */}
      <a href="#main-content" className="skip-link">
        {t("a11y.skipToContent")}
      </a>

      {/* ── Nav ────────────────────────────────────────── */}
      <nav className="top-nav" role="navigation" aria-label="Main navigation">
        <div className="nav-inner">
          <a href={homeUrl} className="nav-brand mono" aria-label="TG — Home">
            TG<span className="nav-brand-dot">.</span>
          </a>
          <div className="nav-links"><a href="#experience">{t("nav.experience")}</a><a href="#showreel">{t("nav.games")}</a><a href="#projects">{t("nav.projects")}</a><a href="#contact">{t("nav.contact")}</a></div>
          <div className="nav-controls">
            {onToggleTheme && <button type="button" className="theme-toggle" onClick={onToggleTheme} aria-label={theme === "dark" ? "Switch to light theme" : "Switch to dark theme"}>{theme === "dark" ? "☀" : "☾"}</button>}
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
