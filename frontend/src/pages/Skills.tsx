import { useTranslation } from "react-i18next";
import { useSkills } from "@/hooks/useSkills";
import { QueryState } from "@/components/QueryState";
import type { SkillDto } from "@/types/api";

const CATEGORY_ORDER: Record<string, number> = {
  Backend: 0,
  Domain: 1,
  Frontend: 2,
  Data: 3,
  DevOps: 4,
  Blockchain: 5,
  Practices: 6,
};

const CATEGORY_ACCENTS: Record<string, string> = {
  Backend: "var(--color-accent-green)",
  Domain: "var(--color-accent-amber)",
  Frontend: "var(--color-accent-blue)",
  Data: "var(--color-accent-gold)",
  DevOps: "var(--color-accent-blue)",
  Blockchain: "var(--color-accent-cherry)",
  Practices: "var(--color-text-secondary)",
};

export function Skills() {
  const { t } = useTranslation();
  const query = useSkills();

  return (
    <section id="skills" className="skills-section" aria-label={t("nav.skills")}>
      <h2 className="section-title reveal">{t("nav.skills")}</h2>

      <QueryState data={query.data} isLoading={query.isLoading} error={query.error}>
        {(data) => <SkillsGrid skills={data} />}
      </QueryState>
    </section>
  );
}

function SkillsGrid({ skills }: { skills: SkillDto[] }) {
  const grouped = new Map<string, SkillDto[]>();

  for (const s of skills) {
    if (!grouped.has(s.category)) {
      grouped.set(s.category, []);
    }
    grouped.get(s.category)!.push(s);
  }

  const entries = [...grouped.entries()].sort(
    (a, b) => (CATEGORY_ORDER[a[0]] ?? 99) - (CATEGORY_ORDER[b[0]] ?? 99),
  );

  return (
    <div className="skills-grid" role="list">
      {entries.map(([category, items], ci) => {
        const sorted = [...items].sort((a, b) => a.sortOrder - b.sortOrder);
        const accent = CATEGORY_ACCENTS[category] ?? "var(--color-primary)";

        return (
          <div
            key={category}
            className={`skills-category reveal reveal-${((ci % 6) + 1) as 1 | 2 | 3 | 4 | 5 | 6}`}
            role="listitem"
            aria-label={category}
          >
            <h3 className="skills-cat-title mono" style={{ color: accent }}>
              {category}
            </h3>

            {sorted.map((skill, si) => (
              <div
                key={skill.id}
                className={`skill-bar reveal reveal-${((si % 4) + 1) as 1 | 2 | 3 | 4}`}
              >
                <div className="skill-bar-head">
                  <span className="skill-name">
                    {skill.name}
                    {!skill.evidence && (
                      <span className="skill-familiar-badge" title="Learning / familiar — no public evidence yet">
                        familiar
                      </span>
                    )}
                    {skill.evidence && (
                      <span className="skill-evidence-icon" title={skill.evidence} aria-label={`Evidence: ${skill.evidence}`}>
                        🔗
                      </span>
                    )}
                  </span>
                </div>
                {skill.evidence && (
                  <p className="skill-evidence-text mono">{skill.evidence}</p>
                )}
              </div>
            ))}
          </div>
        );
      })}
    </div>
  );
}
