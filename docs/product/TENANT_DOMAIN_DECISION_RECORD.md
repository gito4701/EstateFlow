# Tenant Domain Decision Record

## Purpose
This record captures the Product Owner decisions documented for Tenant management in EstateFlow. The intent is to provide an explicit governance baseline for future implementation planning while keeping the work strictly documentation-only.

## Decision Summary

| Decision ID | Decision Topic | Status | Notes |
| --- | --- | --- | --- |
| PD-011 | Tenant definition | Approved | Recorded as the business meaning of Tenant within EstateFlow v1. |
| PD-012 | Tenant scope | Approved | Recorded as a documentation-only product boundary with explicit implementation exclusions. |
| PD-013 | Tenant-to-Property relationship | Approved | Recorded as a business association between a property record and a Tenant record. |
| PD-014 | Tenant business rules | Deferred | No Tenant business rules or lifecycle behavior are approved in this baseline. |
| PD-015 | Tenant acceptance criteria | Approved | Recorded as acceptance expectations for documentation-only Tenant work. |

## PD-011 — Tenant Definition
- Status: Approved.
- Approved Tenant concept: Tenant is a business concept representing the party occupying or paying for a property asset within EstateFlow v1.
- Role within EstateFlow: Tenant provides the product-level business identity for occupancy context and establishes the documented product boundary for future Tenant planning.

## PD-012 — Tenant Scope
- Status: Approved.
- Approved scope: Tenant is in scope as a documentation-only product boundary with explicit exclusions for implementation artifacts.
- Included capabilities:
  - Define Tenant as an explicit business concept within EstateFlow.
  - Describe the intended relationship between Tenant and Property.
  - Establish a documented product boundary for future Tenant planning.
  - Define acceptance principles for future Tenant-related work.
- Excluded capabilities:
  - Domain entities for Tenant.
  - Database tables, persistence design, or storage.
  - API endpoints or contract changes.
  - Application services, workflows, or business-rule implementation.
  - Authentication, authorization, billing, or lease management.
  - Any implementation detail not explicitly required to define the product boundary.

## PD-013 — Tenant-to-Property Relationship
- Status: Approved.
- Approved relationship meaning: The Tenant and Property relationship is a business association between a property record and a Tenant record.
- Relationship constraints: Deferred. No occupancy or lease constraint is approved in this documentation baseline.

## PD-014 — Tenant Business Rules
- Status: Deferred.
- Approved rules: None. No Tenant business rules are approved in this documentation baseline.
- Approved invariants: None. No Tenant invariants are approved in this documentation baseline.
- Approved context retained: Tenant relationship context is documented and preserved as the approved tenant context for future planning.
- Review package: The Product Owner review package for this decision is documented in `docs/product/OWNER_TENANT_BUSINESS_RULES_REVIEW.md` and covers tenant lifecycle expectations, tenant/property relationship rules, tenant/lease relationship expectations, tenant constraints, and acceptance criteria.

## PD-015 — Tenant Acceptance Criteria
- Status: Approved.
- Conditions required for acceptance:
  - Future Tenant-related work remains within the approved documentation boundary.
  - The approved Tenant concept and scope are preserved.
  - The documented Tenant-to-Property relationship is respected.
  - Out-of-scope capabilities are not introduced.
  - Work remains consistent with the approved product baseline and traceability expectations.

## Implementation Authorization
Only the documentation approval status may change in this stage. No Tenant aggregate, Tenant entity, domain rules, database changes, API changes, or application services are authorized by this record.
