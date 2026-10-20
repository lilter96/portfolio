import { describe, expect, it } from "vitest";
import { wideCards } from "@/lib/project-media";

const card = (id: string, title = id) => ({ id, title });

describe("wideCards", () => {
  it("pairs plain cards and widens one left alone", () => {
    expect([...wideCards([card("a"), card("b"), card("c")])]).toEqual(["c"]);
    expect([...wideCards([card("a"), card("b")])]).toEqual([]);
  });

  it("gives cards with media the whole row and widens the card they would strand", () => {
    const wide = wideCards([
      card("a"),
      card("b"),
      card("c"),
      card("hp", "Headphones Battery for GNOME"),
      card("dota", "Dota Time for GNOME"),
    ]);
    expect([...wide].sort()).toEqual(["c", "dota", "hp"]);
  });
});
