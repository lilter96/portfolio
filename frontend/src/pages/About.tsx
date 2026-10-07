import { useTranslation } from "react-i18next";

export function About() {
  const { t } = useTranslation();
  return (
    <section id="about" className="about-section" aria-label={t("about.title")}>
      <p className="eyebrow">{t("about.eyebrow")}</p>
      <h2 className="about-title">{t("about.title")}</h2>
      <p className="section-lead">{t("about.intro")}</p>
      <div className="about-grid">
        <article className="about-card"><span className="card-number">01 / BACKEND</span><h3>{t("about.backendTitle")}</h3><p>{t("about.fullstack")}</p></article>
        <article className="about-card"><span className="card-number">02 / GAME ENGINEERING</span><h3>{t("about.gameTitle")}</h3><p>{t("about.customGames")}</p></article>
        <article className="about-card"><span className="card-number">03 / ENGINEERING PRACTICE</span><h3>{t("about.practiceTitle")}</h3><p>{t("about.philosophy")}</p></article>
      </div>
    </section>
  );
}
