/* ─────────────────────────────────────────────────────────────
   API Client — typed fetch wrappers for all backend endpoints
   ───────────────────────────────────────────────────────────── */

import type {
  ProjectDto,
  ExperienceDto,
  SkillDto,
  GitHubRepoDto,
  ContactRequest,
  ContactResponse,
  HealthStatusResponse,
  ReadinessResponse,
} from "@/types/api";

import { isStaticShowcase } from "@/lib/runtime-config";
import { showcaseProjects, showcaseExperience, showcaseSkills } from "@/lib/showcase-content";

const BASE = import.meta.env.VITE_API_BASE_URL ?? "";


class ApiError extends Error {
  status: number;
  detail: string;

  constructor(status: number, detail: string) {
    super(detail);
    this.name = "ApiError";
    this.status = status;
    this.detail = detail;
  }
}

async function fetchJson<T>(path: string, init?: RequestInit): Promise<T> {
  const url = `${BASE}${path}`;
  const res = await fetch(url, {
    ...init,
    headers: { "Content-Type": "application/json", ...init?.headers },
  });

  if (!res.ok) {
    const body = await res.json().catch(() => ({}));
    throw new ApiError(res.status, body.detail ?? res.statusText);
  }

  return res.json() as Promise<T>;
}

/* ── Content ────────────────────────────────────────────── */

export function fetchProjects(): Promise<ProjectDto[]> {
  if (isStaticShowcase) return Promise.resolve(showcaseProjects);
  return fetchJson("/api/v1/projects");
}

export function fetchExperience(): Promise<ExperienceDto[]> {
  if (isStaticShowcase) return Promise.resolve(showcaseExperience);
  return fetchJson("/api/v1/experience");
}

export function fetchSkills(): Promise<SkillDto[]> {
  if (isStaticShowcase) return Promise.resolve(showcaseSkills);
  return fetchJson("/api/v1/skills");
}

/* ── GitHub ──────────────────────────────────────────────── */

export function fetchGitHubRepos(): Promise<GitHubRepoDto[]> {
  if (isStaticShowcase) return Promise.resolve([]);
  return fetchJson("/api/v1/github/repos");
}

/* ── Health ──────────────────────────────────────────────── */

export function fetchHealth(): Promise<HealthStatusResponse> {
  return fetchJson("/health");
}

export function fetchReadiness(): Promise<ReadinessResponse> {
  return fetchJson("/health/ready");
}

/* ── Contact ─────────────────────────────────────────────── */

export async function submitContact(data: ContactRequest): Promise<ContactResponse> {
  return fetchJson("/api/v1/contact", {
    method: "POST",
    body: JSON.stringify(data),
  });
}
