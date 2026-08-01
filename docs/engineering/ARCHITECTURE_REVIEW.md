# Architecture Review — EstateFlow (Pre-Stage Architecture Review)

Purpose
- Provide a concise, AI-friendly and human-readable architecture decision record for EstateFlow prior to implementing the v1.0 Architecture Skeleton.
- Capture architecture goals, proposed solution structure, dependency rules, responsibility boundaries, and S03 implementation boundaries.

Ownership & Update Responsibility
- Primary owner: Lead Engineer.
- Document steward: Repository steward / engineering coordinator.
- Updates shall be proposed by the Lead Engineer and approved by the Product Owner or delegated authority before committing.

Principle (mandatory)
- Documentation precedes implementation. Governance precedes code. Architecture precedes frameworks.

1. Architecture Goals
- Maintainability (must): code and project layout shall be organized for easy comprehension and safe refactoring.
- Separation of concerns (must): layers shall have clearly defined responsibilities to reduce coupling.
- Dependency control (must): dependency flow shall be one-directional according to rules below.
- Testability (shall): components must be structured to enable unit and integration testing without requiring production infrastructure.
- Domain authority preservation (must): the Domain layer shall be the authoritative source for business meaning and validation.

2. Proposed Solution Structure
- Domain layer (`EstateFlow.Domain`)
  - Responsibility: owns business concepts, invariants, value objects, domain events, and validation rules.
  - Must not depend on other solution layers.
- Application layer (`EstateFlow.Application`)
  - Responsibility: coordinates use cases, orchestrates domain operations, defines application-level DTOs and service interfaces.
  - May depend on `Domain` only.
- Infrastructure layer (`EstateFlow.Infrastructure`)
  - Responsibility: implements technical capabilities (persistence adapters, messaging, file storage) behind interfaces defined by `Application` or `Shared`.
  - May depend on `Application` and `Domain`.
- API layer (`EstateFlow.Api`)
  - Responsibility: exposes application use cases to external clients (HTTP surface) and translates transport into application commands/queries.
  - May depend on `Application` and `Shared`.
- Shared components (`EstateFlow.Shared`)
  - Responsibility: cross-cutting abstractions (contracts, logging interfaces, common DTOs) that do not contain domain rules.
  - May be referenced by Application and API; must avoid domain logic.

3. Dependency Rules
- Allowed dependencies (examples):
  - `Application` -> `Domain`
  - `Infrastructure` -> `Application`, `Domain`
  - `Api` -> `Application`, `Shared`
  - `Shared` -> (no Domain or Application logic)
- Forbidden dependencies (must not occur):
  - `Domain` depends on `Application`, `Infrastructure`, or `Api`.
  - Cyclical project references across layers.
  - Direct dependencies from `Api` or `Infrastructure` into domain implementation details that bypass `Application` contracts.

4. Responsibility Boundaries
- Application
  - Coordinates use cases, handles orchestration, and enforces application-level policies.
  - Must not contain business rules that belong to `Domain`.
- Domain
  - Owns business meaning, entities, value objects, and validation logic.
  - Must be the single source of truth for business invariants.
- Infrastructure
  - Provides technical capabilities and adapters implementing interfaces; must remain replaceable.
- Repository
  - Persists only approved state and artifacts; repository-level changes must be recorded in governance documents.

5. Future S03 Implementation Boundaries
- Allowed in S03 (with explicit Lead Engineer approval):
  - Add project references that establish the allowed dependency directions.
  - Create empty architectural projects or folders for layering (no business code inside).
  - Define public interfaces and adapter contracts (no implementation logic beyond stubs).
- Not allowed in S03 (must not be implemented):
  - Business entities, domain rules, or validation code.
  - Database schemas, persistence logic, or migration code.
  - API endpoints, controllers, or authentication flows.
  - User workflows or UI components.

Notes & Compliance
- This document is an architecture decision record for the Pre-Stage Architecture Review; it does not authorize implementation of the above items.
- All future S03 activities must reference this document and obtain explicit Lead Engineer approval before implementing any allowed artifacts.

Revision Log
- 2026-08-01 — PSAR-001: Initial architecture review created by AI agent under direction of Lead Engineer.
