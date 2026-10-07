/* ─────────────────────────────────────────────────────────────
   API Types — mirrors backend DTOs (hand-typed from C# records)
   ───────────────────────────────────────────────────────────── */

export interface ProjectDto {
  id: string;
  title: string;
  description: string;
  url: string | null;
  sourceUrl: string | null;
  technologies: string[];
  sortOrder: number;
  status: string;
  role: string;
  evidenceUrl: string | null;
  domain: string;
}

export type ProjectDomain = "IGaming" | "Crypto" | "RealTime" | "Fullstack" | "Workflow" | "BIM";

export const DOMAIN_LABELS: Record<ProjectDomain, string> = {
  Workflow: "Durable workflows",
  BIM: "BIM / CAD",
  IGaming: "iGaming",
  Crypto: "Crypto",
  RealTime: "Real-Time",
  Fullstack: "Fullstack",
};

export const DOMAINS: ProjectDomain[] = ["Workflow", "IGaming", "RealTime", "BIM", "Fullstack"];

export type ProjectStatus = "Live" | "Personal" | "WorkNda" | "OpenSource";

export const STATUS_LABELS: Record<ProjectStatus, string> = {
  Live: "Live",
  Personal: "Personal",
  WorkNda: "NDA",
  OpenSource: "Open Source",
};

export interface ExperienceDto {
  id: string;
  company: string;
  role: string;
  description: string;
  startDate: string; // DateOnly → ISO string
  endDate: string | null;
}

export interface SkillDto {
  id: string;
  name: string;
  category: string;
  proficiency: number;
  sortOrder: number;
  evidence: string | null;
}

export interface GitHubRepoDto {
  name: string;
  fullName: string;
  description: string | null;
  htmlUrl: string;
  language: string | null;
  stars: number;
  forks: number;
  topics: string[];
  updatedAt: string;
  pushedAt: string | null;
}

export interface ContactRequest {
  name: string;
  email: string;
  message: string;
  website?: string;
}

export interface ContactResponse {
  accepted: boolean;
  message: string;
}

export interface HealthStatusResponse {
  status: string;
}

export interface ReadinessCheck {
  name: string;
  status: string;
  description: string | null;
}

export interface ReadinessResponse {
  status: string;
  checks: ReadinessCheck[];
}

export interface ValidationProblem {
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
  errors: Record<string, string[]>;
  traceId: string;
}

export interface ProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
  instance: string;
  traceId: string;
}
