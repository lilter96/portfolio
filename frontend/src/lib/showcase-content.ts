/* Curated public-source evidence. Employment and skills follow the user-supplied CVs. */
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
    "description": "Four TypeScript/PixiJS games with Monte Carlo tooling. X7 Club adds a Go session server and RabbitMQ-backed math worker. 785 platform tests and 7 Go race tests passed; live duplicate-request and bonus accounting checks passed. Demo sessions are in memory.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/tgslots",
    "technologies": [
      "TypeScript",
      "Go",
      "RabbitMQ",
      "Bun",
      "Elysia",
      "PixiJS 8",
      "React"
    ],
    "sortOrder": 4,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/tgslots/blob/master/docs/x7-verification.md",
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
  },
  {
    "id": "trading-systems-lab",
    "title": "Trading Systems Lab",
    "description": "C#/.NET and Go execution boundaries: protobuf/gRPC contracts, order state, PostgreSQL audit immutability and an independent emergency watchdog. 151 .NET tests passed, one optional benchmark skipped; Go watchdog tests passed. Live exchange behavior unverified.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/trading-systems-lab",
    "technologies": [
      "C#",
      "Go",
      "gRPC",
      "PostgreSQL",
      "Resilience"
    ],
    "sortOrder": 7,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/trading-systems-lab/blob/main/README.md",
    "domain": "RealTime"
  },
  {
    "id": "signal-processing-lab",
    "title": "Signal Processing Lab",
    "description": "Python asynchronous signal processing with typed LLM/media parsing, SQLite campaigns, dry-run execution and risk invariants. 340 offline tests passed. Sanitized research snapshot; live providers and screener acceptance unverified.",
    "url": null,
    "sourceUrl": "https://github.com/lilter96/signal-processing-lab",
    "technologies": [
      "Python",
      "Asyncio",
      "LLM",
      "SQLite",
      "Testing"
    ],
    "sortOrder": 8,
    "status": "OpenSource",
    "role": "Personal engineering project",
    "evidenceUrl": "https://github.com/lilter96/signal-processing-lab/blob/main/README.md",
    "domain": "Workflow"
  }
];

export const showcaseExperience: ExperienceDto[] = [{"id": "custom-games-studio", "company": "Custom Games Studio", "role": ".NET Developer", "description": "Game backend platform for myKONAMI: 5M+ users. Full game backends, shared mathematics and simulation infrastructure.", "startDate": "2023-11-01", "endDate": "2026-07-01"}, {"id": "solvintech", "company": "Solvintech", "role": "C# / .NET Developer", "description": "MPsklad marketplace SaaS: Ozon, Wildberries, Yandex Market and MoySklad; 50k+ orders/day.", "startDate": "2022-07-01", "endDate": "2023-10-01"}, {"id": "elgrow", "company": "Elgrow", "role": "C# / .NET Developer", "description": "Smart parking for Domodedovo Airport: 1,000+ spaces and 10,000+ users. Booking, payments and equipment integration.", "startDate": "2021-01-01", "endDate": "2022-06-01"}];

export const showcaseSkills: SkillDto[] = [{"id": "resume-skill-0", "name": "C# / .NET / ASP.NET Core", "category": "Backend", "proficiency": 0, "sortOrder": 0, "evidence": "Production backend engineering across gaming, e-commerce and transportation."}, {"id": "resume-skill-1", "name": "Distributed Systems / CQRS / Outbox / Inbox", "category": "Backend", "proficiency": 0, "sortOrder": 1, "evidence": "Transactional consistency, idempotency, asynchronous delivery and separate read/write models."}, {"id": "resume-skill-2", "name": "REST / gRPC / WebSockets / SignalR", "category": "Backend", "proficiency": 0, "sortOrder": 2, "evidence": "Service contracts, mobile APIs and real-time delivery."}, {"id": "resume-skill-3", "name": "PostgreSQL / MS SQL Server / EF Core / Dapper", "category": "Data", "proficiency": 0, "sortOrder": 3, "evidence": "Order consistency, conditional financial updates and transactional persistence."}, {"id": "resume-skill-4", "name": "Couchbase / MongoDB / Redis", "category": "Data", "proficiency": 0, "sortOrder": 4, "evidence": "Distributed profile state, read models, coordination, rate limits and caching."}, {"id": "resume-skill-5", "name": "Kafka / RabbitMQ / MassTransit / Hangfire", "category": "Messaging", "proficiency": 0, "sortOrder": 5, "evidence": "Ordered events, background marketplace integrations and equipment isolation."}, {"id": "resume-skill-6", "name": "C# / F# game mathematics", "category": "Game engineering", "proficiency": 0, "sortOrder": 6, "evidence": "Probability monads, trampolines, exact enumeration, unbiased RNG, alias method and segment trees."}, {"id": "resume-skill-7", "name": "RTP / volatility / Monte Carlo", "category": "Game engineering", "proficiency": 0, "sortOrder": 7, "evidence": "1B+ spin simulations, confidence intervals, distribution checks, checkpoints and deterministic replay."}, {"id": "resume-skill-8", "name": "Span<T> / ArrayPool<T> / Roslyn", "category": "Performance", "proficiency": 0, "sortOrder": 8, "evidence": "Allocation/GC optimization, readonly structs and compile-time serialization."}, {"id": "resume-skill-9", "name": "React / TypeScript / MobX", "category": "Frontend", "proficiency": 0, "sortOrder": 9, "evidence": "Commercial frontend delivery and generated C# / TypeScript contracts."}, {"id": "resume-skill-10", "name": "Docker / Kubernetes / Linux / CI/CD", "category": "Infrastructure", "proficiency": 0, "sortOrder": 10, "evidence": "Containerized delivery, development environments and integration verification."}, {"id": "resume-skill-11", "name": "OpenTelemetry / Prometheus / Grafana", "category": "Infrastructure", "proficiency": 0, "sortOrder": 11, "evidence": "Distributed-system observability and production incident analysis."}, {"id": "resume-skill-12", "name": "xUnit / Testcontainers / AI-assisted development", "category": "Practices", "proficiency": 0, "sortOrder": 12, "evidence": "Behavioral and integration tests; Claude, Cursor and Copilot for implementation, refactoring and test generation."}];
