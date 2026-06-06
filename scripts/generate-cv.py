#!/usr/bin/env python3
"""
CV PDF Generator — EN + RU
Generates polished CV PDFs that exactly match the portfolio site content.
Content Integrity: No fabricated metrics, every claim matches the live site.
Fonts: Helvetica (EN, built-in) + Arial (RU, registered from system for Cyrillic).
"""

from reportlab.lib.pagesizes import A4
from reportlab.lib.units import mm
from reportlab.lib.colors import HexColor, white
from reportlab.lib.styles import ParagraphStyle
from reportlab.lib.enums import TA_CENTER
from reportlab.platypus import (
    SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle, HRFlowable
)
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
import os

OUTPUT_DIR = os.path.join(os.path.dirname(__file__), "..", "frontend", "public", "cv")
os.makedirs(OUTPUT_DIR, exist_ok=True)

# ── Register Arial for Cyrillic (RU CV) ─────────────────────────
_ARIAL = "/System/Library/Fonts/Supplemental/Arial.ttf"
_ARIAL_B = "/System/Library/Fonts/Supplemental/Arial Bold.ttf"
_ARIAL_I = "/System/Library/Fonts/Supplemental/Arial Italic.ttf"
_ARIAL_BI = "/System/Library/Fonts/Supplemental/Arial Bold Italic.ttf"
for _p in [_ARIAL, _ARIAL_B, _ARIAL_I, _ARIAL_BI]:
    if not os.path.exists(_p):
        raise FileNotFoundError(f"Font not found: {_p}")
pdfmetrics.registerFont(TTFont("ArialCV", _ARIAL))
pdfmetrics.registerFont(TTFont("ArialCV-Bold", _ARIAL_B))
pdfmetrics.registerFont(TTFont("ArialCV-Italic", _ARIAL_I))
pdfmetrics.registerFont(TTFont("ArialCV-BI", _ARIAL_BI))

# ── Font selection per locale ────────────────────────────────────
# EN uses Helvetica (built-in, clean). RU uses Arial (Cyrillic-capable).
FONTS = {
    "en": {"body": "Helvetica", "bold": "Helvetica-Bold",
           "italic": "Helvetica-Oblique", "bi": "Helvetica-BoldOblique"},
    "ru": {"body": "ArialCV", "bold": "ArialCV-Bold",
           "italic": "ArialCV-Italic", "bi": "ArialCV-BI"},
}

# ── Colors ──────────────────────────────────────────────────────
DARK_BG    = HexColor("#0a0e14")
NEON_GREEN = HexColor("#39ff14")
AMBER      = HexColor("#ffb000")
RED        = HexColor("#dc2626")
BLUE       = HexColor("#2563eb")
DARK_TEXT   = HexColor("#1a1a2e")
MED_TEXT    = HexColor("#444444")
LIGHT_TEXT  = HexColor("#888888")
GRAY_LINE   = HexColor("#cccccc")
WHITE       = white


def green_hr():
    return HRFlowable(width="100%", thickness=0.5, color=NEON_GREEN, spaceBefore=1, spaceAfter=4)


# ══════════════════════════════════════════════════════════════════
#  CONTENT — exactly matches the portfolio site
# ══════════════════════════════════════════════════════════════════

EN = {
    "name": "Terentiy Gatsukov",
    "title": "Senior .NET Backend Developer",
    "tagline": "iGaming × .NET Backend × Real-Time Systems",
    "contacts": "Minsk, Belarus  ·  terentiy.gatsukov@gmail.com  ·  github.com/lilter96  ·  linkedin.com/in/terentiy-gatsukov-048694224",
    "summary": (
        "Backend engineer working at the intersection of iGaming, .NET, and real-time systems. "
        "I build slot-game backends, provably-fair RNG engines, real-money transaction pipelines, "
        "and crypto trading infrastructure — code that processes real money, powers live games, "
        "and runs under regulatory scrutiny. Full-stack capable with React/TypeScript. "
        "Strong advocate for TDD, clean architecture, and verifiable delivery."
    ),
    "domain": [
        "Slot-game backend development on the myKonami / Aristocrat platform",
        "RNG certification flows &amp; jurisdictional compliance",
        "Real-money transaction pipelines &amp; player wallet systems",
        "Provably-fair systems (HMAC-SHA256) &amp; game math engines",
        "Platform integration &amp; operator tooling",
        "Full-stack delivery: .NET backend + React/TypeScript frontend",
        "Blockchain tooling: TON SDK, OKX perpetuals trading signals",
    ],
    "experience": [
        ("Senior .NET Backend Developer", "Custom Games Studio",
         "Sep 2022 — Present",
         "Backend development for casino slot games on the myKonami/Aristocrat platform. "
         "Designed and built game logic engines, real-money transaction pipelines, and "
         "operator tooling for a multi-title casino backend platform. Integrated RNG "
         "certification flows and jurisdictional compliance requirements."),
        ("Fullstack Developer (.NET + React)", "Solvintech",
         "Jun 2021 — Aug 2022",
         "Developed and maintained web applications across the full stack. Built REST APIs "
         "with ASP.NET Core and interactive UIs with React and TypeScript. Worked on "
         "real-time features, database design, and CI/CD pipelines."),
        ("Software Developer", "Elgrow",
         "Mar 2020 — May 2021",
         "Contributed to commercial software projects using .NET and related technologies."),
        ("Software Developer", "Syberry CIS",
         "Jan 2019 — Feb 2020",
         "Worked on enterprise client projects. Gained experience in full-cycle development, "
         "code review, and agile team practices."),
        ("Junior Developer", "Softeq",
         "Mar 2018 — Dec 2018",
         "Started professional career. Built foundational skills in software engineering "
         "practices and team collaboration."),
    ],
    "education": (
        "<b>BSc Engineering</b> — Belarusian State University of Informatics and "
        "Radioelectronics (BSUIR), 2019–2023"
    ),
    "languages": "<b>English</b> — Fluent  ·  <b>Russian</b> — Native",
    "skills": [
        ("Backend", "C# / .NET, ASP.NET Core, SignalR, Entity Framework Core, F#"),
        ("Data", "PostgreSQL, Redis"),
        ("Frontend", "React, TypeScript, PixiJS, Phaser"),
        ("Domain", "iGaming (Slots), Provably Fair Systems, Game Math / RNG, Crypto / Blockchain"),
        ("Blockchain", "TON SDK, OKX API"),
        ("DevOps", "Docker, GitHub Actions, CI/CD"),
        ("Practices", "TDD, Clean Architecture, REST / gRPC"),
    ],
    "projects": [
        ("TGSlots — Social Casino Telegram Mini App", "Live",
         "C#, ASP.NET Core, SignalR, PostgreSQL, Redis, TON, React, TypeScript, Phaser",
         "Full-stack social-casino Telegram Mini App. Real-time multiplayer with SignalR, "
         "Hangfire job processing, and TON blockchain integration for token transactions. "
         "Live at tgslots-marketing-production.up.railway.app"),
        ("Slot Math Library", "Personal",
         "TypeScript, Bun, Probability Theory, Monte-Carlo Simulation",
         "Standalone TS + Bun library implementing professional slot mathematics: "
         "probability monads, Walker's Alias Method, Trie-based payline evaluator, "
         "Monte-Carlo RTP verification harness."),
        ("Provably Fair Slot Demo", "Personal",
         "C#, .NET 10, HMAC-SHA256, PixiJS, iGaming Math",
         "Production-grade slot machine with HMAC-SHA256 provably-fair RNG, client-side "
         "verification, authentic weighted reel strips, and multi-line payline evaluation."),
        ("SMC/ICT Trading Signal Engine", "Personal",
         "F#, C#, OKX API, WebSocket, Algorithmic Trading",
         "Algorithmic trading signal generator for OKX perpetual futures. Smart Money "
         "Concepts and ICT methodology with functional-core / imperative-shell architecture."),
        ("crypto-exchange-rates", "Open Source",
         "C#, .NET 7, ASP.NET Core, WebSocket, REST, CryptoExchange.Net",
         "Real-time multi-exchange cryptocurrency price tracking. Aggregates order-book "
         "and ticker data via WebSocket/REST. github.com/lilter96/crypto-exchange-rates"),
        ("Casino/Slot Backend Platform", "NDA",
         "C#, .NET, myKonami, Aristocrat, PostgreSQL, Redis, SignalR",
         "Multi-title casino backend platform on the myKonami/Aristocrat stack. Game logic "
         "engines, real-money transaction pipelines, operator tooling, real-time jackpot "
         "system. Custom Games Studio — specific metrics under NDA."),
    ],
    "footer": "Portfolio site · Every claim links to evidence — live URL, public repo, or honestly-framed NDA description.",
}

RU = {
    "name": "Терентий Гацуков",
    "title": "Senior .NET Backend Developer",
    "tagline": "iGaming × .NET Backend × Real-Time системы",
    "contacts": "Минск, Беларусь  ·  terentiy.gatsukov@gmail.com  ·  github.com/lilter96  ·  linkedin.com/in/terentiy-gatsukov-048694224",
    "summary": (
        "Бэкенд-инженер на пересечении iGaming, .NET и real-time систем. Я создаю бэкенды "
        "слотовых игр, доказуемо честные ГСЧ, платёжные пайплайны и крипто-трейдинговую "
        "инфраструктуру — код, который обрабатывает реальные деньги, работает в live-играх "
        "и проходит регуляторные проверки. Full-stack capable с React/TypeScript. "
        "Строгий приверженец TDD, чистой архитектуры и проверяемой доставки."
    ),
    "domain": [
        "Разработка бэкендов слотов на платформе myKonami / Aristocrat",
        "Процессы сертификации ГСЧ и соблюдение юрисдикционных требований",
        "Пайплайны real-money транзакций и системы игровых кошельков",
        "Доказуемо честные системы (HMAC-SHA256) и игровая математика",
        "Интеграция платформ и инструменты для операторов",
        "Full-stack доставка: .NET бэкенд + React/TypeScript фронтенд",
        "Блокчейн-инструменты: TON SDK, торговые сигналы OKX perpetuals",
    ],
    "experience": [
        ("Senior .NET Backend Developer", "Custom Games Studio",
         "Сен 2022 — Наст. время",
         "Разработка бэкендов слотов на платформе myKonami/Aristocrat. Проектирование "
         "игровых движков, платёжных пайплайнов и инструментов для операторов на "
         "мульти-тайтловой казино-платформе. Интеграция процессов сертификации ГСЧ "
         "и соблюдение юрисдикционных требований."),
        ("Fullstack Developer (.NET + React)", "Solvintech",
         "Июн 2021 — Авг 2022",
         "Full-stack разработка приложений на ASP.NET Core и React. Создание REST API, "
         "интеграция сторонних сервисов и доставка end-to-end функциональности в agile-команде."),
        ("Software Developer", "Elgrow",
         "Мар 2020 — Май 2021",
         "Разработка и поддержка веб-приложений на .NET. Проектирование баз данных, "
         "разработка API и реализация пользовательских интерфейсов для enterprise-клиентов."),
        ("Software Developer", "Syberry CIS",
         "Янв 2019 — Фев 2020",
         "Разработка enterprise-проектов на .NET и смежных технологиях. Участие в "
         "code review, тестировании и пайплайнах развёртывания."),
        ("Junior Developer", "Softeq",
         "Мар 2018 — Дек 2018",
         "Начало профессиональной карьеры — разработка программных компонентов на .NET. "
         "Освоение чистого кода, контроля версий и agile-процессов."),
    ],
    "education": (
        "<b>BSc Engineering</b> — Белорусский государственный университет информатики "
        "и радиоэлектроники (БГУИР), 2019–2023"
    ),
    "languages": "<b>Английский</b> — Свободно  ·  <b>Русский</b> — Родной",
    "skills": [
        ("Бэкенд", "C# / .NET, ASP.NET Core, SignalR, Entity Framework Core, F#"),
        ("Данные", "PostgreSQL, Redis"),
        ("Фронтенд", "React, TypeScript, PixiJS, Phaser"),
        ("Домен", "iGaming (Слоты), Provably Fair, Игровая математика / ГСЧ, Крипто / Блокчейн"),
        ("Блокчейн", "TON SDK, OKX API"),
        ("DevOps", "Docker, GitHub Actions, CI/CD"),
        ("Методологии", "TDD, Clean Architecture, REST / gRPC"),
    ],
    "projects": [
        ("TGSlots — Social Casino Telegram Mini App", "Live",
         "C#, ASP.NET Core, SignalR, PostgreSQL, Redis, TON, React, TypeScript, Phaser",
         "Full-stack social-casino Telegram Mini App. Real-time мультиплеер на SignalR, "
         "фоновая обработка Hangfire, интеграция с TON блокчейном. Доступен на "
         "tgslots-marketing-production.up.railway.app"),
        ("Slot Math Library", "Personal",
         "TypeScript, Bun, Теория вероятностей, Монте-Карло",
         "Автономная библиотека на TypeScript + Bun с профессиональной слотовой математикой: "
         "вероятностные монады, метод алиасов Уокера, Trie-based оценщик выплат, "
         "Монте-Карло верификация RTP."),
        ("Provably Fair Slot Demo", "Personal",
         "C#, .NET 10, HMAC-SHA256, PixiJS, iGaming Math",
         "Слот-машина промышленного уровня с доказуемо честным ГСЧ на HMAC-SHA256, "
         "клиентской верификацией, аутентичными взвешенными барабанами и оценкой выплат "
         "по множеству линий."),
        ("SMC/ICT Trading Signal Engine", "Personal",
         "F#, C#, OKX API, WebSocket, Алготрейдинг",
         "Генератор алгоритмических торговых сигналов для OKX perpetual futures. "
         "Методологии Smart Money Concepts и ICT, архитектура functional-core / imperative-shell."),
        ("crypto-exchange-rates", "Open Source",
         "C#, .NET 7, ASP.NET Core, WebSocket, REST, CryptoExchange.Net",
         "Сервис отслеживания цен криптовалют с множества бирж в реальном времени. "
         "Агрегация ордер-буков и тикеров через WebSocket/REST. "
         "github.com/lilter96/crypto-exchange-rates"),
        ("Casino/Slot Backend Platform", "NDA",
         "C#, .NET, myKonami, Aristocrat, PostgreSQL, Redis, SignalR",
         "Мульти-тайтловая казино-платформа на стеке myKonami/Aristocrat. Игровые движки, "
         "real-money платёжные пайплайны, инструменты оператора, real-time джекпот. "
         "Custom Games Studio — конкретные метрики под NDA."),
    ],
    "footer": "Сайт-портфолио · Каждое утверждение подкреплено ссылкой — live URL, публичный репозиторий или честное NDA-описание.",
}


# ══════════════════════════════════════════════════════════════════
#  Builder
# ══════════════════════════════════════════════════════════════════

def build_cv(content, output_path, f):
    """Build a single CV PDF. `f` is the font dict for this locale."""
    doc = SimpleDocTemplate(
        output_path, pagesize=A4,
        leftMargin=14*mm, rightMargin=14*mm,
        topMargin=12*mm, bottomMargin=14*mm,
        title=f"{content['name']} — CV",
        author="Terentiy Gatsukov",
    )

    # ── Styles using locale-specific fonts ──
    S = []
    ff = f  # shorthand

    sBody = ParagraphStyle("sBody", fontName=ff["body"], fontSize=9.5, leading=13,
                           textColor=DARK_TEXT, spaceAfter=3)
    sSection = ParagraphStyle("sSection", fontName=ff["bold"], fontSize=12, leading=15,
                              textColor=DARK_TEXT, spaceBefore=11, spaceAfter=2)
    sRole = ParagraphStyle("sRole", fontName=ff["bold"], fontSize=10.5, leading=14,
                           textColor=DARK_TEXT, spaceAfter=0)
    sDate = ParagraphStyle("sDate", fontName=ff["body"], fontSize=8.5, leading=11,
                           textColor=LIGHT_TEXT, spaceAfter=1)
    sDesc = ParagraphStyle("sDesc", fontName=ff["body"], fontSize=9, leading=12.5,
                           textColor=DARK_TEXT, spaceAfter=3)
    sProjTitle = ParagraphStyle("sProjTitle", fontName=ff["bold"], fontSize=9.5, leading=13,
                                textColor=DARK_TEXT, spaceAfter=0)
    sProjDesc = ParagraphStyle("sProjDesc", fontName=ff["body"], fontSize=8.5, leading=11.5,
                               textColor=MED_TEXT, spaceAfter=1)
    sProjTech = ParagraphStyle("sProjTech", fontName=ff["body"], fontSize=7.5, leading=10,
                               textColor=LIGHT_TEXT, spaceAfter=2)
    sSkillItem = ParagraphStyle("sSkillItem", fontName=ff["body"], fontSize=8, leading=10,
                                textColor=MED_TEXT, spaceAfter=0)
    sFooter = ParagraphStyle("sFooter", fontName=ff["body"], fontSize=6.5, leading=9,
                             textColor=LIGHT_TEXT, alignment=TA_CENTER)

    sLive = ParagraphStyle("sLive", fontName=ff["bold"], fontSize=7.5, leading=10, textColor=HexColor("#0d7c0d"))
    sPersonal = ParagraphStyle("sPersonal", fontName=ff["bold"], fontSize=7.5, leading=10, textColor=AMBER)
    sOS = ParagraphStyle("sOS", fontName=ff["bold"], fontSize=7.5, leading=10, textColor=BLUE)
    sNDA = ParagraphStyle("sNDA", fontName=ff["bold"], fontSize=7.5, leading=10, textColor=RED)
    STATUS_MAP = {"Live": sLive, "Personal": sPersonal, "Open Source": sOS, "NDA": sNDA}

    # ── Header ──
    header_data = [[
        Table([
            [Paragraph(content["name"], ParagraphStyle("hn", fontName=ff["bold"],
                        fontSize=20, leading=24, textColor=WHITE))],
            [Paragraph(content["tagline"], ParagraphStyle("ht", fontName=ff["body"],
                        fontSize=9.5, leading=12, textColor=NEON_GREEN))],
            [Spacer(1, 2)],
            [Paragraph(content["contacts"], ParagraphStyle("hc", fontName=ff["body"],
                        fontSize=7.5, leading=10, textColor=HexColor("#b0b0b0")))],
        ], colWidths=[180*mm])
    ]]
    ht = Table(header_data, colWidths=[192*mm])
    ht.setStyle(TableStyle([
        ("BACKGROUND", (0, 0), (-1, -1), DARK_BG),
        ("LEFTPADDING", (0, 0), (-1, -1), 10*mm),
        ("RIGHTPADDING", (0, 0), (-1, -1), 10*mm),
        ("TOPPADDING", (0, 0), (-1, -1), 6*mm),
        ("BOTTOMPADDING", (0, 0), (-1, -1), 5*mm),
    ]))
    S.append(ht)
    S.append(Spacer(1, 4*mm))

    # ── Professional Summary ──
    S.append(Paragraph("PROFESSIONAL SUMMARY" if ff["body"] == "Helvetica" else "О СЕБЕ", sSection))
    S.append(green_hr())
    S.append(Paragraph(content["summary"], sBody))

    # ── Domain Expertise ──
    label_de = "DOMAIN EXPERTISE" if ff["body"] == "Helvetica" else "ДОМЕННАЯ ЭКСПЕРТИЗА"
    S.append(Paragraph(label_de, sSection))
    S.append(green_hr())
    for item in content["domain"]:
        S.append(Paragraph(f"•  {item}", sDesc))

    # ── Experience ──
    label_exp = "EXPERIENCE" if ff["body"] == "Helvetica" else "ОПЫТ РАБОТЫ"
    S.append(Paragraph(label_exp, sSection))
    S.append(green_hr())
    for role, company, dates, desc in content["experience"]:
        exp_header = [[
            Paragraph(role, sRole),
            Paragraph(f"<i>{company}</i>  ·  {dates}", sDate)
        ]]
        et = Table(exp_header, colWidths=[92*mm, 88*mm])
        et.setStyle(TableStyle([
            ("VALIGN", (0, 0), (-1, -1), "BOTTOM"),
            ("LEFTPADDING", (0, 0), (-1, -1), 0),
            ("RIGHTPADDING", (0, 0), (-1, -1), 0),
            ("ALIGN", (1, 0), (1, 0), "RIGHT"),
        ]))
        S.append(et)
        S.append(Paragraph(desc, sDesc))
        S.append(Spacer(1, 1))

    # ── Education + Languages ──
    label_ed = "EDUCATION & LANGUAGES" if ff["body"] == "Helvetica" else "ОБРАЗОВАНИЕ И ЯЗЫКИ"
    S.append(Paragraph(label_ed, sSection))
    S.append(green_hr())
    S.append(Paragraph(content["education"], sDesc))
    S.append(Paragraph(content["languages"], sDesc))

    # ── Technical Skills ──
    label_sk = "TECHNICAL SKILLS" if ff["body"] == "Helvetica" else "ТЕХНИЧЕСКИЕ НАВЫКИ"
    S.append(Paragraph(label_sk, sSection))
    S.append(green_hr())
    for cat, items in content["skills"]:
        S.append(Paragraph(f"<b>{cat}:</b>  {items}", sSkillItem))

    # ── Flagship Projects ──
    label_fp = "FLAGSHIP PROJECTS" if ff["body"] == "Helvetica" else "КЛЮЧЕВЫЕ ПРОЕКТЫ"
    S.append(Paragraph(label_fp, sSection))
    S.append(green_hr())
    for title, status, techs, desc in content["projects"]:
        ss = STATUS_MAP.get(status, sPersonal)
        proj_header = [[
            Paragraph(title, sProjTitle),
            Paragraph(f"● {status}", ss),
        ]]
        pt = Table(proj_header, colWidths=[148*mm, 32*mm])
        pt.setStyle(TableStyle([
            ("VALIGN", (0, 0), (-1, -1), "BOTTOM"),
            ("LEFTPADDING", (0, 0), (-1, -1), 0),
            ("RIGHTPADDING", (0, 0), (-1, -1), 0),
            ("ALIGN", (1, 0), (1, 0), "RIGHT"),
        ]))
        S.append(pt)
        S.append(Paragraph(desc, sProjDesc))
        S.append(Paragraph(techs, sProjTech))
        S.append(Spacer(1, 1.5))

    # ── Footer ──
    S.append(Spacer(1, 4*mm))
    S.append(HRFlowable(width="100%", thickness=0.3, color=GRAY_LINE, spaceBefore=2, spaceAfter=2))
    S.append(Paragraph(content["footer"], sFooter))

    doc.build(S)
    print(f"  ✓  {output_path}")


if __name__ == "__main__":
    print("Generating CV PDFs...")
    build_cv(EN, os.path.join(OUTPUT_DIR, "terentiy-gatsukov-cv-en.pdf"), FONTS["en"])
    build_cv(RU, os.path.join(OUTPUT_DIR, "terentiy-gatsukov-cv-ru.pdf"), FONTS["ru"])
    print("Done — both CVs generated.")
