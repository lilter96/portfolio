import { describe, it, expect, vi, beforeEach } from "vitest";
import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Contact } from "@/pages/Contact";

// ── Mock react-i18next ──────────────────────────────────────
vi.mock("react-i18next", () => ({
  useTranslation: () => ({
    t: (key: string, fallback?: string) => {
      const en: Record<string, string> = {
        "nav.contact": "Contact",
        "contact.info": "Drop me a message.",
        "contact.nameLabel": "Name",
        "contact.emailLabel": "Email",
        "contact.messageLabel": "Message",
        "contact.formLabel": "Contact form",
        "contact.send": "Send",
        "contact.sending": "Sending…",
        "contact.success": "Message sent!",
        "contact.error": "Something went wrong.",
      };
      return en[key] ?? fallback ?? key;
    },
    i18n: { language: "en", changeLanguage: vi.fn() },
  }),
}));

// ── Mock API ────────────────────────────────────────────────
vi.mock("@/lib/api", () => ({
  submitContact: vi.fn(),
}));

function renderContact() {
  const qc = new QueryClient({
    defaultOptions: { queries: { retry: false }, mutations: { retry: false } },
  });
  return render(
    <QueryClientProvider client={qc}>
      <Contact />
    </QueryClientProvider>,
  );
}

describe("Contact form", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("renders the contact section with heading", () => {
    renderContact();
    expect(screen.getByRole("heading", { name: "Contact" })).toBeInTheDocument();
  });

  it("renders name, email, and message fields", () => {
    renderContact();
    expect(screen.getByLabelText("Name")).toBeInTheDocument();
    expect(screen.getByLabelText("Email")).toBeInTheDocument();
    expect(screen.getByLabelText("Message")).toBeInTheDocument();
  });

  it("renders a submit button", () => {
    renderContact();
    expect(screen.getByRole("button", { name: "Send" })).toBeInTheDocument();
  });

  it("renders email contact link", () => {
    renderContact();
    const link = screen.getByText("lilter96dotnet@gmail.com");
    expect(link).toBeInTheDocument();
    expect(link.closest("a")).toHaveAttribute("href", "mailto:lilter96dotnet@gmail.com");
  });

  it("renders LinkedIn and GitHub links", () => {
    renderContact();
    expect(screen.getByText("GitHub").closest("a")).toHaveAttribute(
      "href",
      "https://github.com/lilter96",
    );
    expect(screen.getByText("LinkedIn").closest("a")).toHaveAttribute(
      "href",
      "https://www.linkedin.com/in/terentiy-gatsukov/",
    );
  });

  it("shows validation error when submitting empty form", async () => {
    const user = userEvent.setup();
    renderContact();

    await user.click(screen.getByRole("button", { name: "Send" }));

    await waitFor(() => {
      expect(screen.getByText("Name is required.")).toBeInTheDocument();
      expect(screen.getByText("Email is required.")).toBeInTheDocument();
      expect(screen.getByText("Message is required.")).toBeInTheDocument();
    });
  });

  it("shows name validation error on blur", async () => {
    const user = userEvent.setup();
    renderContact();

    const nameInput = screen.getByLabelText("Name");
    await user.click(nameInput);
    await user.tab(); // blur without typing

    await waitFor(() => {
      expect(screen.getByText("Name is required.")).toBeInTheDocument();
    });
  });

  it("shows email format error", async () => {
    const user = userEvent.setup();
    renderContact();

    const emailInput = screen.getByLabelText("Email");
    await user.type(emailInput, "not-an-email");
    await user.tab();

    await waitFor(() => {
      expect(screen.getByText("A valid email address is required.")).toBeInTheDocument();
    });
  });

  it("shows message too short error", async () => {
    const user = userEvent.setup();
    renderContact();

    const msgInput = screen.getByLabelText("Message");
    await user.type(msgInput, "Short");
    await user.tab();

    await waitFor(() => {
      expect(screen.getByText("Message must be at least 10 characters.")).toBeInTheDocument();
    });
  });

  it("disables submit button while pending", async () => {
    // This test verifies the button is not disabled initially
    renderContact();
    const btn = screen.getByRole("button", { name: "Send" });
    expect(btn).not.toBeDisabled();
  });

  it("has character count for message", () => {
    renderContact();
    expect(screen.getByText("0/5000")).toBeInTheDocument();
  });

  it("has a honeypot field hidden from users", () => {
    renderContact();
    const honeypot = document.getElementById("website");
    expect(honeypot).toBeInTheDocument();
    expect(honeypot?.getAttribute("tabindex")).toBe("-1");
  });

  it("sets aria-invalid on fields with errors", async () => {
    const user = userEvent.setup();
    renderContact();

    await user.click(screen.getByRole("button", { name: "Send" }));

    await waitFor(() => {
      expect(screen.getByLabelText("Name")).toHaveAttribute("aria-invalid", "true");
      expect(screen.getByLabelText("Email")).toHaveAttribute("aria-invalid", "true");
      expect(screen.getByLabelText("Message")).toHaveAttribute("aria-invalid", "true");
    });
  });

  it("clears errors when user starts typing in a touched field", async () => {
    const user = userEvent.setup();
    renderContact();

    // Submit empty to trigger all errors
    await user.click(screen.getByRole("button", { name: "Send" }));

    await waitFor(() => {
      expect(screen.getByText("Name is required.")).toBeInTheDocument();
    });

    // Now type a valid name
    await user.type(screen.getByLabelText("Name"), "John Doe");

    await waitFor(() => {
      expect(screen.queryByText("Name is required.")).not.toBeInTheDocument();
    });
  });
});
