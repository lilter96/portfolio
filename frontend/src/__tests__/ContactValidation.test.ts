import { describe, it, expect } from "vitest";

// Test the validation logic directly (extracted for testability)
function validate(data: { name: string; email: string; message: string }): Record<string, string> {
  const errors: Record<string, string> = {};
  if (!data.name.trim()) errors.name = "Name is required.";
  else if (data.name.length > 100) errors.name = "Name must not exceed 100 characters.";

  if (!data.email.trim()) errors.email = "Email is required.";
  else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.email))
    errors.email = "A valid email address is required.";

  if (!data.message.trim()) errors.message = "Message is required.";
  else if (data.message.length < 10) errors.message = "Message must be at least 10 characters.";
  else if (data.message.length > 5000) errors.message = "Message must not exceed 5000 characters.";

  return errors;
}

describe("Contact form validation", () => {
  it("passes for valid data", () => {
    const errors = validate({
      name: "Test User",
      email: "test@example.com",
      message: "Hello, this is a valid test message.",
    });
    expect(Object.keys(errors)).toHaveLength(0);
  });

  it("requires name", () => {
    const errors = validate({ name: "", email: "test@example.com", message: "Valid message here" });
    expect(errors.name).toBe("Name is required.");
  });

  it("requires valid email", () => {
    const errors = validate({ name: "User", email: "not-an-email", message: "Valid message here" });
    expect(errors.email).toBe("A valid email address is required.");
  });

  it("requires message of at least 10 characters", () => {
    const errors = validate({ name: "User", email: "test@example.com", message: "Short" });
    expect(errors.message).toBe("Message must be at least 10 characters.");
  });

  it("rejects message over 5000 characters", () => {
    const errors = validate({
      name: "User",
      email: "test@example.com",
      message: "x".repeat(5001),
    });
    expect(errors.message).toBe("Message must not exceed 5000 characters.");
  });

  it("returns multiple errors for empty form", () => {
    const errors = validate({ name: "", email: "", message: "" });
    expect(errors.name).toBeDefined();
    expect(errors.email).toBeDefined();
    expect(errors.message).toBeDefined();
  });
});
