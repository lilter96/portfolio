import { useTranslation } from "react-i18next";
import { useCallback } from "react";

const CV_PDFS = {
  en: { url: "/cv/terentiy-gatsukov-cv-en.pdf", name: "terentiy-gatsukov-cv-en.pdf" },
  ru: { url: "/cv/terentiy-gatsukov-cv-ru.pdf", name: "terentiy-gatsukov-cv-ru.pdf" },
} as const;

export function Hero() {
  const { t, i18n } = useTranslation();
  const cv = CV_PDFS[i18n.language as keyof typeof CV_PDFS] ?? CV_PDFS.en;

  const scrollToAbout = useCallback(() => {
    document.getElementById("about")?.scrollIntoView({ behavior: "smooth" });
  }, []);

  return (
    <section className="hero-section" aria-label={t("hero.greeting")}>
      <div className="hero-bg" aria-hidden="true">
        <div className="hero-circuit hero-circuit-1" />
        <div className="hero-circuit hero-circuit-2" />
        <div className="hero-grid-overlay" />
      </div>

      <div className="hero-content">
        <p className="hero-greeting reveal reveal-1">
          <span className="hero-prompt mono">{">"}</span> {t("hero.greeting")}
        </p>

        <h1 className="hero-name reveal reveal-2">
          <span className="hero-name-line">{t("hero.name").split(" ")[0]}</span>
          <span className="hero-name-line hero-name-last">{t("hero.name").split(" ").slice(1).join(" ")}</span>
        </h1>

        <p className="hero-role reveal reveal-3">{t("hero.role")}</p>

        <p className="hero-tagline reveal reveal-4">
          <span className="hero-tagline-icon neon" aria-hidden="true">
            🎰
          </span>{" "}
          {t("hero.tagline")}
        </p>

        <div className="hero-actions reveal reveal-5">
          <button className="hero-cta hero-cta-primary" onClick={scrollToAbout} type="button">
            {t("hero.cta")}
            <span className="hero-cta-arrow" aria-hidden="true">
              ↓
            </span>
          </button>
          <a className="hero-cta hero-cta-secondary" href="mailto:terentiy.gatsukov@gmail.com">
            {t("hero.contact")}
          </a>
          <a
            className="hero-cta hero-cta-cv"
            href={`${import.meta.env.BASE_URL}${cv.url.slice(1)}`}
            download={cv.name}
            rel="noopener noreferrer"
          >
            {t("hero.downloadCv")}
            <span className="hero-cta-cv-icon" aria-hidden="true">
              📄
            </span>
          </a>
        </div>
      </div>
    </section>
  );
}
