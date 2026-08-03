# EstateFlow Architecture Overview

## System Architecture
EstateFlow is built using a Clean Architecture-inspired layered design.

### Layered Boundary Summary
- `src/EstateFlow.Api` — ASP.NET Core API host and delivery boundary.
- `src/EstateFlow.Application` — application services, use cases, and orchestration logic.
- `src/EstateFlow.Domain` — domain model, aggregates, value objects, and business invariants.
- `src/EstateFlow.Infrastructure` — infrastructure adapters, persistence foundations, and external service contracts.
- `src/EstateFlow.Shared` — reusable shared types, utilities, and cross-cutting abstractions.
- `tests/` — xUnit test projects for integration, domain, application, and infrastructure verification.

## Project Boundaries
- Domain remains isolated from infrastructure and framework concerns.
- Application depends on Domain and defines use-case orchestration.
- API depends on Application and serves as the external request boundary.
- Infrastructure depends on Application and Domain for persistence and runtime services.
- Shared contains cross-cutting and reusable abstractions without direct domain logic.

## Technology Stack
- .NET 9.0 for main application projects.
- .NET 10.0 for test projects.
- ASP.NET Core for API hosting.
- xUnit for automated testing.
- GitHub Actions for CI and release workflow automation.

## Delivery and Verification
- `build-validation.yml` validates restore, build, and test execution for pull requests and branch pushes.
- `release-workflow.yml` is triggered by version tags matching `v*`, performs restore/build/test, packages release artifacts, and validates release metadata.

## Architectural Principles
- Separation of concerns across domain, application, API, infrastructure, and shared layers.
- Documentation-first governance and release preparation.
- Traceability between product decisions, engineering artifacts, and release readiness documentation.
- Minimal implementation scope for release preparation: no release deployment or infrastructure automation.

## Deployment and Operations
- No production deployment pipeline is implemented in this branch.
- Release readiness is currently validated through documentation, workflow configuration, and build/test verification.
- Operational and environment-specific concerns are to be handled outside this repository until a separate deployment plan is authorized.
