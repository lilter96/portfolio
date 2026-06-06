# Portfolio Projects — Canonical Catalog

> **Content integrity rules apply.** Every project has a status label, a verifiable link
> (repo or live URL) where one exists, the role played, and the stack. NDA work is described
> by architecture and scope only — no invented metrics, no client names, no revenue figures.
>
> See also: [[content-integrity-rules]], [[positioning]], [[portfolio-owner-dossier]]

## Status labels

| Label | Meaning |
|-------|---------|
| **Live** | Deployed and clickable |
| **Personal / In development** | Real but not shipped — specs, MVPs, WIP |
| **Work — under NDA** | Production work; described by architecture, scope, and role only |
| **Open source** | Public repo, linked |

---

## iGaming

### TGSlots — Social Casino Telegram Mini App
- **Status:** Live (TGSlots) + Personal / In development (broader SpinTon platform)
- **Role:** Solo full-stack developer
- **Stack:** ASP.NET Core + SignalR + Hangfire + Telegram.Bot; PostgreSQL + Redis; TON blockchain; Phaser + React + Zustand
- **Evidence:** [tgslots-marketing-production.up.railway.app](https://tgslots-marketing-production.up.railway.app/)
- **Description:** A social-casino experience delivered as a Telegram Mini App. Real-time multiplayer with SignalR, background job processing with Hangfire, and TON blockchain integration for token transactions. The live deployment demonstrates full-stack iGaming delivery end-to-end. The broader SpinTon platform (Personal / In development) extends this with additional game modes and operator features.

### Slot Math Library
- **Status:** Personal / In development
- **Role:** Solo developer
- **Stack:** TypeScript + Bun; probability monads; Walker's Alias Method; Trie-based payline evaluator
- **Evidence:** Design artifact — no public repo yet
- **Description:** Standalone library implementing professional slot machine mathematics. Probability monads for symbol distributions, Walker's Alias Method for O(1) weighted random selection, and a Trie-based payline evaluator for O(k) line scoring. Monte-Carlo simulation harness for RTP verification. Built as the reference math engine for the provably-fair slot demo.

### Provably Fair Slot Demo
- **Status:** Personal / In development
- **Role:** Solo developer
- **Stack:** C# + .NET 10; HMAC-SHA256 provably-fair RNG; PixiJS reel renderer; authentic reel strips; multi-line payline evaluation
- **Evidence:** This portfolio (design + WIP)
- **Description:** The centerpiece of this portfolio. A production-grade slot machine demonstrating real iGaming math and provably-fair cryptography. HMAC-SHA256 RNG with client-side verification, authentic reel strips with weighted symbol distributions, multi-line payline evaluation, and a PixiJS-powered reel animation engine.

---

## Crypto / Trading

### SMC/ICT Trading Signal Engine
- **Status:** Personal / In development
- **Role:** Solo developer
- **Stack:** F# + C#; functional-core / imperative-shell architecture; OKX perpetuals API; WebSocket streaming
- **Evidence:** Design artifact — no public repo yet
- **Description:** Algorithmic trading signal generator targeting OKX perpetual futures. Implements Smart Money Concepts (SMC) and Inner Circle Trader (ICT) methodologies. Features a liquidity-map module for market-structure analysis, IMarketFeed abstraction with parity between backtesting and live modes, and a functional-core / imperative-shell architecture for correctness and testability.

### crypto-exchange-rates
- **Status:** Open source
- **Role:** Solo developer
- **Stack:** .NET 7; ASP.NET Core; JKorf CryptoExchange.Net library; WebSocket + REST
- **Evidence:** [github.com/lilter96/crypto-exchange-rates](https://github.com/lilter96/crypto-exchange-rates) — ⭐ 1
- **Description:** Real-time multi-exchange cryptocurrency price tracking service. Aggregates order-book and ticker data via WebSocket and REST from multiple exchanges using the JKorf CryptoExchange.Net library stack. Built in C# with ASP.NET Core. Flagship public code sample demonstrating real-time data pipeline design.

### cryptobot
- **Status:** Open source
- **Role:** Solo developer
- **Stack:** Details TBD — public repo
- **Evidence:** [github.com/lilter96/cryptobot](https://github.com/lilter96/cryptobot)

### TronRiskAnalyzer
- **Status:** Open source
- **Role:** Solo developer
- **Stack:** Details TBD — public repo
- **Evidence:** [github.com/lilter96/TronRiskAnalyzer](https://github.com/lilter96/TronRiskAnalyzer)

---

## Real-Time

### realtime_chat
- **Status:** Open source
- **Role:** Solo developer
- **Stack:** C#; ASP.NET Web API; SignalR; WebSockets
- **Evidence:** [github.com/lilter96/realtime_chat](https://github.com/lilter96/realtime_chat)
- **Description:** Real-time messaging backend demonstrating SignalR hubs, message persistence, and multi-room architecture. Built with ASP.NET Web API. Public on GitHub.

---

## Full-Stack / Other

### Solvintech Commercial Work
- **Status:** Open source (sample)
- **Role:** Fullstack Developer
- **Stack:** ASP.NET Core + React + TypeScript
- **Evidence:** [github.com/lilter96/Solvintech](https://github.com/lilter96/Solvintech)
- **Description:** Sample application demonstrating full-stack development with ASP.NET Core backend and React/TypeScript frontend. Built during tenure at Solvintech as part of commercial web application delivery.

### MekashronTest
- **Status:** Open source
- **Role:** Solo developer
- **Stack:** .NET 7; Umbraco CMS; C#
- **Evidence:** [github.com/lilter96/MekashronTest](https://github.com/lilter96/MekashronTest) — ⭐ 3
- **Description:** .NET 7 / Umbraco CMS integration project. Demonstrates enterprise CMS customization, content modeling, and API integration patterns.

### Custom Games Studio — Casino/Slot Backend Platform
- **Status:** Work — under NDA
- **Role:** Senior .NET Backend Developer (2022–present)
- **Stack:** C# / .NET; ASP.NET Core; myKonami / Aristocrat platform; PostgreSQL; Redis; SignalR
- **Evidence:** NDA — no public code or URLs. Described by architecture and scope only.
- **Description:** Backend development for casino slot games on the myKonami/Aristocrat platform. Designed and built game logic engines, real-money transaction pipelines, and operator tooling for multi-title casino backend platform. Integrated RNG certification flows and jurisdictional compliance requirements. Real-time jackpot system with SignalR for live operator dashboards. **No title counts, player numbers, or revenue metrics are claimed — all specific figures are under NDA.**
