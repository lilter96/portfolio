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

            _context.Skills.AddRange(
                // Backend — all backed by public repos, TGSlots live, or this portfolio
                new Skill("C# / .NET", "Backend", 95, 10,
                    "github.com/lilter96 (multiple public repos)"),
                new Skill("ASP.NET Core", "Backend", 95, 20,
                    "TGSlots (live) + portfolio API + public repos"),
                new Skill("SignalR", "Backend", 85, 30,
                    "TGSlots (live) + github.com/lilter96/realtime_chat"),
                new Skill("PostgreSQL", "Backend", 90, 40,
                    "TGSlots (live) + portfolio (EF Core + Testcontainers)"),
                new Skill("Redis", "Backend", 85, 50,
                    "TGSlots (live) + portfolio (caching layer)"),
                new Skill("F#", "Backend", 60, 60,
                    "SMC/ICT Trading Engine (design artifact, WIP) — familiar"),

                // Frontend — backed by TGSlots + this portfolio
                new Skill("React", "Frontend", 80, 70,
                    "TGSlots (live) + this portfolio + Solvintech sample"),
                new Skill("TypeScript", "Frontend", 82, 80,
                    "TGSlots (live) + this portfolio + Slot Math Library (TS+Bun)"),
                new Skill("PixiJS", "Frontend", 70, 90,
                    "TGSlots (live, Phaser/PixiJS animations)"),

                // Domain — backed by TGSlots + NDA work + design artifacts
                new Skill("iGaming (Slots)", "Domain", 92, 100,
                    "TGSlots (live) + Custom Games Studio (NDA)"),
                new Skill("Provably Fair Systems", "Domain", 88, 110,
                    "Slot Demo (this portfolio, HMAC-SHA256 design)"),
                new Skill("Game Math / RNG", "Domain", 85, 120,
                    "Slot Math Library (TS+Bun, design artifact)"),
                new Skill("Crypto / Blockchain", "Domain", 70, 130,
                    "TGSlots (TON) + SMC/ICT Trading Engine (OKX, design)"),

                // Tools & Practices
                new Skill("Docker", "DevOps", 82, 140,
                    "This portfolio (multi-stage Dockerfiles, docker-compose)"),
                new Skill("GitHub Actions", "DevOps", 80, 150,
                    "This portfolio (5 CI/CD workflows)"),
                new Skill("TON SDK", "Blockchain", 65, 160,
                    "TGSlots (live, TON blockchain integration)"),
                new Skill("Entity Framework Core", "Backend", 90, 170,
                    "This portfolio (EF Core 10 + PostgreSQL)"),
                new Skill("REST / gRPC", "Backend", 85, 180,
                    "Portfolio API + crypto-exchange-rates + multiple public repos"),
                new Skill("TDD", "Practices", 82, 190,
                    "This portfolio (56 tests, xUnit, Testcontainers)"));

            _logger.LogInformation("Seeded skills");
        }

        private async Task SeedProjectsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Projects.AnyAsync(cancellationToken)) return;

            _context.Projects.AddRange(
                // ═══════════════════════════════════════════════════════════
                //  iGaming
                // ═══════════════════════════════════════════════════════════

                new Project(
                    title: "TGSlots — Social Casino Telegram Mini App",
                    description:
                        "A social-casino experience delivered as a Telegram Mini App. Real-time multiplayer "
                        + "with SignalR, background job processing with Hangfire, and TON blockchain integration "
                        + "for token transactions. Live deployment demonstrates full-stack iGaming delivery "
                        + "end-to-end. The broader SpinTon platform extends this with additional game modes "
                        + "and operator features.",
                    url: "https://tgslots-marketing-production.up.railway.app/",
                    sourceUrl: null,
                    technologies: ["C#", "ASP.NET Core", "SignalR", "Hangfire", "Telegram.Bot",
                                   "PostgreSQL", "Redis", "TON", "Phaser", "React", "TypeScript",
                                   "Zustand"],
                    sortOrder: 10,
                    status: ProjectStatus.Live,
                    role: "Solo full-stack developer",
                    evidenceUrl: "https://tgslots-marketing-production.up.railway.app/",
                    domain: ProjectDomain.IGaming),

                new Project(
                    title: "Slot Math Library",
                    description:
                        "Standalone TypeScript + Bun library implementing professional slot machine "
                        + "mathematics. Probability monads for symbol distributions, Walker's Alias Method "
                        + "for O(1) weighted random selection, Trie-based payline evaluator for O(k) line "
                        + "scoring. Monte-Carlo simulation harness for RTP verification.",
                    url: null,
                    sourceUrl: null,
                    technologies: ["TypeScript", "Bun", "Probability Theory", "Data Structures",
                                   "Monte-Carlo Simulation"],
                    sortOrder: 20,
                    status: ProjectStatus.Personal,
                    role: "Solo developer",
                    evidenceUrl: null,
                    domain: ProjectDomain.IGaming),

                new Project(
                    title: "Provably Fair Slot Demo",
                    description:
                        "Production-grade slot machine demonstrating real iGaming math and provably-fair "
                        + "cryptography. HMAC-SHA256 RNG with client-side verification, authentic reel "
                        + "strips with weighted symbol distributions, multi-line payline evaluation, and "
                        + "a PixiJS-powered reel animation engine.",
                    url: null,
                    sourceUrl: null,
                    technologies: ["C#", ".NET 10", "HMAC-SHA256", "PixiJS", "iGaming Math"],
                    sortOrder: 30,
                    status: ProjectStatus.Personal,
                    role: "Solo developer",
                    evidenceUrl: null,
                    domain: ProjectDomain.IGaming),

                // ═══════════════════════════════════════════════════════════
                //  Crypto / Trading
                // ═══════════════════════════════════════════════════════════

                new Project(
                    title: "SMC/ICT Trading Signal Engine",
                    description:
                        "Algorithmic trading signal generator targeting OKX perpetual futures. Implements "
                        + "Smart Money Concepts (SMC) and Inner Circle Trader (ICT) methodologies. Features "
                        + "a liquidity-map module, IMarketFeed abstraction with backtest/live parity, and "
                        + "functional-core / imperative-shell architecture in F# and C#.",
                    url: null,
                    sourceUrl: null,
                    technologies: ["F#", "C#", "OKX API", "Algorithmic Trading", "WebSocket"],
                    sortOrder: 40,
                    status: ProjectStatus.Personal,
                    role: "Solo developer",
                    evidenceUrl: null,
                    domain: ProjectDomain.Crypto),

                new Project(
                    title: "crypto-exchange-rates",
                    description:
                        "Real-time multi-exchange cryptocurrency price tracking service. Aggregates "
                        + "order-book and ticker data via WebSocket and REST from multiple exchanges "
                        + "using the JKorf CryptoExchange.Net library stack. Flagship public code sample "
                        + "demonstrating real-time data pipeline design.",
                    url: "https://github.com/lilter96/crypto-exchange-rates",
                    sourceUrl: "https://github.com/lilter96/crypto-exchange-rates",
                    technologies: ["C#", ".NET 7", "ASP.NET Core", "WebSocket", "REST",
                                   "CryptoExchange.Net"],
                    sortOrder: 50,
                    status: ProjectStatus.OpenSource,
                    role: "Solo developer",
                    evidenceUrl: "https://github.com/lilter96/crypto-exchange-rates",
                    domain: ProjectDomain.Crypto),

                new Project(
                    title: "cryptobot",
                    description: "Cryptocurrency trading bot. Public repository.",
                    url: "https://github.com/lilter96/cryptobot",
                    sourceUrl: "https://github.com/lilter96/cryptobot",
                    technologies: ["C#", ".NET"],
                    sortOrder: 60,
                    status: ProjectStatus.OpenSource,
                    role: "Solo developer",
                    evidenceUrl: "https://github.com/lilter96/cryptobot",
                    domain: ProjectDomain.Crypto),

                new Project(
                    title: "TronRiskAnalyzer",
                    description:
                        "Risk analysis tool for the TRON blockchain ecosystem. Public repository.",
                    url: "https://github.com/lilter96/TronRiskAnalyzer",
                    sourceUrl: "https://github.com/lilter96/TronRiskAnalyzer",
                    technologies: ["C#", ".NET"],
                    sortOrder: 70,
                    status: ProjectStatus.OpenSource,
                    role: "Solo developer",
                    evidenceUrl: "https://github.com/lilter96/TronRiskAnalyzer",
                    domain: ProjectDomain.Crypto),

                // ═══════════════════════════════════════════════════════════
                //  Real-Time
                // ═══════════════════════════════════════════════════════════

                new Project(
                    title: "realtime_chat",
                    description:
                        "Real-time messaging backend demonstrating SignalR hubs, message persistence, "
                        + "and multi-room architecture. Built with ASP.NET Web API.",
                    url: "https://github.com/lilter96/realtime_chat",
                    sourceUrl: "https://github.com/lilter96/realtime_chat",
                    technologies: ["C#", "ASP.NET Core", "SignalR", "WebSockets"],
                    sortOrder: 80,
                    status: ProjectStatus.OpenSource,
                    role: "Solo developer",
                    evidenceUrl: "https://github.com/lilter96/realtime_chat",
                    domain: ProjectDomain.RealTime),

                // ═══════════════════════════════════════════════════════════
                //  Full-Stack / Other
                // ═══════════════════════════════════════════════════════════

                new Project(
                    title: "Solvintech Commercial Sample",
                    description:
                        "Sample full-stack application demonstrating ASP.NET Core backend with "
                        + "React/TypeScript frontend. Built during commercial tenure at Solvintech.",
                    url: "https://github.com/lilter96/Solvintech",
                    sourceUrl: "https://github.com/lilter96/Solvintech",
                    technologies: ["C#", "ASP.NET Core", "React", "TypeScript"],
                    sortOrder: 90,
                    status: ProjectStatus.OpenSource,
                    role: "Fullstack Developer",
                    evidenceUrl: "https://github.com/lilter96/Solvintech",
                    domain: ProjectDomain.Fullstack),

                new Project(
                    title: "MekashronTest",
                    description:
                        ".NET 7 / Umbraco CMS integration project. Demonstrates enterprise CMS "
                        + "customization, content modeling, and API integration patterns.",
                    url: "https://github.com/lilter96/MekashronTest",
                    sourceUrl: "https://github.com/lilter96/MekashronTest",
                    technologies: ["C#", ".NET 7", "Umbraco", "CMS"],
                    sortOrder: 100,
                    status: ProjectStatus.OpenSource,
                    role: "Solo developer",
                    evidenceUrl: "https://github.com/lilter96/MekashronTest",
                    domain: ProjectDomain.Fullstack),

                // ═══════════════════════════════════════════════════════════
                //  Work — under NDA
                // ═══════════════════════════════════════════════════════════

                new Project(
                    title: "Casino/Slot Backend Platform (Custom Games Studio)",
                    description:
                        "Multi-title casino backend platform on the myKonami/Aristocrat stack. Designed "
                        + "and built game logic engines, real-money transaction pipelines, and operator "
                        + "tooling. Integrated RNG certification flows and jurisdictional compliance "
                        + "requirements. Real-time jackpot system with SignalR for live operator dashboards. "
                        + "All specific metrics, title counts, and player numbers are under NDA.",
                    url: null,
                    sourceUrl: null,
                    technologies: ["C#", ".NET", "ASP.NET Core", "myKonami", "Aristocrat",
                                   "PostgreSQL", "Redis", "SignalR"],
                    sortOrder: 110,
                    status: ProjectStatus.WorkNda,
                    role: "Senior .NET Backend Developer",
                    evidenceUrl: null,
                    domain: ProjectDomain.IGaming));

            _logger.LogInformation("Seeded projects");
        }
    }
}
