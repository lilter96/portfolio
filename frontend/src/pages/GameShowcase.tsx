import { useState } from "react";
import { useTranslation } from "react-i18next";

const clips = [
  {
    id: "tgslots-reel",
    title: "TGSlots",
    description: "reelDescription",
    duration: "0:36",
    fullDuration: "0:36",
  },
  {
    id: "ancient-dragon",
    title: "Ancient Dragon",
    description: "dragon",
    duration: "0:21",
    fullDuration: "0:21",
  },
  {
    id: "woodland-whisper",
    title: "Woodland Whisper",
    description: "woodland",
    duration: "0:22",
    fullDuration: "0:33",
  },
  {
    id: "le-militare",
    title: "Le Militare",
    description: "militare",
    duration: "0:24",
    fullDuration: "0:59",
  },
] as const;
const media = `${import.meta.env.BASE_URL}media/slots/`;

export function GameShowcase() {
  const { t, i18n } = useTranslation();
  const [selected, setSelected] = useState(0);
  const [format, setFormat] = useState<"gameplay" | "promo">("promo");
  const [failed, setFailed] = useState(false);
  const clip = clips[selected] ?? clips[0];
  const isReel = selected === 0;
  const file = `${media}${clip.id}${isReel ? "" : `-${format}`}.mp4`;
  const title = isReel ? t("showreel.reel") : clip.title;
  const captionLanguage = i18n.resolvedLanguage?.startsWith("ru") ? "ru" : "en";

  return (
    <section id="showreel" className="showreel-section" aria-label={t("showreel.title")}>
      <p className="eyebrow">{t("showreel.eyebrow")}</p>
      <h2 className="section-title">{t("showreel.title")}</h2>
      <p className="section-lead">{t("showreel.intro")}</p>
      <div className="showreel-player">
        <video
          key={file}
          controls
          playsInline
          preload="none"
          poster={`${media}${clip.id}.webp`}
          aria-label={`${title} · ${isReel ? t("showreel.promo") : t(`showreel.${format}`)}`}
          onError={() => setFailed(true)}
        >
          <source src={file} type="video/mp4" />
          <track
            key={captionLanguage}
            kind="captions"
            src={`${media}${clip.id}${isReel ? "" : `-${format}`}-${captionLanguage}.vtt`}
            srcLang={captionLanguage}
            label={captionLanguage === "ru" ? "Русский" : "English"}
          />
          <a href={file}>{t("showreel.fallback")}</a>
        </video>
        <div className="showreel-detail">
          <div>
            <h3>{title}</h3>
            <p>{t(`showreel.${clip.description}`)}</p>
          </div>
          {!isReel && (
            <div className="showreel-formats" role="group" aria-label={t("showreel.format")}>
              {(["gameplay", "promo"] as const).map((value) => (
                <button
                  key={value}
                  type="button"
                  aria-pressed={format === value}
                  onClick={() => {
                    setFormat(value);
                    setFailed(false);
                  }}
                >
                  {t(`showreel.${value}`)}
                </button>
              ))}
            </div>
          )}
        </div>
        {failed && (
          <p className="showreel-error">
            <a href={file}>{t("showreel.failure")}</a>
          </p>
        )}
      </div>
      <div className="showreel-picker" role="group" aria-label={t("showreel.choose")}>
        {clips.map((item, index) => (
          <button
            type="button"
            key={item.id}
            className="showreel-choice"
            aria-label={index === 0 ? t("showreel.reel") : item.title}
            aria-pressed={selected === index}
            onClick={() => {
              setSelected(index);
              setFailed(false);
            }}
          >
            <div className="showreel-thumbnail">
              <img src={`${media}${item.id}.webp`} alt="" loading="lazy" width="480" height="270" />
              <span>{format === "gameplay" ? item.fullDuration : item.duration}</span>
            </div>
            <span className="showreel-choice-title">
              {index === 0 ? t("showreel.reel") : item.title}
            </span>
          </button>
        ))}
      </div>
      <div className="showreel-footer">
        <p>{t("showreel.note")}</p>
        <a href={`${media}tgslots-linkedin.mp4`} download>
          {t("showreel.linkedin")}
        </a>
        <a href="https://github.com/lilter96/tgslots" target="_blank" rel="noopener noreferrer">
          {t("showreel.source")} ↗
        </a>
      </div>
      <p className="showreel-credits">
        {t("showreel.music")}{" "}
        <a
          href="https://www.scottbuckley.com.au/library/catalyst/"
          target="_blank"
          rel="noopener noreferrer"
        >
          Catalyst
        </a>
        ,{" "}
        <a
          href="https://www.scottbuckley.com.au/library/helios/"
          target="_blank"
          rel="noopener noreferrer"
        >
          Helios
        </a>
        ,{" "}
        <a
          href="https://www.scottbuckley.com.au/library/titan/"
          target="_blank"
          rel="noopener noreferrer"
        >
          Titan
        </a>{" "}
        — Scott Buckley ·{" "}
        <a
          href="https://creativecommons.org/licenses/by/4.0/"
          target="_blank"
          rel="noopener noreferrer"
        >
          CC BY 4.0
        </a>
        . {t("showreel.musicEdits")}
      </p>
    </section>
  );
}
