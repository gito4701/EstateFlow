# Tenant Product Definition Baseline

## Purpose of Tenant in EstateFlow
Tenant is a product concept introduced in EstateFlow v1 to represent the party occupying or paying for a property asset. This baseline defines Tenant as a documentation-only business boundary for future planning.

## Scope of Tenant within EstateFlow v1
This baseline defines Tenant at the product level only. It covers the approved understanding of Tenant as a business concept related to property occupancy and the Tenant-to-Property relationship. It does not authorize Tenant domain entities, persistence design, API contracts, or implementation work.

## Relationship Between Tenant and Property
The Tenant and Property relationship is defined as a business association between a property record and a Tenant record. This baseline documents the intended association only; it does not define tenancy workflow, lease terms, or data model implementation.

## Approved Capabilities
The following capabilities are approved for the Tenant product baseline:
- Define Tenant as an explicit business concept within EstateFlow.
- Describe the intended relationship between Tenant and Property.
- Establish a documented product boundary for future Tenant planning.
- Define acceptance principles for future Tenant-related work.

## Explicit Exclusions
The following items are explicitly excluded from this baseline:
- Domain entities for Tenant.
- Database tables, persistence design, or storage.
- API endpoints or contract changes.
- Application services, workflows, or business-rule implementation.
- Authentication, authorization, billing, or lease management.
- Any implementation detail not explicitly required to define the product boundary.

## Acceptance Principles
Tenant-related work is considered acceptable only when it remains within this approved documentation boundary. Any future implementation must:
- Preserve the approved Tenant concept and scope.
- Respect the documented Tenant-to-Property relationship.
- Avoid introducing out-of-scope capabilities.
- Remain consistent with the approved product baseline and traceability expectations.

## Product Boundary Summary
This document establishes the Tenant product boundary only. It does not authorize implementation, persistence design, or functional delivery beyond the documented baseline.

## Product Owner Decision Status
The Tenant baseline has received Product Owner decisions for PD-011 through PD-015. PD-011, PD-012, PD-013, and PD-015 are approved; PD-014 is deferred. This document records the approved documentation baseline only and does not authorize Tenant implementation work.
