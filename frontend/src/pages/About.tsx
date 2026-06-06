import { useTranslation } from "react-i18next";

export function About() {
  const { t } = useTranslation();

  return (
    <section id="about" className="about-section" aria-label={t("about.title")}>
      <h2 className="about-title reveal">{t("about.title")}</h2>

      <div className="about-grid">
        {/* Intro — the intersection */}
        <div className="about-card reveal reveal-2">
          <p className="about-card-icon" aria-hidden="true">
            ⚡
          </p>
          <p className="about-card-text">{t("about.intro")}</p>
        </div>

        {/* Custom Games Studio + TGSlots live link */}
        <div className="about-card about-card-highlight reveal reveal-3">
          <p className="about-card-icon" aria-hidden="true">
            🎰
          </p>
          <p className="about-card-text">{t("about.customGames")}</p>
          <p className="about-card-text about-card-tgslots">{t("about.tgslotsLive")}</p>
          <a
            className="about-tgslots-cta"
            href="https://tgslots-marketing-production.up.railway.app/"
            target="_blank"
            rel="noopener noreferrer"
          >
            {t("about.tgslotsCta")}
          </a>
        </div>

        {/* Fullstack + tech tags */}
        <div className="about-card reveal reveal-4">
          <p className="about-card-icon" aria-hidden="true">
            ⚙️
          </p>
          <p className="about-card-text">{t("about.fullstack")}</p>
          <div className="about-tech-tags">
            <span>.NET</span>
            <span>ASP.NET Core</span>
            <span>SignalR</span>
            <span>React</span>
            <span>PostgreSQL</span>
            <span>Redis</span>
            <span>F#</span>
            <span>TON</span>
          </div>
        </div>

        {/* Philosophy */}
        <div className="about-card reveal reveal-5">
          <p className="about-card-icon" aria-hidden="true">
            ✦
          </p>
          <p className="about-card-text about-card-quote">{t("about.philosophy")}</p>
        </div>
      </div>
    </section>
  );
}
