import { useTranslation } from "react-i18next";

export function Education() {
  const { t } = useTranslation();
  return (
    <section id="education" className="education-section" aria-label={t("education.title")}>
      <h2 className="section-title">{t("education.title")}</h2>
      <div className="education-grid">
        <article><p className="eyebrow">BSUIR / 2023</p><h3>{t("education.degree")}</h3><p>{t("education.university")}</p><p>{t("education.specialty")}</p></article>
        <article><p className="eyebrow">{t("education.languagesTitle")}</p><h3>{t("education.languages")}</h3><p>{t("education.location")}</p><p>{t("education.authorization")}</p></article>
      </div>
    </section>
  );
}
