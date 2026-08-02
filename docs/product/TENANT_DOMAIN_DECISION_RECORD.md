# Tenant Domain Decision Record

## Purpose
This record captures the Product Owner decisions documented for Tenant management in EstateFlow. The intent is to provide an explicit governance baseline for future implementation planning while keeping the work strictly documentation-only.

## Decision Summary

| Decision ID | Decision Topic | Status | Notes |
| --- | --- | --- | --- |
| PD-011 | Tenant definition | Pending review | Decision requested from Product Owner. |
| PD-012 | Tenant scope | Pending review | Decision requested from Product Owner. |
| PD-013 | Tenant-to-Property relationship | Pending review | Decision requested from Product Owner. |
| PD-014 | Tenant business rules | Pending review | Decision requested from Product Owner. |
| PD-015 | Tenant acceptance criteria | Pending review | Decision requested from Product Owner. |

## PD-011 — Tenant Definition
- Status: Pending review.
- Review request: confirm whether Tenant is defined as a business concept representing the party occupying or paying for a property asset.
- Role within EstateFlow: Tenant is intended to provide a product-level business identity for occupancy context and establish the product boundary for future Tenant planning.

## PD-012 — Tenant Scope
- Status: Pending review.
- Review request: confirm the Tenant scope as a documentation-only product boundary with explicit exclusions for implementation artifacts.
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
- Status: Pending review.
- Review request: confirm whether the Tenant and Property relationship is a business association between a property record and a Tenant record.
- Ownership constraints: Deferred. No occupancy or lease constraint is approved in this documentation baseline.

## PD-014 — Tenant Business Rules
- Status: Pending review.
- Review request: confirm that no Tenant business rules or invariants are approved in this documentation baseline.
- Approved rules: None. No Tenant business rules are approved in this documentation baseline.
- Approved invariants: None. No Tenant invariants are approved in this documentation baseline.

## PD-015 — Tenant Acceptance Criteria
- Status: Pending review.
- Review request: confirm the acceptance criteria requiring Tenant-related work to remain within the approved documentation boundary.
- Conditions required for acceptance:
  - Future Tenant-related work remains within the approved documentation boundary.
  - The approved Tenant concept and scope are preserved.
  - The documented Tenant-to-Property relationship is respected.
  - Out-of-scope capabilities are not introduced.
  - Work remains consistent with the approved product baseline and traceability expectations.

## Implementation Authorization
Only the documentation approval status may change in this stage. No Tenant aggregate, Tenant entity, domain rules, database changes, API changes, or application services are authorized by this record.
