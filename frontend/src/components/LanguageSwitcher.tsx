import { useTranslation } from "react-i18next";

export function LanguageSwitcher() {
  const { i18n } = useTranslation();

  const isRussian = i18n.language.startsWith("ru");

  return (
    <button
      className="lang-switcher"
      onClick={() => i18n.changeLanguage(isRussian ? "en" : "ru")}
      type="button"
      aria-label={`${isRussian ? "EN" : "RU"} — Switch to ${isRussian ? "English" : "Russian"}`}
    >
      {isRussian ? "EN" : "RU"}
    </button>
  );
}
