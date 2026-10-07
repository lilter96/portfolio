import { useTranslation } from "react-i18next";
import { useCallback } from "react";
import { contactDetails } from "@/lib/contact-details";

export function Hero() {
  const { t } = useTranslation();
  const scrollToWork = useCallback(() => {
    document.getElementById("experience")?.scrollIntoView({ behavior: "smooth" });
  }, []);
  const base = import.meta.env.BASE_URL;

  return (
    <section className="hero-section" aria-label={t("hero.greeting")}>
      <div className="hero-bg" aria-hidden="true"><div className="hero-grid-overlay" /></div>
      <div className="hero-content">
        <p className="hero-greeting reveal reveal-1">{t("hero.greeting")}</p>
        <h1 className="hero-name reveal reveal-2">
          <span className="hero-name-line">{t("hero.name").split(" ")[0]}</span>
          <span className="hero-name-line hero-name-last">{t("hero.name").split(" ").slice(1).join(" ")}</span>
        </h1>
        <p className="hero-role reveal reveal-3">{t("hero.role")}</p>
        <p className="hero-tagline reveal reveal-4">{t("hero.tagline")}</p>
        <p className="hero-location">{t("hero.location")}</p>
        <div className="hero-actions reveal reveal-5">
          <button className="hero-cta hero-cta-primary" onClick={scrollToWork} type="button">{t("hero.cta")} <span aria-hidden="true">↓</span></button>
          <a className="hero-cta hero-cta-secondary" href={`mailto:${contactDetails.email}`}>{t("hero.contact")}</a>
        </div>
        <div className="resume-links">
          <a href={`${base}cv/terentiy-gatsukov-slot-games-en.pdf`} download="terentiy-gatsukov-slot-games-en.pdf">{t("hero.downloadCv")} <span aria-hidden="true">↗</span></a>
          <a href={`${base}cv/terentiy-gatsukov-dotnet-ru.pdf`} download="terentiy-gatsukov-dotnet-ru.pdf">{t("hero.backendCv")} <span aria-hidden="true">↗</span></a>
        </div>
      </div>
      <aside className="hero-proof reveal reveal-4" aria-label={t("hero.metricsTitle")}>
        <p className="eyebrow">{t("hero.metricsTitle")}</p>
        <div className="proof-grid">
          <div><strong>5k+</strong><span>{t("hero.rps")}</span></div>
          <div><strong>6×</strong><span>{t("hero.speedup")}</span></div>
          <div><strong>~15</strong><span>{t("hero.games")}</span></div>
          <div><strong>20+</strong><span>{t("hero.library")}</span></div>
        </div>
        <p className="hero-proof-note">{t("hero.metricsContext")}</p>
        <div className="hero-proof-footer"><span>C# / F#</span><span>Distributed systems</span><span>Game math</span></div>
      </aside>
    </section>
  );
}
