# ──────────────────────────────────────────────────────────────
# Portfolio — common development tasks
#   make up              → start the full stack
#   make logs            → follow all service logs
#   make test            → run backend tests
#   make clean           → stop everything + remove volumes
# ──────────────────────────────────────────────────────────────

.PHONY: up down build rebuild logs logs-api logs-web ps clean \
        test test-backend test-frontend test-watch \
        db-reset db-migrate db-seed db-recreate \
        restart-api restart-web \
        lint lint-backend lint-frontend \
        format format-backend format-frontend \
        typecheck help

# ═══════════════════════════════════════════════════════════════
#  Docker Compose
# ═══════════════════════════════════════════════════════════════

up:                    ## Start all services (build + up)
	docker compose up --build -d
	@echo "✅ Full stack running at http://localhost:3000"

down:                  ## Stop all services
	docker compose down

build:                 ## Build service images
	docker compose build

rebuild:               ## Rebuild images without cache
	docker compose build --no-cache

logs:                  ## Follow all service logs
	docker compose logs -f

logs-api:              ## Follow API logs
	docker compose logs -f api

logs-web:              ## Follow frontend logs
	docker compose logs -f web

ps:                    ## Show running services
	docker compose ps

restart-api:           ## Restart the API service
	docker compose restart api

restart-web:           ## Restart the frontend service
	docker compose restart web

# ═══════════════════════════════════════════════════════════════
#  Cleanup
# ═══════════════════════════════════════════════════════════════

clean:                 ## Stop services and remove volumes (destroys DB data)
	docker compose down -v
	@echo "✅ Stopped and removed volumes"

# ═══════════════════════════════════════════════════════════════
#  Database
# ═══════════════════════════════════════════════════════════════

db-reset: clean up     ## Recreate database from scratch

db-migrate:            ## Run EF Core migrations (API must be running)
	dotnet ef database update --project backend/src/Api --startup-project backend/src/Api

db-seed:               ## Re-run database seeder (API must be running)
	@echo "⚠️  Seeder runs automatically on API startup"
	@echo "   Restart the API to re-seed: make restart-api"

# ═══════════════════════════════════════════════════════════════
#  Testing
# ═══════════════════════════════════════════════════════════════

test: test-backend test-frontend ## Run all tests

test-backend:          ## Run .NET tests
	dotnet test backend/Portfolio.slnx --verbosity normal

test-frontend:         ## Run frontend tests
	cd frontend && npm test

test-watch:            ## Run frontend tests in watch mode
	cd frontend && npm run test:watch

# ═══════════════════════════════════════════════════════════════
#  Code Quality
# ═══════════════════════════════════════════════════════════════

lint: lint-backend lint-frontend ## Run all linters

lint-backend:          ## Analyze .NET code
	dotnet format analyzers backend/Portfolio.slnx --verify-no-changes

lint-frontend:         ## Run ESLint
	cd frontend && npm run lint

format: format-backend format-frontend ## Format all code

format-backend:        ## Format C# code
	dotnet format backend/Portfolio.slnx

format-frontend:       ## Format TS/CSS/JSON
	cd frontend && npm run format

typecheck:             ## Check TypeScript types
	cd frontend && npm run typecheck

# ═══════════════════════════════════════════════════════════════
#  Help
# ═══════════════════════════════════════════════════════════════

help:                  ## Show this help
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) \
	| sort \
	| awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-20s\033[0m %s\n", $$1, $$2}'
