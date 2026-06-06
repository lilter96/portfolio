import { useTranslation } from "react-i18next";

export function LanguageSwitcher() {
  const { i18n } = useTranslation();

  return (
    <button
      className="lang-switcher"
      onClick={() => i18n.changeLanguage(i18n.language === "en" ? "ru" : "en")}
      type="button"
      aria-label={`${i18n.language === "en" ? "RU" : "EN"} — Switch to ${i18n.language === "en" ? "Russian" : "English"}`}
    >
      {i18n.language === "en" ? "RU" : "EN"}
    </button>
  );
}
