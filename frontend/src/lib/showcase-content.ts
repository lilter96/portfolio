/* Curated public-source evidence. The employment timeline preserves the existing public seed. */
import type { ProjectDto, ExperienceDto, SkillDto } from "@/types/api";

export const showcaseProjects: ProjectDto[] = [
  {
    "id": "project-0",
    "title": "JobFinder",
    "description": "Durable workflow engineering: revision witnesses, transactional outbox, typed LLM recipes and reconciliation of unknown external outcomes. 939 selected tests verified, including native PostgreSQL/process-restart scenarios.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/jobfinder-showcase",
    "technologies": [
      ".NET 10",
      "Wolverine",
      "PostgreSQL",
      "Blazor",
      "LLM"
    ],
    "sortOrder": 0,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/jobfinder-showcase/blob/main/docs/PUBLICATION-VERIFICATION.md",
    "domain": "Workflow"
  },
  {
    "id": "project-1",
    "title": "Slot Math Lab",
    "description": "Exact rational and Monte Carlo interpreters, deterministic random streams and a React graph editor. 704 core tests passed; research prototype with full-CI follow-up still needed.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/slot-math-lab",
    "technologies": [
      ".NET 10",
      "C#",
      "React 19",
      "Probability",
      "Deterministic RNG"
    ],
    "sortOrder": 1,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/slot-math-lab/blob/main/README.md",
    "domain": "IGaming"
  },
  {
    "id": "project-2",
    "title": "Realtime Primitives",
    "description": "Extracted C# library: circuit breakers, typed HTTP failures, retained audio replay and bounded flight recording. 89 offline tests and public CI.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/dotnet-realtime-primitives",
    "technologies": [
      ".NET 10",
      "Concurrency",
      "Streaming",
      "Resilience"
    ],
    "sortOrder": 2,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/dotnet-realtime-primitives/blob/main/README.md",
    "domain": "RealTime"
  },
  {
    "id": "project-3",
    "title": "AccessRoute for Revit",
    "description": "BVH spatial search, Dijkstra routing, versioned storage and WPF/MVVM. 68 shared core/presentation tests; synthetic indexed queries ~11× faster than full scan. Revit host execution remains unverified.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/access-route-revit",
    "technologies": [
      "C#",
      "Revit API",
      "WPF/MVVM",
      "Algorithms"
    ],
    "sortOrder": 3,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/access-route-revit/blob/main/docs/evidence/benchmark.json",
    "domain": "BIM"
  },
  {
    "id": "project-4",
    "title": "TGSlots",
    "description": "Modular TypeScript slot games, PixiJS rendering and Monte Carlo simulation tooling. 736 tests, all workspace typechecks and lint passed. Bun/Elysia API uses prototype in-memory state.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/tgslots",
    "technologies": [
      "TypeScript",
      "Bun",
      "Elysia",
      "PixiJS 8",
      "React"
    ],
    "sortOrder": 4,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/tgslots/blob/main/README.md",
    "domain": "IGaming"
  },
  {
    "id": "project-5",
    "title": ".NET Engineering Portfolio",
    "description": "This delivery monorepo: versioned content API, PostgreSQL/Redis, isolated Testcontainers integration tests and a bilingual React site. 56 backend + 43 frontend tests passed.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/portfolio",
    "technologies": [
      ".NET 10",
      "React 19",
      "PostgreSQL",
      "Redis",
      "Testcontainers",
      "Docker"
    ],
    "sortOrder": 5,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/portfolio/blob/main/docs/adr/0001-architecture-and-stack.md",
    "domain": "Fullstack"
  },
  {
    "id": "project-6",
    "title": "ModelGuard for Revit",
    "description": "Companion BIM tool: parameter contracts, equipment references, transactional mark updates and worksharing ownership. Shares the AccessRoute core/test suite; Revit execution unverified.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/model-guard-revit",
    "technologies": [
      "C#",
      "Revit API",
      "WPF/MVVM",
      "Transactions"
    ],
    "sortOrder": 6,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/model-guard-revit/blob/main/README.md",
    "domain": "BIM"
  }
];

export const showcaseExperience: ExperienceDto[] = [
  {
    "id": "experience-0",
    "company": "Custom Games Studio",
    "role": "Senior .NET Backend Developer",
    "description": "Backend development for casino slot games on the myKonami/Aristocrat platform. Designed game logic engines, real-money transaction pipelines, and operator tooling for a multi-title casino backend platform. Integrated RNG certification flows and jurisdictional compliance requirements.",
    "startDate": "2022-09-01",
    "endDate": null
  },
  {
    "id": "experience-1",
    "company": "Solvintech",
    "role": "Fullstack Developer (.NET + React)",
    "description": "Developed and maintained web applications across the full stack. Built REST APIs with ASP.NET Core and interactive UIs with React and TypeScript. Worked on real-time features, database design, and CI/CD pipelines.",
    "startDate": "2021-06-01",
    "endDate": "2022-08-31"
  },
  {
    "id": "experience-2",
    "company": "Elgrow",
    "role": "Software Developer",
    "description": "Contributed to commercial software projects using .NET and related technologies.",
    "startDate": "2020-03-01",
    "endDate": "2021-05-31"
  },
  {
    "id": "experience-3",
    "company": "Syberry CIS",
    "role": "Software Developer",
    "description": "Worked on enterprise client projects. Gained experience in full-cycle development, code review, and agile team practices.",
    "startDate": "2019-01-01",
    "endDate": "2020-02-28"
  },
  {
    "id": "experience-4",
    "company": "Softeq",
    "role": "Junior Developer",
    "description": "Started professional career. Built foundational skills in software engineering practices and team collaboration.",
    "startDate": "2018-03-01",
    "endDate": "2018-12-31"
  },
  {
    "id": "experience-5",
    "company": "BSUIR",
    "role": "BSc Engineering",
    "description": "Belarusian State University of Informatics and Radioelectronics. Foundation in computer science, algorithms, and software engineering.",
    "startDate": "2019-09-01",
    "endDate": "2023-06-30"
  }
];

export const showcaseSkills: SkillDto[] = [
  {
    "id": "skill-0",
    "name": "C# / .NET 10",
    "category": "Backend",
    "proficiency": 0,
    "sortOrder": 0,
    "evidence": "JobFinder: durable workflows, typed contracts; Realtime Primitives: concurrency and bounded buffers."
  },
  {
    "id": "skill-1",
    "name": "ASP.NET Core / Wolverine",
    "category": "Backend",
    "proficiency": 0,
    "sortOrder": 1,
    "evidence": "JobFinder: PostgreSQL transport and transactional outbox; Portfolio: versioned endpoints and rate limiting."
  },
  {
    "id": "skill-2",
    "name": "PostgreSQL / EF Core",
    "category": "Data",
    "proficiency": 0,
    "sortOrder": 2,
    "evidence": "JobFinder: revision witnesses and reconciliation; Portfolio: isolated database integration tests."
  },
  {
    "id": "skill-3",
    "name": "Redis",
    "category": "Data",
    "proficiency": 0,
    "sortOrder": 3,
    "evidence": "Portfolio: caching and container-based integration tests."
  },
  {
    "id": "skill-4",
    "name": "Exact probability / Monte Carlo",
    "category": "Domain",
    "proficiency": 0,
    "sortOrder": 4,
    "evidence": "Slot Math Lab: exact rational interpreter and seeded simulation; TGSlots: game math tests."
  },
  {
    "id": "skill-5",
    "name": "Revit API / WPF / MVVM",
    "category": "Domain",
    "proficiency": 0,
    "sortOrder": 5,
    "evidence": "AccessRoute and ModelGuard: core/ViewModel tests; Revit host verification pending."
  },
  {
    "id": "skill-6",
    "name": "React / TypeScript / PixiJS",
    "category": "Frontend",
    "proficiency": 0,
    "sortOrder": 6,
    "evidence": "Portfolio: bilingual UI and 43 tests; TGSlots: PixiJS renderer and modular games."
  },
  {
    "id": "skill-7",
    "name": "Docker / Testcontainers / CI",
    "category": "DevOps",
    "proficiency": 0,
    "sortOrder": 7,
    "evidence": "JobFinder: real PostgreSQL/process-boundary tests; Portfolio: PostgreSQL + Redis fixtures."
  },
  {
    "id": "skill-8",
    "name": "AI-assisted engineering",
    "category": "Practices",
    "proficiency": 0,
    "sortOrder": 8,
    "evidence": "Versioned LLM recipes, architecture decisions, test/review workflows and explicit verification boundaries."
  }
];
