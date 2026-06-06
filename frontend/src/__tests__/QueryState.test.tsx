import { describe, it, expect, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import { QueryState } from "@/components/QueryState";

// Wrap in a provider since QueryState sub-components use useTranslation
// We mock react-i18next to avoid needing the full i18n setup
vi.mock("react-i18next", () => ({
  useTranslation: () => ({
    t: (key: string) => {
      const map: Record<string, string> = {
        "common.loading": "Loading",
        "common.error": "ERROR",
        "common.noData": "No data available.",
      };
      return map[key] ?? key;
    },
    i18n: { language: "en", changeLanguage: vi.fn() },
  }),
}));

describe("QueryState", () => {
  it("renders loading state when isLoading is true", () => {
    render(
      <QueryState data={undefined} isLoading={true} error={null}>
        {() => <p>Data</p>}
      </QueryState>,
    );

    expect(screen.getByRole("status")).toBeInTheDocument();
    expect(screen.getByText("Loading")).toBeInTheDocument();
  });

  it("renders error state when error is present", () => {
    render(
      <QueryState data={undefined} isLoading={false} error={new Error("Something broke")}>
        {() => <p>Data</p>}
      </QueryState>,
    );

    expect(screen.getByRole("alert")).toBeInTheDocument();
    expect(screen.getByText("ERROR")).toBeInTheDocument();
    expect(screen.getByText("Something broke")).toBeInTheDocument();
  });

  it("renders empty state when data is empty array", () => {
    render(
      <QueryState data={[]} isLoading={false} error={null}>
        {() => <p>Data</p>}
      </QueryState>,
    );

    expect(screen.getByText("No data available.")).toBeInTheDocument();
  });

  it("renders children when data is present", () => {
    render(
      <QueryState<{ id: number }>
        data={[{ id: 1 }, { id: 2 }]}
        isLoading={false}
        error={null}
      >
        {(data) => <p>Got {data.length} items</p>}
      </QueryState>,
    );

    expect(screen.getByText("Got 2 items")).toBeInTheDocument();
  });
});
