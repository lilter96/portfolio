import { useState, useRef, useCallback } from "react";
import { useTranslation } from "react-i18next";
import { useMutation } from "@tanstack/react-query";
import { submitContact } from "@/lib/api";

interface FormErrors {
  name?: string;
  email?: string;
  message?: string;
}

function validate(data: { name: string; email: string; message: string }): FormErrors {
  const errors: FormErrors = {};
  if (!data.name.trim()) errors.name = "Name is required.";
  else if (data.name.length > 100) errors.name = "Name must not exceed 100 characters.";

  if (!data.email.trim()) errors.email = "Email is required.";
  else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.email))
    errors.email = "A valid email address is required.";
  else if (data.email.length > 256) errors.email = "Email must not exceed 256 characters.";

  if (!data.message.trim()) errors.message = "Message is required.";
  else if (data.message.length < 10)
    errors.message = "Message must be at least 10 characters.";
  else if (data.message.length > 5000)
    errors.message = "Message must not exceed 5000 characters.";

  return errors;
}

export function Contact() {
  const { t } = useTranslation();
  const [form, setForm] = useState({ name: "", email: "", message: "", website: "" });
  const [errors, setErrors] = useState<FormErrors>({});
  const [touched, setTouched] = useState<Record<string, boolean>>({});
  const formRef = useRef<HTMLFormElement>(null);
  const nameRef = useRef<HTMLInputElement>(null);

  const mutation = useMutation({
    mutationFn: submitContact,
    onSuccess: () => {
      setForm({ name: "", email: "", message: "", website: "" });
      setErrors({});
      setTouched({});
      nameRef.current?.focus();
    },
  });

  const handleChange = useCallback(
    (field: string) => (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
      const value = e.target.value;
      setForm((prev) => {
        const next = { ...prev, [field]: value };
        if (touched[field]) {
          setErrors(validate(next));
        }
        return next;
      });
    },
    [touched],
  );

  const handleBlur = useCallback(
    (field: string) => () => {
      setTouched((prev) => ({ ...prev, [field]: true }));
      setErrors(validate(form));
    },
    [form],
  );

  const handleSubmit = useCallback(
    (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      const errs = validate(form);
      setErrors(errs);
      setTouched({ name: true, email: true, message: true });
      if (Object.keys(errs).length > 0) return;
      mutation.mutate(form);
    },
    [form, mutation],
  );

  const fieldError = (field: keyof FormErrors) =>
    touched[field] && errors[field] ? errors[field] : undefined;

  return (
    <section id="contact" className="contact-section" aria-label={t("nav.contact")}>
      <h2 className="section-title reveal">{t("nav.contact")}</h2>

      <div className="contact-layout">
        <div className="contact-info reveal reveal-1">
          <p className="contact-info-text">{t("contact.info")}</p>
          <a className="contact-email-link mono" href="mailto:terentiy.gatsukov@gmail.com">
            terentiy.gatsukov@gmail.com
          </a>
          <div className="contact-links">
            <a
              href="https://github.com/lilter96"
              target="_blank"
              rel="noopener noreferrer"
              className="contact-social-link"
            >
              GitHub
            </a>
            <a
              href="https://linkedin.com/in/terentiy-gatsukov-048694224"
              target="_blank"
              rel="noopener noreferrer"
              className="contact-social-link"
            >
              LinkedIn
            </a>
          </div>
        </div>

        <form
          ref={formRef}
          className="contact-form reveal reveal-2"
          onSubmit={handleSubmit}
          noValidate
          aria-label={t("contact.formLabel")}
          aria-busy={mutation.isPending}
        >
          {/* Status messages */}
          {mutation.isSuccess && (
            <div className="contact-status contact-status-ok" role="status">
              {t("contact.success")}
            </div>
          )}
          {mutation.isError && (
            <div className="contact-status contact-status-err" role="alert">
              {mutation.error?.message ?? t("contact.error")}
            </div>
          )}

          {/* Honeypot — visually hidden, real users leave empty */}
          <div className="sr-only" aria-hidden="true">
            <label htmlFor="website">Website</label>
            <input
              id="website"
              name="website"
              type="text"
              tabIndex={-1}
              autoComplete="off"
              value={form.website}
              onChange={handleChange("website")}
            />
          </div>

          <FormField label={t("contact.nameLabel")} error={fieldError("name")} htmlFor="contact-name">
            <input
              ref={nameRef}
              id="contact-name"
              type="text"
              className={`contact-input ${fieldError("name") ? "contact-input-err" : ""}`}
              value={form.name}
              onChange={handleChange("name")}
              onBlur={handleBlur("name")}
              required
              maxLength={100}
              autoComplete="name"
              aria-required="true"
              aria-invalid={!!fieldError("name")}
              aria-describedby={fieldError("name") ? "contact-name-err" : undefined}
            />
          </FormField>

          <FormField label={t("contact.emailLabel")} error={fieldError("email")} htmlFor="contact-email">
            <input
              id="contact-email"
              type="email"
              className={`contact-input ${fieldError("email") ? "contact-input-err" : ""}`}
              value={form.email}
              onChange={handleChange("email")}
              onBlur={handleBlur("email")}
              required
              maxLength={256}
              autoComplete="email"
              aria-required="true"
              aria-invalid={!!fieldError("email")}
              aria-describedby={fieldError("email") ? "contact-email-err" : undefined}
            />
          </FormField>

          <FormField
            label={t("contact.messageLabel")}
            error={fieldError("message")}
            htmlFor="contact-message"
          >
            <textarea
              id="contact-message"
              className={`contact-input contact-textarea ${fieldError("message") ? "contact-input-err" : ""}`}
              value={form.message}
              onChange={handleChange("message")}
              onBlur={handleBlur("message")}
              required
              minLength={10}
              maxLength={5000}
              rows={5}
              autoComplete="off"
              aria-required="true"
              aria-invalid={!!fieldError("message")}
              aria-describedby={`contact-message-charcount${fieldError("message") ? " contact-message-err" : ""}`}
            />
            <span id="contact-message-charcount" className="contact-charcount mono">
              {form.message.length}/5000
            </span>
          </FormField>

          <button
            type="submit"
            className="contact-submit"
            disabled={mutation.isPending}
          >
            {mutation.isPending ? t("contact.sending") : t("contact.send")}
          </button>
        </form>
      </div>
    </section>
  );
}

function FormField({
  label,
  error,
  htmlFor,
  children,
}: {
  label: string;
  error?: string;
  htmlFor: string;
  children: React.ReactNode;
}) {
  const errId = `${htmlFor}-err`;
  return (
    <div className="contact-field">
      <label htmlFor={htmlFor} className="contact-label">
        {label}
      </label>
      {children}
      {error && (
        <p id={errId} className="contact-field-err" role="alert">
          {error}
        </p>
      )}
    </div>
  );
}
