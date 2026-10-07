import { useState, useCallback, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { Layout } from "@/components/Layout";
import { Hero } from "@/pages/Hero";
import { About } from "@/pages/About";
import { Experience } from "@/pages/Experience";
import { Skills } from "@/pages/Skills";
import { Projects } from "@/pages/Projects";
import { GameShowcase } from "@/pages/GameShowcase";
import { Games } from "@/pages/Games";
import { Education } from "@/pages/Education";
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
      <Layout theme={theme} onToggleTheme={toggleTheme}>
        <Hero />
        <About />
        <Experience />
        <GameShowcase />
        <Games />
        <Projects />
        <Skills />
        <Education />
        <Contact />
      </Layout>
    </div>
  );
}

export default App;
