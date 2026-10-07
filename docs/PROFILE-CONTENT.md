# Profile content sources

Updated on 2026-10-07 from two CVs supplied by the profile owner. The owner requested these as the authoritative sources for contacts, commercial experience and skills.

| Supplied source | Public copy | SHA-256 |
| --- | --- | --- |
| `Gatsukov_Terentiy_Slot_Game_Developer_EN.pdf` | [terentiy-gatsukov-slot-games-en.pdf](../frontend/public/cv/terentiy-gatsukov-slot-games-en.pdf) | `d4d4116830a01bd65ff89bde856b0c156862289c5b85b131bb85365d6b70c3ac` |
| `Terentiy_Gatsukov_DotNET_CV.pdf` | [terentiy-gatsukov-dotnet-ru.pdf](../frontend/public/cv/terentiy-gatsukov-dotnet-ru.pdf) | `82248d2f829471e397240834517f7e1e6c1f324708245169edea5c7cfbbbe5d7` |

## Content rules

- Commercial dates, metrics, game roles, contacts, education and language levels follow the supplied CVs. They are not remeasured by the portfolio test suites.
- Custom Games Studio: November 2023–July 2026; Solvintech: July 2022–October 2023; Elgrow: January 2021–June 2022. Month precision is used; stored first-of-month dates are display representatives, not claims about exact employment days.
- Softeq and Syberry were removed from the old seed because these CVs do not list them. Education is a separate 2023 engineering degree entry; no inferred enrollment date.
- The LinkedIn and game-video destinations were extracted from PDF hyperlink annotations rather than inferred from shortened visible labels.
- Public project descriptions, test counts and prototype boundaries come from the separate repository audit. The Revit projects are portfolio work; commercial Revit/Tekla experience is not added to the CV history.
- Both CV copies are byte-identical to the supplied originals. No translated or synthesized CV is substituted.
- GitHub Pages uses static curated content. Fresh API databases receive the corrected seed; existing database records are not silently overwritten.

## Verification

Frontend: 43 tests, strict TypeScript build and ESLint. Backend: 56 tests using disposable PostgreSQL/Redis fixtures. Two committed Playwright profile checks run in CI. Additional browser verification covers both languages, desktop/mobile layout, contact destinations and byte-identical CV downloads.

The backend coverage gate merges executable lines from all four test-project reports. All 56 tests participate in collection; empty reports no longer determine the result by directory order. The 30% threshold is retained. On the local Release verification, merged line coverage was 1,987/2,219 (89.5%).
