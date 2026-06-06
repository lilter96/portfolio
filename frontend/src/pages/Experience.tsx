import { useTranslation } from "react-i18next";
import { useExperience } from "@/hooks/useExperience";
import { QueryState } from "@/components/QueryState";
import type { ExperienceDto } from "@/types/api";

export function Experience() {
  const { t } = useTranslation();
  const query = useExperience();

  return (
    <section id="experience" className="exp-section" aria-label={t("nav.experience")}>
      <h2 className="section-title reveal">{t("nav.experience")}</h2>

      <QueryState data={query.data} isLoading={query.isLoading} error={query.error}>
        {(data) => <ExperienceTimeline items={data} />}
      </QueryState>
    </section>
  );
}

function ExperienceTimeline({ items }: { items: ExperienceDto[] }) {
  const { t } = useTranslation();
  return (
    <div className="exp-timeline" role="list">
      {items.map((exp, i) => {
        const start = new Date(exp.startDate);
        const end = exp.endDate ? new Date(exp.endDate) : null;
        const isCurrent = !exp.endDate;

        return (
          <div
            key={exp.id}
            className={`exp-item reveal reveal-${((i % 6) + 1) as 1 | 2 | 3 | 4 | 5 | 6}`}
            role="listitem"
          >
            <div className="exp-marker" aria-hidden="true">
              <div className={`exp-dot ${isCurrent ? "exp-dot-current" : ""}`} />
              <div className="exp-line" />
            </div>

            <div className="exp-card">
              <div className="exp-period mono">
                <time dateTime={start.toISOString().slice(0, 7)}>
                  {start.toLocaleDateString("en-US", { month: "short", year: "numeric" })}
                </time>
                <span className="exp-period-sep">—</span>
                {isCurrent ? (
                  <span className="exp-current-label">{t("common.present")}</span>
                ) : (
                  <time dateTime={end!.toISOString().slice(0, 7)}>
                    {end!.toLocaleDateString("en-US", { month: "short", year: "numeric" })}
                  </time>
                )}
              </div>

              <h3 className="exp-role">{exp.role}</h3>
              <p className="exp-company">{exp.company}</p>
              <p className="exp-desc">{exp.description}</p>
            </div>
          </div>
        );
      })}
    </div>
  );
}
