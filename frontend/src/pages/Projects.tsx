import { useMemo, useState, useCallback } from "react";
import { useTranslation } from "react-i18next";
import { projectDescriptionsRussian } from "@/lib/project-locales";
import { useProjects } from "@/hooks/useProjects";
import { useGitHubRepos } from "@/hooks/useGitHubRepos";
import { QueryState } from "@/components/QueryState";
import { DOMAINS, DOMAIN_LABELS } from "@/types/api";
import type { ProjectDto, GitHubRepoDto, ProjectDomain, ProjectStatus } from "@/types/api";

const FLAGSHIP_NAMES = ["JobFinder", "Slot Math Lab"];

export function Projects() {
  const { t } = useTranslation();
  const projects = useProjects();
  const github = useGitHubRepos();
  const [techFilter, setTechFilter] = useState<string | null>(null);
  const [domainFilter, setDomainFilter] = useState<ProjectDomain | null>(null);

  const allTags = useMemo(() => {
    if (!projects.data) return [];
    const tags = new Set<string>();
    for (const p of projects.data) {
      for (const tech of p.technologies) tags.add(tech);
    }
    return [...tags].sort((a, b) => a.localeCompare(b));
  }, [projects.data]);

  const clearTechFilter = useCallback(() => setTechFilter(null), []);
  const clearDomainFilter = useCallback(() => setDomainFilter(null), []);

  const isLoading = projects.isLoading || github.isLoading;
  const error = projects.error ?? github.error;


  return (
    <section id="projects" className="projects-section" aria-label={t("nav.projects")}>
      <p className="eyebrow">{t("projects.eyebrow")}</p>
      <h2 className="section-title reveal">{t("nav.projects")}</h2>

      <p className="section-lead">{t("projects.intro")}</p>
      <a className="workflow-preview reveal" href="https://github.com/lilter96/jobfinder-showcase" target="_blank" rel="noopener noreferrer">
        <img src={`${import.meta.env.BASE_URL}images/jobfinder-workflow.png`} alt="JobFinder synthetic acceptance: revision conflict preserves the unsaved application draft" loading="lazy" />
      </a>
      <p className="preview-caption">{t("projects.previewCaption")}</p>

      {/* ── Domain filters ────────────────────────────────── */}
      <div className="projects-filters reveal reveal-1" role="group" aria-label={t("projects.filterByDomain")}>
        <button
          className={`filter-tag ${domainFilter === null ? "filter-tag-active" : ""}`}
          onClick={clearDomainFilter}
          type="button"
          aria-pressed={domainFilter === null}
        >
          {t("projects.allDomains")}
        </button>
        {DOMAINS.map((domain) => (
          <button
            key={domain}
            className={`filter-tag filter-tag-domain ${domainFilter === domain ? "filter-tag-active" : ""}`}
            onClick={() => setDomainFilter(domainFilter === domain ? null : domain)}
            type="button"
            aria-pressed={domainFilter === domain}
          >
            {t(`projects.domain${domain}`, DOMAIN_LABELS[domain])}
          </button>
        ))}
      </div>

      {/* ── Tech tag filters ──────────────────────────────── */}
      {allTags.length > 0 && (
        <div className="projects-filters reveal reveal-1" role="group" aria-label={t("projects.filterBy")}>
          <button
            className={`filter-tag ${techFilter === null ? "filter-tag-active" : ""}`}
            onClick={clearTechFilter}
            type="button"
            aria-pressed={techFilter === null}
          >
            {t("projects.all")}
          </button>
          {allTags.slice(0, 12).map((tag) => (
            <button
              key={tag}
              className={`filter-tag ${techFilter === tag ? "filter-tag-active" : ""}`}
              onClick={() => setTechFilter(techFilter === tag ? null : tag)}
              type="button"
              aria-pressed={techFilter === tag}
            >
              {tag}
            </button>
          ))}
        </div>
      )}

      <QueryState
        data={projects.data}
        isLoading={isLoading}
        error={error}
      >
        {(data) => (
          <ProjectsGrid
            projects={data}
            githubRepos={github.data ?? []}
            techFilter={techFilter}
            domainFilter={domainFilter}
          />
        )}
      </QueryState>
    </section>
  );
}

/* ─────────────────────────────────────────────────────────────
   Status Badge
   ───────────────────────────────────────────────────────────── */

function StatusBadge({ status }: { status: ProjectStatus }) {
  const { t } = useTranslation();

  return (
    <span className={`status-badge status-badge-${status.toLowerCase()}`}>
      {status === "Live" && <span className="status-dot" aria-hidden="true" />}
      {status === "WorkNda" && <span className="status-lock" aria-hidden="true">🔒</span>}
      {t(`projects.status${status}`)}
    </span>
  );
}

/* ─────────────────────────────────────────────────────────────
   Project Grid
   ───────────────────────────────────────────────────────────── */

function ProjectsGrid({
  projects,
  githubRepos,
  techFilter,
  domainFilter,
}: {
  projects: ProjectDto[];
  githubRepos: GitHubRepoDto[];
  techFilter: string | null;
  domainFilter: ProjectDomain | null;
}) {
  const filtered = useMemo(() => {
    let result = projects;

    if (domainFilter) {
      result = result.filter((p) => p.domain === domainFilter);
    }

    if (techFilter) {
      result = result.filter((p) =>
        p.technologies.some((t) => t.toLowerCase() === techFilter.toLowerCase()),
      );
    }

    return result;
  }, [projects, techFilter, domainFilter]);

  if (filtered.length === 0) {
    return (
      <div className="projects-empty" role="status">
        <p className="projects-empty-text">No projects match the selected filters.</p>
      </div>
    );
  }

  return (
    <div className="projects-grid" role="list">
      {filtered.map((project, i) => {
        const ghMatch = githubRepos.find(
          (r) =>
            r.name === project.title ||
            project.title.toLowerCase().includes(r.name.toLowerCase()) ||
            project.sourceUrl?.includes(r.name),
        );
        const isFlagship = FLAGSHIP_NAMES.some((n) => project.title.includes(n));

        return (
          <ProjectCard
            key={project.id}
            project={project}
            github={ghMatch}
            isFlagship={isFlagship}
            delay={((i % 6) + 1) as 1 | 2 | 3 | 4 | 5 | 6}
          />
        );
      })}
    </div>
  );
}

/* ─────────────────────────────────────────────────────────────
   Project Card
   ───────────────────────────────────────────────────────────── */

function ProjectCard({
  project,
  github,
  isFlagship,
  delay,
}: {
  project: ProjectDto;
  github?: GitHubRepoDto;
  isFlagship: boolean;
  delay: 1 | 2 | 3 | 4 | 5 | 6;
}) {
  const { t, i18n } = useTranslation();

  return (
    <div
      className={`project-card reveal reveal-${delay} ${isFlagship ? "project-card-flagship" : ""}`}
      role="listitem"
    >
      {/* Header row: status badge + domain label */}
      <div className="project-card-header">
        <StatusBadge status={project.status as ProjectStatus} />
        {project.domain && (
          <span className="project-domain-label">
            {t(`projects.domain${project.domain}`, DOMAIN_LABELS[project.domain as ProjectDomain] ?? project.domain)}
          </span>
        )}
        {isFlagship && <span className="project-flagship-star" aria-label="Flagship project">★</span>}
      </div>

      <h3 className="project-title">{project.title}</h3>

      {project.role && (
        <p className="project-role">{project.role === "Personal engineering project" ? t("projects.personalRole") : project.role}</p>
      )}

      <p className="project-desc">{i18n.language.startsWith("ru") ? projectDescriptionsRussian[project.title] ?? project.description : project.description}</p>

      {/* Tech tags */}
      <div className="project-tech-list">
        {project.technologies.map((tech) => (
          <span key={tech} className="project-tech-tag">
            {tech}
          </span>
        ))}
      </div>

      {/* GitHub stats + links */}
      <div className="project-meta">
        <div className="project-links">
          {project.url && (
            <a
              href={project.url}
              target="_blank"
              rel="noopener noreferrer"
              className="project-link"
            >
              {project.title.includes("TGSlots") ? t("projects.playLive") : `${t("projects.live")} ↗`}
            </a>
          )}
          {project.sourceUrl && (
            <a
              href={project.sourceUrl}
              target="_blank"
              rel="noopener noreferrer"
              className="project-link project-link-source"
            >
              {t("projects.source")} ↗
            </a>
          )}
          {project.evidenceUrl && project.evidenceUrl !== project.url && project.evidenceUrl !== project.sourceUrl && (
            <a
              href={project.evidenceUrl}
              target="_blank"
              rel="noopener noreferrer"
              className="project-link project-link-evidence"
            >
              {t("projects.evidence")} ↗
            </a>
          )}
        </div>

        {github && (
          <div className="project-gh-stats mono">
            <span className="gh-stat" title={`${github.stars} stars`}>
              ⭐&nbsp;{github.stars}
            </span>
            <span className="gh-stat" title={`${github.forks} forks`}>
              🍴&nbsp;{github.forks}
            </span>
            {github.language && (
              <span className="gh-stat">{github.language}</span>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
