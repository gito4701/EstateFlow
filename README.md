# EstateFlow

EstateFlow is a governance-led Clean Architecture reference application built for release preparation and documentation-driven delivery.

## What is included
- `src/EstateFlow.Api` — ASP.NET Core API host and delivery boundary.
- `src/EstateFlow.Application` — application services and use-case orchestration.
- `src/EstateFlow.Domain` — domain model, aggregates, and business invariants.
- `src/EstateFlow.Infrastructure` — infrastructure scaffolding and persistence foundations.
- `src/EstateFlow.Shared` — shared utilities and cross-cutting abstractions.
- `tests/` — integration and unit tests for the current solution.
- `docs/` — governance, product definition, release readiness, and engineering evidence.
- `.github/workflows/build-validation.yml` — CI validation pipeline for PRs and branch pushes.
- `.github/workflows/release-workflow.yml` — tag-triggered release workflow for packaging and metadata validation.

## Release preparation
This branch is focused on release readiness documentation only.

The release preparation artifacts include:
- `RELEASE_NOTES_v1.0.md`
- `ARCHITECTURE_OVERVIEW.md`
- `PROJECT_STATUS.md`
- `CHANGELOG.md`
- Updated `README.md` describing architecture, build/run instructions, and release readiness.

No production code, business logic, deployment automation, or infrastructure changes are included in this release preparation branch.

## Changelog
See `CHANGELOG.md` for the project history and release notes.

## Build and test
From the repository root:
```powershell
dotnet restore EstateFlow.sln
dotnet build EstateFlow.sln
dotnet test EstateFlow.sln
```

## Run locally
```powershell
dotnet run --project src/EstateFlow.Api/EstateFlow.Api.csproj
```

## CI/CD workflows
- `build-validation.yml` runs on pull requests and branch pushes to validate restore, build, and test execution.
- `release-workflow.yml` runs on Git tags matching `v*` and validates release artifact packaging and metadata.

## Governance posture
EstateFlow is currently maintained as a documentation-driven project with an emphasis on:
- architectural clarity
- release governance and traceability
- CI/CD readiness without deploying production infrastructure
- documentation-only release preparation for v1.0

## Notes
This repository is intended for review, release preparation, and evidence generation. Code and workflow changes are intentionally limited to the documentation and validation scope for the current branch.
