/**
 * Generate static SEO assets:
 *   apple-touch-icon.png  180×180
 *   og-card.png          1200×630
 */
import sharp from "sharp";
import { resolve, dirname } from "node:path";
import { fileURLToPath } from "node:url";

const __dirname = dirname(fileURLToPath(import.meta.url));
const publicDir = resolve(__dirname, "..", "public");

// ── Colour palette ──────────────────────────────────────────
const BG = "#08080f";
const ACCENT = "#00ff41";
const TEXT = "#e8e6f0";
const MUTED = "#a098b0";

// ── Apple Touch Icon (180×180) ──────────────────────────────
async function generateAppleTouchIcon() {
  const size = 180;
  const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}" viewBox="0 0 ${size} ${size}">
    <rect width="${size}" height="${size}" rx="30" fill="${BG}"/>
    <rect width="${size}" height="${size}" rx="30" fill="none" stroke="${ACCENT}" stroke-width="2" opacity="0.25"/>
    <text x="${size / 2}" y="${size / 2 + 18}" font-family="Orbitron, 'Courier New', monospace" font-size="68" font-weight="900" fill="${ACCENT}" text-anchor="middle" letter-spacing="4">TG</text>
  </svg>`;

  await sharp(Buffer.from(svg)).png().toFile(resolve(publicDir, "apple-touch-icon.png"));
  console.log("✅ apple-touch-icon.png");
}

// ── OG Card (1200×630) ──────────────────────────────────────
async function generateOgCard() {
  const w = 1200;
  const h = 630;
  const pad = 64;

  const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="${w}" height="${h}" viewBox="0 0 ${w} ${h}">
    <defs>
      <linearGradient id="grid" x1="0" y1="0" x2="1" y2="1">
        <stop offset="0%" stop-color="${ACCENT}" stop-opacity="0.04"/>
        <stop offset="100%" stop-color="${ACCENT}" stop-opacity="0.01"/>
      </linearGradient>
      <linearGradient id="scanline" x1="0" y1="0" x2="0" y2="1">
        <stop offset="0%" stop-color="${ACCENT}" stop-opacity="0.03"/>
        <stop offset="50%" stop-color="${ACCENT}" stop-opacity="0"/>
        <stop offset="100%" stop-color="${ACCENT}" stop-opacity="0.03"/>
      </linearGradient>
    </defs>

    <!-- Background -->
    <rect width="${w}" height="${h}" fill="${BG}"/>
    <rect width="${w}" height="${h}" fill="url(#grid)"/>

    <!-- Decorative circuit lines -->
    <line x1="0" y1="${h - 1}" x2="${w * 0.35}" y2="${h - 1}" stroke="${ACCENT}" stroke-width="1" opacity="0.12"/>
    <line x1="${w * 0.65}" y1="0" x2="${w}" y2="0" stroke="${ACCENT}" stroke-width="1" opacity="0.12"/>
    <line x1="0" y1="0" x2="0" y2="${h * 0.12}" stroke="${ACCENT}" stroke-width="1" opacity="0.12"/>
    <line x1="${w}" y1="${h * 0.88}" x2="${w}" y2="${h}" stroke="${ACCENT}" stroke-width="1" opacity="0.12"/>

    <!-- Monogram -->
    <text x="${w / 2}" y="${h / 2 - 40}" font-family="Orbitron, 'Courier New', monospace" font-size="96" font-weight="900" fill="${ACCENT}" text-anchor="middle" letter-spacing="8">TG</text>

    <!-- Name -->
    <text x="${w / 2}" y="${h / 2 + 40}" font-family="'Courier New', monospace" font-size="48" font-weight="700" fill="${TEXT}" text-anchor="middle" letter-spacing="2">Terentiy Gatsukov</text>

    <!-- Role line -->
    <text x="${w / 2}" y="${h / 2 + 95}" font-family="'Courier New', monospace" font-size="28" fill="${MUTED}" text-anchor="middle" letter-spacing="1">iGaming × .NET Backend × Real-Time Systems</text>

    <!-- Bottom accent -->
    <rect x="${w / 2 - 80}" y="${h - pad - 4}" width="160" height="2" fill="${ACCENT}" opacity="0.4" rx="1"/>

    <!-- Scanline overlay -->
    <rect width="${w}" height="${h}" fill="url(#scanline)"/>
  </svg>`;

  await sharp(Buffer.from(svg)).png().toFile(resolve(publicDir, "og-card.png"));
  console.log("✅ og-card.png");
}

// ── Run ─────────────────────────────────────────────────────
await generateAppleTouchIcon();
await generateOgCard();
console.log("Done.");
