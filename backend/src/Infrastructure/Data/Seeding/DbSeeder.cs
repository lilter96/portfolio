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
                    "Built slot-game backends on the myKonami/Aristocrat platform. Shipped 10+ titles " +
                    "serving 5M+ players. Designed and implemented game logic engines, real-money " +
                    "transaction pipelines, and operator tooling. Deep iGaming domain expertise: " +
                    "RNG certification flows, jurisdictional compliance, and platform integration.",
                    new DateOnly(2022, 9, 1),
                    null),

                new Experience(
                    "Solvintech",
                    "Fullstack Developer (.NET + React)",
                    "Developed and maintained web applications across the full stack. " +
                    "Built REST APIs with ASP.NET Core and interactive UIs with React and TypeScript. " +
                    "Worked on real-time features, database design, and CI/CD pipelines.",
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
                    "Worked on enterprise client projects. Gained experience in full-cycle development, " +
                    "code review, and agile team practices.",
                    new DateOnly(2019, 1, 1),
                    new DateOnly(2020, 2, 28)),

                new Experience(
                    "Softeq",
                    "Junior Developer",
                    "Started professional career working on embedded and desktop applications. " +
                    "Built foundational skills in software engineering practices and team collaboration.",
                    new DateOnly(2018, 3, 1),
                    new DateOnly(2018, 12, 31)));

            _logger.LogInformation("Seeded experience");
        }

        private async Task SeedSkillsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Skills.AnyAsync(cancellationToken)) return;

            _context.Skills.AddRange(
                // Backend
                new Skill("C# / .NET", "Backend", 95, 10),
                new Skill("ASP.NET Core", "Backend", 95, 20),
                new Skill("SignalR", "Backend", 85, 30),
                new Skill("PostgreSQL", "Backend", 90, 40),
                new Skill("Redis", "Backend", 85, 50),
                new Skill("F#", "Backend", 65, 60),

                // Frontend
                new Skill("React", "Frontend", 80, 70),
                new Skill("TypeScript", "Frontend", 82, 80),
                new Skill("PixiJS", "Frontend", 70, 90),

                // Domain
                new Skill("iGaming (Slots)", "Domain", 92, 100),
                new Skill("Provably Fair Systems", "Domain", 88, 110),
                new Skill("Game Math / RNG", "Domain", 85, 120),
                new Skill("Crypto / Blockchain", "Domain", 70, 130),

                // Tools & Practices
                new Skill("Docker", "DevOps", 82, 140),
                new Skill("GitHub Actions", "DevOps", 80, 150),
                new Skill("TON SDK", "Blockchain", 65, 160),
                new Skill("Entity Framework Core", "Backend", 90, 170),
                new Skill("REST / gRPC", "Backend", 85, 180),
                new Skill("TDD", "Practices", 82, 190));

            _logger.LogInformation("Seeded skills");
        }

        private async Task SeedProjectsAsync(CancellationToken cancellationToken)
        {
            if (await _context.Projects.AnyAsync(cancellationToken)) return;

            _context.Projects.AddRange(
                new Project(
                    "SpinTon — Social Casino Telegram Mini App",
                    "Flagship full-stack project: a social-casino experience delivered as a " +
                    "Telegram Mini App. Phaser + React + TypeScript frontend with PixiJS " +
                    "animations. ASP.NET Core + SignalR backend for real-time multiplayer. " +
                    "PostgreSQL + Redis data layer. Integrated TON blockchain for token " +
                    "transactions. Served as the primary demo of full-stack iGaming expertise.",
                    null,
                    null,
                    ["C#", ".NET", "ASP.NET Core", "SignalR", "Phaser", "PixiJS", "React",
                     "TypeScript", "PostgreSQL", "Redis", "TON"],
                    10),

                new Project(
                    "Provably Fair Slot Demo",
                    "The centerpiece of this portfolio: a production-grade slot machine with " +
                    "HMAC-SHA256 provably-fair RNG, authentic reel strips, weighted symbol " +
                    "distributions, multi-line payline evaluation, and a PixiJS-powered " +
                    "reel animation engine. Demonstrates real iGaming math and full-stack range.",
                    null,
                    null,
                    ["C#", ".NET 10", "HMAC-SHA256", "PixiJS", "iGaming Math"],
                    20),

                new Project(
                    "crypto-exchange-rates",
                    "Real-time multi-exchange cryptocurrency price tracking service. " +
                    "Aggregates order-book data via WebSocket + REST from multiple exchanges. " +
                    "Built in C# with ASP.NET Core. Public on GitHub.",
                    "https://github.com/lilter96/crypto-exchange-rates",
                    "https://github.com/lilter96/crypto-exchange-rates",
                    ["C#", ".NET", "WebSocket", "REST", "ASP.NET Core"],
                    30),

                new Project(
                    "Slot Math Engine",
                    "Standalone TypeScript + Bun library implementing professional slot machine " +
                    "mathematics: probability monads for symbol distributions, Walker's Alias " +
                    "Method for O(1) weighted random selection, and a Trie-based payline " +
                    "evaluator for O(k) line scoring. Used as the reference implementation " +
                    "for the provably-fair slot backend.",
                    null,
                    null,
                    ["TypeScript", "Bun", "Probability Theory", "Data Structures"],
                    40),

                new Project(
                    "SMC/ICT Trading Signal Engine",
                    "Algorithmic trading signal generator targeting OKX perpetual futures. " +
                    "Implements Smart Money Concepts (SMC) and Inner Circle Trader (ICT) " +
                    "methodologies. Built in F# and C# for correctness and performance. " +
                    "Integrates with OKX API for live execution.",
                    null,
                    null,
                    ["F#", "C#", "OKX API", "Algorithmic Trading"],
                    50),

                new Project(
                    "realtime_chat",
                    "Real-time messaging backend built with ASP.NET Web API. Demonstrates " +
                    "SignalR hubs, message persistence, and multi-room architecture. " +
                    "Public on GitHub.",
                    "https://github.com/lilter96/realtime_chat",
                    "https://github.com/lilter96/realtime_chat",
                    ["C#", "ASP.NET Core", "SignalR", "WebSockets"],
                    60),

                new Project(
                    "MekashronTest",
                    ".NET 7 / Umbraco CMS integration project. Demonstrates enterprise CMS " +
                    "customization, content modeling, and API integration patterns. " +
                    "Public on GitHub.",
                    "https://github.com/lilter96/MekashronTest",
                    "https://github.com/lilter96/MekashronTest",
                    ["C#", ".NET 7", "Umbraco", "CMS"],
                    70));

            _logger.LogInformation("Seeded projects");
        }
    }
}
