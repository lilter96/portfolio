import type { ReactNode } from "react";
import { useTranslation } from "react-i18next";

interface QueryStateProps<T> {
  data: T[] | undefined;
  isLoading: boolean;
  error: Error | null;
  children: (data: T[]) => ReactNode;
}

export function QueryState<T>({ data, isLoading, error, children }: QueryStateProps<T>) {
  if (isLoading) return <Loading />;
  if (error) return <ErrorBlock message={error.message} />;
  if (!data || data.length === 0) return <Empty />;
  return <>{children(data)}</>;
}

function Loading() {
  const { t } = useTranslation();

  return (
    <div className="query-loading" role="status" aria-label={t("common.loading")}>
      <div className="loading-pulse" />
      <p className="mono" style={{ fontSize: "var(--text-sm)", color: "var(--color-text-muted)" }}>
        {t("common.loading")}
        <span className="cursor" />
      </p>
    </div>
  );
}

function ErrorBlock({ message }: { message: string }) {
  const { t } = useTranslation();

  return (
    <div className="query-error" role="alert">
      <p
        className="display"
        style={{
          fontSize: "var(--text-sm)",
          color: "var(--color-accent-cherry)",
          marginBottom: "var(--space-2)",
        }}
      >
        {t("common.error")}
      </p>
      <p className="mono" style={{ fontSize: "var(--text-xs)", color: "var(--color-text-muted)" }}>
        {message}
      </p>
    </div>
  );
}

function Empty() {
  const { t } = useTranslation();

  return (
    <div className="query-empty">
      <p className="mono" style={{ fontSize: "var(--text-sm)", color: "var(--color-text-muted)" }}>
        {t("common.noData")}
      </p>
    </div>
  );
}
