# ──────────────────────────────────────────────────────────────
# Portfolio Frontend — multi-stage Dockerfile
# Build context: frontend/
# ──────────────────────────────────────────────────────────────

# ── Build stage ─────────────────────────────────────────────
FROM node:22-alpine AS build
WORKDIR /app

# Layer install: copy package manifests first
COPY package.json package-lock.json ./
RUN npm ci

# Copy source and build
COPY . ./

# Vite build — API calls go to same origin (nginx proxies /api)
ENV VITE_API_BASE_URL=

RUN npm run build

# ── Runtime stage ───────────────────────────────────────────
FROM nginx:stable-alpine AS runtime

# Remove default nginx config
RUN rm /etc/nginx/conf.d/default.conf

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

# Nginx runs as nginx user by default in the official image
