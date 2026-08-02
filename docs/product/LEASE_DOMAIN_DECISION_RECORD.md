# Lease Domain Decision Record

## Purpose
This record captures the Product Owner decisions documented for Lease management in EstateFlow. The intent is to provide an explicit governance baseline for future implementation planning while keeping the work strictly documentation-only.

## Decision Summary

| Decision ID | Decision Topic | Status | Notes |
| --- | --- | --- | --- |
| PD-016 | Lease definition | Approved | Recorded as the business meaning of Lease within EstateFlow v1. |
| PD-017 | Lease scope | Approved | Recorded as a documentation-only product boundary with explicit implementation exclusions. |
| PD-018 | Lease relationships | Approved | Recorded as the business relationship between Lease, Property, Owner, and Tenant. |
| PD-019 | Lease business rules | Deferred | No Lease business rules or lifecycle behavior are approved in this baseline. |
| PD-020 | Lease acceptance criteria | Approved | Recorded as acceptance expectations for documentation-only Lease work. |

## PD-016 — Lease Definition
- Status: Approved.
- Draft Lease concept: Lease is the business concept representing the agreed arrangement for the use of a property asset by a tenant, with ownership context provided by the property owner.
- Role within EstateFlow: Lease provides the product-level business context for future planning and review.

## PD-017 — Lease Scope
- Status: Approved.
- Draft scope: Lease is in scope as a documentation-only product boundary with explicit exclusions for implementation artifacts.
- Included capabilities:
  - Define Lease as an explicit business concept within EstateFlow.
  - Describe the intended business relationships between Lease, Property, Owner, and Tenant.
  - Establish a documented product boundary for future Lease planning.
  - Define acceptance principles for future Lease-related work.
- Excluded capabilities:
  - Domain entities, value objects, or aggregate design for Lease.
  - Database tables, persistence design, or storage.
  - API endpoints or contract changes.
  - Application services, workflows, or business-rule implementation.
  - Legal review, payment terms, renewal workflows, termination logic, or financial obligations.

## PD-018 — Lease Relationships
- Status: Approved.
- Draft relationship meaning: Lease relates to a Property as the subject asset, to a Tenant as the occupying party, and to an Owner as the ownership context for the arrangement.
- Relationship constraints: No legal, financial, or lifecycle behavior is approved in this documentation baseline.

## PD-019 — Lease Business Rules
- Status: Deferred.
- Approved rules: None. No Lease business rules are approved in this documentation baseline.
- Approved invariants: None. No Lease invariants are approved in this documentation baseline.

## PD-020 — Lease Acceptance Criteria
- Status: Approved.
- Draft conditions required for acceptance:
  - Future Lease-related work remains within the approved documentation boundary.
  - The recorded Lease concept and scope are preserved.
  - The documented Lease-to-Property, Lease-to-Tenant, and Lease-to-Owner relationships are respected.
  - Out-of-scope capabilities are not introduced.
  - Work remains consistent with the approved product baseline and traceability expectations.

## Implementation Authorization
Only the documentation approval status may change in this stage. No Lease aggregate, Lease entity, domain rules, database changes, API changes, or application services are authorized by this record.
