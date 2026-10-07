namespace Portfolio.Infrastructure.Data.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Portfolio.Domain.Entities;

    public sealed class DbSeeder
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(AppDbContext context, ILogger<DbSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await SeedUsersAsync(cancellationToken);
            await SeedExperienceAsync(cancellationToken);
            await SeedSkillsAsync(cancellationToken);
            await SeedProjectsAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(cancellationToken)) return;
            _context.Users.Add(new User("terentiy.gatsukov@gmail.com", "Terentiy Gatsukov"));
            _logger.LogInformation("Seeded users");
        }

        private async Task SeedExperienceAsync(CancellationToken cancellationToken)
        {
            if (await _context.Experiences.AnyAsync(cancellationToken)) return;

            _context.Experiences.AddRange(
                new Experience(
                    "Custom Games Studio",
                    "Senior .NET Backend Developer",
                    "Backend development for casino slot games on the myKonami/Aristocrat platform. "
                    + "Designed game logic engines, real-money transaction pipelines, and operator "
                    + "tooling for a multi-title casino backend platform. Integrated RNG certification "
                    + "flows and jurisdictional compliance requirements.",
                    new DateOnly(2022, 9, 1),
                    null),

                new Experience(
                    "Solvintech",
                    "Fullstack Developer (.NET + React)",
                    "Developed and maintained web applications across the full stack. "
                    + "Built REST APIs with ASP.NET Core and interactive UIs with React and TypeScript. "
                    + "Worked on real-time features, database design, and CI/CD pipelines.",
                    new DateOnly(2021, 6, 1),
                    new DateOnly(2022, 8, 31)),

                new Experience(
                    "Elgrow",
                    "Software Developer",
                    "Contributed to commercial software projects using .NET and related technologies.",
                    new DateOnly(2020, 3, 1),
                    new DateOnly(2021, 5, 31)),

                new Experience(
                    "Syberry CIS",
                    "Software Developer",
                    "Worked on enterprise client projects. Gained experience in full-cycle development, "
                    + "code review, and agile team practices.",
                    new DateOnly(2019, 1, 1),
                    new DateOnly(2020, 2, 28)),

                new Experience(
                    "Softeq",
                    "Junior Developer",
                    "Started professional career. Built foundational skills in software engineering "
                    + "practices and team collaboration.",
                    new DateOnly(2018, 3, 1),
                    new DateOnly(2018, 12, 31)),

                new Experience(
                    "BSUIR",
                    "BSc Engineering",
                    "Belarusian State University of Informatics and Radioelectronics. "
                    + "Foundation in computer science, algorithms, and software engineering.",
                    new DateOnly(2019, 9, 1),
                    new DateOnly(2023, 6, 30)));

            _logger.LogInformation("Seeded experience");
        }

        private async Task SeedSkillsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Skills.AnyAsync(cancellationToken)) return;

            // Evidence replaces arbitrary percentage claims; 0 means no self-rating supplied.
            _context.Skills.AddRange(
                new Skill("C# / .NET 10", "Backend", 0, 0, "JobFinder: durable workflows, typed contracts; Realtime Primitives: concurrency and bounded buffers."),
                new Skill("ASP.NET Core / Wolverine", "Backend", 0, 1, "JobFinder: PostgreSQL transport and transactional outbox; Portfolio: versioned endpoints and rate limiting."),
                new Skill("PostgreSQL / EF Core", "Data", 0, 2, "JobFinder: revision witnesses and reconciliation; Portfolio: isolated database integration tests."),
                new Skill("Redis", "Data", 0, 3, "Portfolio: caching and container-based integration tests."),
                new Skill("Exact probability / Monte Carlo", "Domain", 0, 4, "Slot Math Lab: exact rational interpreter and seeded simulation; TGSlots: game math tests."),
                new Skill("Revit API / WPF / MVVM", "Domain", 0, 5, "AccessRoute and ModelGuard: core/ViewModel tests; Revit host verification pending."),
                new Skill("React / TypeScript / PixiJS", "Frontend", 0, 6, "Portfolio: bilingual UI and 43 tests; TGSlots: PixiJS renderer and modular games."),
                new Skill("Docker / Testcontainers / CI", "DevOps", 0, 7, "JobFinder: real PostgreSQL/process-boundary tests; Portfolio: PostgreSQL + Redis fixtures."),
                new Skill("AI-assisted engineering", "Practices", 0, 8, "Versioned LLM recipes, architecture decisions, test/review workflows and explicit verification boundaries."));

            _logger.LogInformation("Seeded skills");
        }

        private async Task SeedProjectsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Projects.AnyAsync(cancellationToken)) return;

            _context.Projects.AddRange(
                new Project(
                    title: "JobFinder",
                    description: "Durable workflow engineering: revision witnesses, transactional outbox, typed LLM recipes and reconciliation of unknown external outcomes. 939 selected tests verified, including native PostgreSQL/process-restart scenarios.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/jobfinder-showcase",
                    technologies: [".NET 10", "Wolverine", "PostgreSQL", "Blazor", "LLM"],
                    sortOrder: 0,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/jobfinder-showcase/blob/main/docs/PUBLICATION-VERIFICATION.md",
                    domain: ProjectDomain.Fullstack),

                new Project(
                    title: "Slot Math Lab",
                    description: "Exact rational and Monte Carlo interpreters, deterministic random streams and a React graph editor. 704 core tests passed; research prototype with full-CI follow-up still needed.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/slot-math-lab",
                    technologies: [".NET 10", "C#", "React 19", "Probability", "Deterministic RNG"],
                    sortOrder: 1,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/slot-math-lab/blob/main/README.md",
                    domain: ProjectDomain.IGaming),

                new Project(
                    title: "Realtime Primitives",
                    description: "Extracted C# library: circuit breakers, typed HTTP failures, retained audio replay and bounded flight recording. 89 offline tests and public CI.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/dotnet-realtime-primitives",
                    technologies: [".NET 10", "Concurrency", "Streaming", "Resilience"],
                    sortOrder: 2,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/dotnet-realtime-primitives/blob/main/README.md",
                    domain: ProjectDomain.RealTime),

                new Project(
                    title: "AccessRoute for Revit",
                    description: "BVH spatial search, Dijkstra routing, versioned storage and WPF/MVVM. 68 shared core/presentation tests; synthetic indexed queries ~11× faster than full scan. Revit host execution remains unverified.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/access-route-revit",
                    technologies: ["C#", "Revit API", "WPF/MVVM", "Algorithms"],
                    sortOrder: 3,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/access-route-revit/blob/main/docs/evidence/benchmark.json",
                    domain: ProjectDomain.Fullstack),

                new Project(
                    title: "TGSlots",
                    description: "Modular TypeScript slot games, PixiJS rendering and Monte Carlo simulation tooling. 736 tests and lint passed. Bun/Elysia API prototype; simulation-runner typecheck needs follow-up.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/tgslots",
                    technologies: ["TypeScript", "Bun", "Elysia", "PixiJS 8", "React"],
                    sortOrder: 4,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/tgslots/blob/main/README.md",
                    domain: ProjectDomain.IGaming),

                new Project(
                    title: ".NET Engineering Portfolio",
                    description: "This delivery monorepo: versioned content API, PostgreSQL/Redis, isolated Testcontainers integration tests and a bilingual React site. 56 backend + 43 frontend tests passed.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/portfolio",
                    technologies: [".NET 10", "React 19", "PostgreSQL", "Redis", "Testcontainers", "Docker"],
                    sortOrder: 5,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/portfolio/blob/main/docs/adr/0001-architecture-and-stack.md",
                    domain: ProjectDomain.Fullstack),

                new Project(
                    title: "ModelGuard for Revit",
                    description: "Companion BIM tool: parameter contracts, equipment references, transactional mark updates and worksharing ownership. Shares the AccessRoute core/test suite; Revit execution unverified.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/model-guard-revit",
                    technologies: ["C#", "Revit API", "WPF/MVVM", "Transactions"],
                    sortOrder: 6,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/model-guard-revit/blob/main/README.md",
                    domain: ProjectDomain.Fullstack));

            _logger.LogInformation("Seeded projects");
        }
    }
}
