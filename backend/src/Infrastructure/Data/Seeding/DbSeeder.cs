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
            _context.Users.Add(new User("lilter96dotnet@gmail.com", "Terentiy Gatsukov"));
            _logger.LogInformation("Seeded users");
        }

        private async Task SeedExperienceAsync(CancellationToken cancellationToken)
        {
            if (await _context.Experiences.AnyAsync(cancellationToken)) return;

            _context.Experiences.AddRange(
                new Experience("Custom Games Studio", ".NET Developer", "Game backend platform for myKONAMI: 5M+ users. Full game backends, shared mathematics and simulation infrastructure.", new DateOnly(2023, 11, 1), new DateOnly(2026, 7, 1)),

                new Experience("Solvintech", "C# / .NET Developer", "MPsklad marketplace SaaS: Ozon, Wildberries, Yandex Market and MoySklad; 50k+ orders/day.", new DateOnly(2022, 7, 1), new DateOnly(2023, 10, 1)),

                new Experience("Elgrow", "C# / .NET Developer", "Smart parking for Domodedovo Airport: 1,000+ spaces and 10,000+ users. Booking, payments and equipment integration.", new DateOnly(2021, 1, 1), new DateOnly(2022, 6, 1)));

            _logger.LogInformation("Seeded experience");
        }

        private async Task SeedSkillsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Skills.AnyAsync(cancellationToken)) return;

            // Evidence replaces arbitrary percentage claims; 0 means no self-rating supplied.
            _context.Skills.AddRange(
                new Skill("C# / .NET / ASP.NET Core", "Backend", 0, 0, "Production backend engineering across gaming, e-commerce and transportation."),
                new Skill("Distributed Systems / CQRS / Outbox / Inbox", "Backend", 0, 1, "Transactional consistency, idempotency, asynchronous delivery and separate read/write models."),
                new Skill("REST / gRPC / WebSockets / SignalR", "Backend", 0, 2, "Service contracts, mobile APIs and real-time delivery."),
                new Skill("PostgreSQL / MS SQL Server / EF Core / Dapper", "Data", 0, 3, "Order consistency, conditional financial updates and transactional persistence."),
                new Skill("Couchbase / MongoDB / Redis", "Data", 0, 4, "Distributed profile state, read models, coordination, rate limits and caching."),
                new Skill("Kafka / RabbitMQ / MassTransit / Hangfire", "Messaging", 0, 5, "Ordered events, background marketplace integrations and equipment isolation."),
                new Skill("C# / F# game mathematics", "Game engineering", 0, 6, "Probability monads, trampolines, exact enumeration, unbiased RNG, alias method and segment trees."),
                new Skill("RTP / volatility / Monte Carlo", "Game engineering", 0, 7, "1B+ spin simulations, confidence intervals, distribution checks, checkpoints and deterministic replay."),
                new Skill("Span<T> / ArrayPool<T> / Roslyn", "Performance", 0, 8, "Allocation/GC optimization, readonly structs and compile-time serialization."),
                new Skill("React / TypeScript / MobX", "Frontend", 0, 9, "Commercial frontend delivery and generated C# / TypeScript contracts."),
                new Skill("Docker / Kubernetes / Linux / CI/CD", "Infrastructure", 0, 10, "Containerized delivery, development environments and integration verification."),
                new Skill("OpenTelemetry / Prometheus / Grafana", "Infrastructure", 0, 11, "Distributed-system observability and production incident analysis."),
                new Skill("xUnit / Testcontainers / AI-assisted development", "Practices", 0, 12, "Behavioral and integration tests; Claude, Cursor and Copilot for implementation, refactoring and test generation."));

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
                    description: "Modular TypeScript slot games, PixiJS rendering and Monte Carlo simulation tooling. 736 tests, all workspace typechecks and lint passed. Bun/Elysia API uses prototype in-memory state.",
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
                    domain: ProjectDomain.Fullstack),

                new Project(
                    title: "Trading Systems Lab",
                    description: "C#/.NET and Go execution boundaries: protobuf/gRPC contracts, order state, PostgreSQL audit immutability and an independent emergency watchdog. 151 .NET tests passed, one optional benchmark skipped; Go watchdog tests passed. Live exchange behavior unverified.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/trading-systems-lab",
                    technologies: ["C#", "Go", "gRPC", "PostgreSQL", "Resilience"],
                    sortOrder: 7,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/trading-systems-lab/blob/main/README.md",
                    domain: ProjectDomain.RealTime),

                new Project(
                    title: "Signal Processing Lab",
                    description: "Python asynchronous signal processing with typed LLM/media parsing, SQLite campaigns, dry-run execution and risk invariants. 340 offline tests passed. Sanitized research snapshot; live providers and screener acceptance unverified.",
                    url: null,
                    sourceUrl: "https://github.com/lilter96/signal-processing-lab",
                    technologies: ["Python", "Asyncio", "LLM", "SQLite", "Testing"],
                    sortOrder: 8,
                    status: ProjectStatus.OpenSource,
                    role: "Personal engineering project",
                    evidenceUrl: "https://github.com/lilter96/signal-processing-lab/blob/main/README.md",
                    domain: ProjectDomain.Fullstack));

            _logger.LogInformation("Seeded projects");
        }
    }
}
