/* ── Project media ──────────────────────────────────────────
   Animations and screenshots for projects that have a UI to show.
   Keyed by title, like the Russian descriptions. Files live in
   public/media/projects/<dir>/: demo.webp (animated), still.webp
   (its last frame, for reduced motion), cards.webp and panel.webp.
   ───────────────────────────────────────────────────────── */

export interface MediaSize {
  w: number;
  h: number;
}

interface MediaText {
  animation: string;
  cards: string;
  panel: string;
  note: string;
}

export interface ProjectMedia {
  dir: string;
  animation: MediaSize;
  cards: MediaSize;
  panel: MediaSize;
  en: MediaText;
  ru: MediaText;
}

export const projectMedia: Record<string, ProjectMedia> = {
  "Headphones Battery for GNOME": {
    dir: "gnome-headphones-battery",
    animation: { w: 377, h: 660 },
    cards: { w: 1581, h: 953 },
    panel: { w: 1420, h: 132 },
    en: {
      animation: "The popup opening: the ring sweeps to 72%, the plumbob drops in and the cat bobs to the music",
      cards: "Three states of the popup: listening at 72%, 2% with a tired cat, and an approximate 16% while the cat dozes",
      panel: "The top bar item in every mood: green, yellow, orange and red plumbobs, then the dozing cat",
      note: "Rendered in a headless GNOME Shell with numbers from the real estimator. UI in Russian.",
    },
    ru: {
      animation: "Попап открывается: кольцо доходит до 72%, падает пламбоб, кот качается под музыку",
      cards: "Три состояния попапа: музыка при 72%, 2% с уставшим котом и примерные 16%, пока кот дремлет",
      panel: "Значок в панели во всех настроениях: зелёный, жёлтый, оранжевый и красный пламбоб, затем дремлющий кот",
      note: "Снято в headless GNOME Shell, числа считает настоящая модель разряда.",
    },
  },
  "Dota Time for GNOME": {
    dir: "gnome-dota-time",
    animation: { w: 416, h: 933 },
    cards: { w: 1725, h: 1303 },
    panel: { w: 782, h: 132 },
    en: {
      animation: "The popup opening: the time counts up with the ring to 4 h 21 min, Roshan stomps, then the month chart grows",
      cards: "Three moments of a day: 1 h 12 min with Roshan nibbling cheese, 3 h 7 min in a match as Invoker, 4 h 21 min with Roshan in aggro",
      panel: "The top bar item: a sleeping Roshan, then 1:12, 3:07 and a red 4:21",
      note: "Rendered in a headless GNOME Shell from made-up data. UI in Russian.",
    },
    ru: {
      animation: "Попап открывается: время досчитывается вместе с кольцом до 4 ч 21 мин, Рошан бьёт лапами, затем растёт график месяца",
      cards: "Три момента дня: 1 ч 12 мин, Рошан грызёт сыр; 3 ч 7 мин в матче за Invoker; 4 ч 21 мин, Рошан в агре",
      panel: "Значок в панели: спящий Рошан, затем 1:12, 3:07 и красные 4:21",
      note: "Снято в headless GNOME Shell на выдуманных данных.",
    },
  },
  "Psiphon Tunnel for GNOME": {
    dir: "gnome-psiphon-tunnel",
    animation: { w: 416, h: 993 },
    cards: { w: 1725, h: 1329 },
    panel: { w: 696, h: 132 },
    en: {
      animation: "Psiphon finds a server and the mole climbs out with its lamp on, then the stop button asks about the three programs using the tunnel and the popup waits for them",
      cards: "Three moments: the tunnel open with Firefox, Claude Code and Codex going through it; the question before closing it; waiting for them to close. Below them, the country and the system proxy switch",
      panel: "The top bar helmet: off, connecting, open, waiting to close, crashed",
      note: "Rendered in a headless GNOME Shell against a fake Psiphon; the programs are made up.",
    },
    ru: {
      animation: "Psiphon находит сервер, крот вылезает с зажжённой лампой, затем кнопка выключения спрашивает про три программы в туннеле, и попап ждёт их",
      cards: "Три момента: туннель открыт, через него ходят Firefox, Claude Code и Codex; вопрос перед выключением; ожидание, пока они закроются. Под ними выбор страны и переключатель системного прокси",
      panel: "Шлем в панели: выключен, подключается, открыт, ждёт, чтобы выключиться, упал",
      note: "Снято в headless GNOME Shell с фейковым Psiphon; программы выдуманы. Интерфейс на снимках английский, в расширении есть и русский.",
    },
  },
};

/** Which cards take the full row: every card with media, and a card that
 *  would otherwise sit alone in its row of two. */
export function wideCards(projects: { id: string; title: string }[]): Set<string> {
  const wide = new Set<string>();
  let waiting: string | null = null; // a card holding the left half of a row
  for (const p of projects) {
    if (projectMedia[p.title]) {
      if (waiting) wide.add(waiting);
      waiting = null;
      wide.add(p.id);
    } else {
      waiting = waiting ? null : p.id;
    }
  }
  if (waiting) wide.add(waiting);
  return wide;
}
