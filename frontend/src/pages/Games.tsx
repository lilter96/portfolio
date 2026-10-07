import { useTranslation } from "react-i18next";
import { selectedGames } from "@/lib/resume-content";

export function Games() {
  const { t } = useTranslation();
  return (
    <section id="games" className="games-section" aria-label={t("games.title")}>
      <p className="eyebrow">myKONAMI / CUSTOM GAMES STUDIO</p>
      <h2 className="section-title">{t("games.title")}</h2>
      <p className="section-lead">{t("games.intro")}</p>
      <div className="games-grid">
        {selectedGames.map((game, i) => <a className="game-card" href={game.url} key={game.title} target="_blank" rel="noopener noreferrer">
          <span className="card-number">{String(i + 1).padStart(2, "0")} / GAME BACKEND</span>
          <h3>{game.title}</h3><span className="game-link">{t("games.video")} ↗</span>
        </a>)}
      </div>
      <p className="mechanics-note">{t("games.mechanics")}</p>
    </section>
  );
}
