# Security Domain Decision Record

## Record Purpose

This decision record captures the documentation-only governance position for security and identity in EstateFlow. It records the current product baseline and the pending Product Owner decisions required before any authentication or authorization implementation may proceed.

## Recorded Decisions

### Approved for documentation only
- Security and identity are recognized as necessary platform capabilities for future EstateFlow maturity.
- Identity and access control must be defined as a governed product boundary rather than inferred from implementation convenience.
- The current baseline must remain documentation-only until Product Owner approval is captured for the key security decisions.

### Pending Product Owner approval
- Authentication approach
- User/account model
- Roles
- Permissions
- Administrative access
- Tenant/owner access expectations
- API protection expectations
- Audit/security requirements

## Explicit Exclusions

No implementation authorization is granted by this record. The following remain excluded from this task and from any implied implementation approval:
- authentication implementation
- authorization implementation
- users table
- identity provider integration
- JWT/session handling
- roles/permissions code
- API security changes
- database changes

## Governance Outcome

The security baseline remains documentation-only and is intended to support future implementation planning once Product Owner decisions are recorded.
