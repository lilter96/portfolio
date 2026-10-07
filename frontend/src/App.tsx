import { useState, useCallback, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { Layout } from "@/components/Layout";
import { Hero } from "@/pages/Hero";
import { About } from "@/pages/About";
import { Experience } from "@/pages/Experience";
import { Skills } from "@/pages/Skills";
import { Projects } from "@/pages/Projects";
import { Contact } from "@/pages/Contact";

const THEME_KEY = "portfolio-theme";

function getInitialTheme(): "dark" | "light" {
  try {
    const stored = localStorage.getItem(THEME_KEY);
    if (stored === "dark" || stored === "light") return stored;
  } catch {
    // localStorage unavailable (private browsing, SSR, etc.)
  }
  return "dark";
}

function App() {
  const { i18n } = useTranslation();
  const [theme, setTheme] = useState<"dark" | "light">(getInitialTheme);

  const toggleTheme = useCallback(() => {
    setTheme((t) => (t === "dark" ? "light" : "dark"));
  }, []);

  // Persist theme to localStorage
  useEffect(() => {
    try {
      localStorage.setItem(THEME_KEY, theme);
    } catch {
      // localStorage unavailable
    }
  }, [theme]);

  // Sync html[lang] with i18n language
  useEffect(() => {
    document.documentElement.lang = i18n.language;
  }, [i18n.language]);

  return (
    <div data-theme={theme}>
      <div className="scanlines" />
      <Layout>
        <button
          className="theme-toggle"
          onClick={toggleTheme}
          type="button"
          aria-label={
            theme === "dark"
              ? "☀ Light — Switch to light theme"
              : "☾ Dark — Switch to dark theme"
          }
          style={{
            position: "fixed",
            top: "var(--space-4)",
            right: "var(--space-4)",
            zIndex: "var(--z-sticky)",
          }}
        >
          {theme === "dark" ? "☀ Light" : "☾ Dark"}
        </button>
        <Hero />
        <About />
        <Projects />
        <Experience />
        <Skills />
        <Contact />
      </Layout>
    </div>
  );
}

export default App;
