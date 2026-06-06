import { useTranslation } from "react-i18next";
import { useProjects } from "@/hooks/useProjects";
import { useExperience } from "@/hooks/useExperience";
import { useSkills } from "@/hooks/useSkills";
import { useGitHubRepos } from "@/hooks/useGitHubRepos";
import { QueryState } from "@/components/QueryState";
import type { ProjectDto, ExperienceDto, SkillDto, GitHubRepoDto } from "@/types/api";

export function DesignSystem() {
  const { t } = useTranslation();
  return (
    <div style={{ maxWidth: 960, margin: "0 auto", padding: "var(--space-16) var(--space-6)" }}>
      {/* ── Header ─────────────────────────────────────────── */}
      <header style={{ marginBottom: "var(--space-20)" }}>
        <p
          className="display reveal reveal-1"
          style={{
            fontSize: "var(--text-xs)",
            color: "var(--color-primary)",
            marginBottom: "var(--space-4)",
            letterSpacing: "0.2em",
          }}
        >
          {t("design.title")}
        </p>
        <h1
          className="reveal reveal-2"
          style={{
            fontFamily: "var(--font-display)",
            fontSize: "var(--text-5xl)",
            lineHeight: 1,
            letterSpacing: "0.04em",
            marginBottom: "var(--space-6)",
          }}
        >
          TERMINAL
          <br />
          <span style={{ color: "var(--color-accent-cherry)" }}>CASINO</span>
        </h1>
        <p
          className="reveal reveal-3 cursor"
          style={{ fontSize: "var(--text-md)", maxWidth: "32rem" }}
        >
          {t("design.subtitle")}
        </p>
      </header>

      {/* ── Color Palette ──────────────────────────────────── */}
      <Section title={t("design.colorPalette")} delay={4}>
        <SwatchRow>
          <Swatch color="--color-bg-primary" label={t("design.swatchPrimaryBg")} />
          <Swatch color="--color-bg-secondary" label={t("design.swatchSecondaryBg")} />
          <Swatch color="--color-bg-tertiary" label={t("design.swatchTertiaryBg")} />
          <Swatch color="--color-bg-elevated" label={t("design.swatchElevatedBg")} />
        </SwatchRow>
        <SwatchRow>
          <Swatch color="--color-accent-green" label={t("design.swatchTerminalGreen")} />
          <Swatch color="--color-accent-amber" label={t("design.swatchJackpotAmber")} />
          <Swatch color="--color-accent-cherry" label={t("design.swatchCherryRed")} />
          <Swatch color="--color-accent-gold" label={t("design.swatchGold")} />
          <Swatch color="--color-accent-blue" label={t("design.swatchCrtBlue")} />
        </SwatchRow>
      </Section>

      {/* ── Typography ─────────────────────────────────────── */}
      <Section title={t("design.typography")} delay={5}>
        <p className="mono" style={{ color: "var(--color-text-muted)", marginBottom: "var(--space-6)" }}>
          {t("design.typeDisplay")} &nbsp;|&nbsp; {t("design.typeHeadings")} &nbsp;|&nbsp; {t("design.typeBody")}
        </p>

        <TypeRow label={t("design.typeDisplay5xl")} font="var(--font-display)" size="var(--text-5xl)" weight={900}>
          {t("design.typeJackpot")}
        </TypeRow>
        <TypeRow label={t("design.typeHeading3xl")} font="var(--font-heading)" size="var(--text-3xl)" weight={700}>
          {t("design.typeHeadingSample")}
        </TypeRow>
        <TypeRow label={t("design.typeHeadingXl")} font="var(--font-heading)" size="var(--text-xl)" weight={600}>
          {t("design.typeHeadingSmallSample")}
        </TypeRow>
        <TypeRow label={t("design.typeBodyBase")} font="var(--font-body)" size="var(--text-base)" weight={400}>
          {t("design.typeBodySample")}
        </TypeRow>
        <TypeRow label={t("design.typeMonoSm")} font="var(--font-mono)" size="var(--text-sm)" weight={400}>
          {t("design.typeMonoSample")}
        </TypeRow>
      </Section>

      {/* ── Motion Demo ────────────────────────────────────── */}
      <Section title={t("design.motion")} delay={6}>
        <div style={{ display: "flex", gap: "var(--space-4)", flexWrap: "wrap" }}>
          <MotionCard className="neon" label={t("design.motionNeonPulse")} />
          <MotionCard className="spin" label={t("design.motionSlotSpin")} accent="var(--color-accent-cherry)" />
          <MotionCard className="reveal" label={t("design.motionStaggered")} />
          <MotionCard
            className="glow-hover"
            label={t("design.motionGlowHover")}
            style={{ cursor: "pointer" }}
            accent="var(--color-accent-amber)"
          />
        </div>
      </Section>

      {/* ── Cards ───────────────────────────────────────────── */}
      <Section title={t("design.surfacePrimitives")} delay={7}>
        <div className="grid-2col">
          <Card>
            <h4 className="display" style={{ fontSize: "var(--text-sm)", color: "var(--color-primary)", marginBottom: "var(--space-3)" }}>
              {t("design.rngCertified")}
            </h4>
            <p style={{ fontSize: "var(--text-sm)", margin: 0 }}>
              {t("design.rngDesc")}
            </p>
            <div
              style={{
                marginTop: "var(--space-4)",
                padding: "var(--space-2) var(--space-3)",
                background: "var(--color-bg-tertiary)",
                borderRadius: "var(--radius-md)",
                fontFamily: "var(--font-mono)",
                fontSize: "var(--text-xs)",
                color: "var(--color-primary)",
              }}
            >
              0xa3f2...b71e
            </div>
          </Card>

          <Card>
            <h4 className="display" style={{ fontSize: "var(--text-sm)", color: "var(--color-accent-amber)", marginBottom: "var(--space-3)" }}>
              {t("design.paylineEngine")}
            </h4>
            <p style={{ fontSize: "var(--text-sm)", margin: 0 }}>
              {t("design.paylineDesc")}
            </p>
            <div
              style={{
                marginTop: "var(--space-4)",
                display: "flex",
                gap: "var(--space-2)",
                fontFamily: "var(--font-mono)",
                fontSize: "var(--text-xs)",
              }}
            >
              <span style={{ color: "var(--color-accent-cherry)" }}>🍒</span>
              <span style={{ color: "var(--color-accent-cherry)" }}>🍒</span>
              <span style={{ color: "var(--color-accent-cherry)" }}>🍒</span>
              <span style={{ color: "var(--color-text-muted)" }}>→</span>
              <span style={{ color: "var(--color-accent-gold)" }}>WIN x5</span>
            </div>
          </Card>
        </div>
      </Section>

      {/* ── Spacing Scale ──────────────────────────────────── */}
      <Section title={t("design.spacingScale")} delay={8}>
        <div>
          {[1, 2, 4, 6, 8, 12, 16, 20, 24].map((s) => (
            <div
              key={s}
              style={{
                display: "flex",
                alignItems: "center",
                gap: "var(--space-4)",
                marginBottom: "var(--space-2)",
              }}
            >
              <span className="mono" style={{ fontSize: "var(--text-xs)", color: "var(--color-text-muted)", width: 48 }}>
                {s}
              </span>
              <div
                style={{
                  width: `var(--space-${s})`,
                  height: 8,
                  background: "var(--color-primary)",
                  borderRadius: "var(--radius-full)",
                  transition: "width var(--duration-normal) var(--ease-out-expo)",
                }}
              />
            </div>
          ))}
        </div>
      </Section>

      {/* ── Live Data (API) ────────────────────────────────── */}
      <LiveDataSection delay={8} />

    </div>
  );
}

/* ── Helper Components ──────────────────────────────────────── */

function Section({
  title,
  delay,
  children,
}: {
  title: string;
  delay: number;
  children: React.ReactNode;
}) {
  return (
    <section className={`reveal reveal-${delay}`} style={{ marginBottom: "var(--space-20)" }}>
      <h2
        className="display"
        style={{
          fontSize: "var(--text-sm)",
          color: "var(--color-primary)",
          marginBottom: "var(--space-6)",
          paddingBottom: "var(--space-2)",
          borderBottom: "var(--border-glow)",
          letterSpacing: "0.15em",
        }}
      >
        {title}
      </h2>
      {children}
    </section>
  );
}

function SwatchRow({ children }: { children: React.ReactNode }) {
  return (
    <div
      style={{
        display: "flex",
        gap: "var(--space-3)",
        marginBottom: "var(--space-3)",
        flexWrap: "wrap",
      }}
    >
      {children}
    </div>
  );
}

function Swatch({ color, label }: { color: string; label: string }) {
  return (
    <div style={{ display: "flex", alignItems: "center", gap: "var(--space-2)" }}>
      <div
        style={{
          width: 40,
          height: 40,
          borderRadius: "var(--radius-md)",
          background: `var(${color})`,
          border: "var(--border-thin)",
        }}
      />
      <div>
        <p className="mono" style={{ fontSize: "var(--text-xs)", margin: 0 }}>
          {label}
        </p>
        <p className="mono" style={{ fontSize: "10px", color: "var(--color-text-muted)", margin: 0 }}>
          {color}
        </p>
      </div>
    </div>
  );
}

function TypeRow({
  label,
  font,
  size,
  weight,
  children,
}: {
  label: string;
  font: string;
  size: string;
  weight: number;
  children: React.ReactNode;
}) {
  return (
    <div style={{ marginBottom: "var(--space-6)" }}>
      <p className="mono" style={{ fontSize: "var(--text-xs)", color: "var(--color-text-muted)", marginBottom: "var(--space-1)" }}>
        {label} &nbsp;·&nbsp; {font.split(",")[0]?.replace(/"/g, "")} {weight}
      </p>
      <div style={{ fontFamily: font, fontSize: size, fontWeight: weight, lineHeight: "var(--leading-normal)" }}>
        {children}
      </div>
    </div>
  );
}

function Card({ children }: { children: React.ReactNode }) {
  return (
    <div
      style={{
        background: "var(--color-surface-card)",
        border: "var(--border-glow)",
        borderRadius: "var(--radius-lg)",
        padding: "var(--space-6)",
        transition: "all var(--duration-normal) var(--ease-out-expo)",
      }}
      className="glow-hover"
    >
      {children}
    </div>
  );
}

function MotionCard({
  className,
  label,
  accent,
  style,
}: {
  className: string;
  label: string;
  accent?: string;
  style?: React.CSSProperties;
}) {
  return (
    <div
      className={className}
      style={{
        width: 120,
        height: 120,
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        background: "var(--color-surface-card)",
        border: `1px solid ${accent ?? "var(--color-primary)"}`,
        borderRadius: "var(--radius-lg)",
        fontFamily: "var(--font-display)",
        fontSize: "var(--text-xs)",
        textTransform: "uppercase",
        letterSpacing: "0.05em",
        color: accent ?? "var(--color-primary)",
        textAlign: "center",
        ...style,
      }}
    >
      {label}
    </div>
  );
}

/* ── Live Data Section ──────────────────────────────────────── */

function LiveDataSection({ delay }: { delay: number }) {
  const { t } = useTranslation();
  const projects = useProjects();
  const experience = useExperience();
  const skills = useSkills();
  const github = useGitHubRepos();

  return (
    <section className={`reveal reveal-${delay}`} style={{ marginBottom: "var(--space-20)" }}>
      <h2
        className="display"
        style={{
          fontSize: "var(--text-sm)",
          color: "var(--color-accent-blue)",
          marginBottom: "var(--space-6)",
          paddingBottom: "var(--space-2)",
          borderBottom: "var(--border-glow)",
          letterSpacing: "0.15em",
        }}
      >
        {t("design.liveData")}
      </h2>

      <div className="grid-2col">
        {/* Projects */}
        <Card>
          <h4 className="display" style={{ fontSize: "var(--text-xs)", color: "var(--color-primary)", marginBottom: "var(--space-4)" }}>
            {t("design.projectsLabel")}
          </h4>
          <QueryState data={projects.data} isLoading={projects.isLoading} error={projects.error}>
            {(data: ProjectDto[]) => (
              <ul style={{ listStyle: "none", padding: 0 }}>
                {data.slice(0, 3).map((p) => (
                  <li
                    key={p.id}
                    style={{
                      padding: "var(--space-2) 0",
                      borderBottom: "var(--border-thin)",
                      fontSize: "var(--text-sm)",
                    }}
                  >
                    <span style={{ color: "var(--color-text-primary)" }}>{p.title}</span>
                    <span className="mono" style={{ color: "var(--color-text-muted)", marginLeft: "var(--space-2)", fontSize: "var(--text-xs)" }}>
                      {p.technologies.slice(0, 3).join(", ")}
                    </span>
                  </li>
                ))}
              </ul>
            )}
          </QueryState>
        </Card>

        {/* Experience */}
        <Card>
          <h4 className="display" style={{ fontSize: "var(--text-xs)", color: "var(--color-accent-amber)", marginBottom: "var(--space-4)" }}>
            {t("design.experienceLabel")}
          </h4>
          <QueryState data={experience.data} isLoading={experience.isLoading} error={experience.error}>
            {(data: ExperienceDto[]) => (
              <ul style={{ listStyle: "none", padding: 0 }}>
                {data.map((e) => (
                  <li key={e.id} style={{ padding: "var(--space-2) 0", borderBottom: "var(--border-thin)" }}>
                    <p style={{ margin: 0, fontSize: "var(--text-sm)", color: "var(--color-text-primary)" }}>
                      {e.role}
                    </p>
                    <p className="mono" style={{ margin: 0, fontSize: "var(--text-xs)", color: "var(--color-text-muted)" }}>
                      {e.company}
                    </p>
                  </li>
                ))}
              </ul>
            )}
          </QueryState>
        </Card>

        {/* Skills */}
        <Card>
          <h4 className="display" style={{ fontSize: "var(--text-xs)", color: "var(--color-accent-cherry)", marginBottom: "var(--space-4)" }}>
            {t("design.skillsLabel")}
          </h4>
          <QueryState data={skills.data} isLoading={skills.isLoading} error={skills.error}>
            {(data: SkillDto[]) => (
              <div style={{ display: "flex", flexWrap: "wrap", gap: "var(--space-1)" }}>
                {data.slice(0, 8).map((s) => (
                  <span
                    key={s.id}
                    className="mono"
                    style={{
                      fontSize: "var(--text-xs)",
                      padding: "var(--space-1) var(--space-2)",
                      background: "var(--color-bg-tertiary)",
                      borderRadius: "var(--radius-md)",
                      color: "var(--color-text-secondary)",
                    }}
                  >
                    {s.name}
                  </span>
                ))}
              </div>
            )}
          </QueryState>
        </Card>

        {/* GitHub */}
        <Card>
          <h4 className="display" style={{ fontSize: "var(--text-xs)", color: "var(--color-accent-blue)", marginBottom: "var(--space-4)" }}>
            {t("design.githubLabel")}
          </h4>
          <QueryState data={github.data} isLoading={github.isLoading} error={github.error}>
            {(data: GitHubRepoDto[]) => (
              <ul style={{ listStyle: "none", padding: 0 }}>
                {data.slice(0, 3).map((r) => (
                  <li key={r.name} style={{ padding: "var(--space-2) 0", borderBottom: "var(--border-thin)" }}>
                    <div style={{ display: "flex", alignItems: "center", gap: "var(--space-2)" }}>
                      <span style={{ fontSize: "var(--text-sm)", color: "var(--color-text-primary)" }}>
                        {r.name}
                      </span>
                      <span className="mono" style={{ fontSize: "var(--text-xs)", color: "var(--color-accent-gold)" }}>
                        ⭐{r.stars}
                      </span>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </QueryState>
        </Card>
      </div>
    </section>
  );
}
