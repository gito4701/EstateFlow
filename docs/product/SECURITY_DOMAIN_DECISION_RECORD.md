# Security Domain Decision Record

## Record Purpose

This decision record captures the documentation-only governance position for security and identity in EstateFlow. It records the approved documentation decisions, deferred decisions, explicit exclusions, and the implementation authorization boundary for the security baseline.

## Recorded Decisions

### Identity
- User identity model — Approved for documentation only. The product baseline recognizes a user identity concept but does not approve any implementation mechanism.
- Authentication expectations — Deferred. The concrete authentication approach remains unresolved and is not approved for implementation.
- Account ownership model — Deferred. The ownership model remains unresolved and is not approved for implementation.

### Authorization
- Roles — Deferred. The role model remains unresolved and is not approved for implementation.
- Permissions — Deferred. The permission model remains unresolved and is not approved for implementation.
- Access boundaries — Approved for documentation only. The baseline documents the need for access boundaries between administrative and non-administrative concerns.
- Administrative capabilities — Deferred. The specific administrative capabilities remain unresolved.

### API security
- Protected endpoints — Deferred. The set of protected endpoints remains unresolved.
- Public endpoints — Approved for documentation only. Operational endpoints may remain publicly accessible in documentation terms.
- Security requirements — Approved for documentation only. Security requirements must be documented before implementation, but no enforcement behavior is approved.

### Operational security
- Audit expectations — Approved for documentation only. Security-relevant audit expectations are documented without approving implementation.
- Security monitoring expectations — Deferred. Monitoring scope and operational ownership remain unresolved.
- Secrets and configuration expectations — Approved for documentation only. Secrets and configuration must be handled through approved practices rather than hard-coded values.

## Explicit Exclusions

No implementation authorization is granted by this record. The following remain excluded from this task and from any implied implementation approval:
- authentication implementation
- authorization implementation
- users
- roles
- permissions
- identity providers
- JWT/session handling
- API security middleware
- database changes

## Implementation Authorization Status

- Documentation approval: Approved.
- Implementation authorization: Not authorized.
- Governance outcome: The security baseline remains documentation-only and is intended to support future implementation planning once Product Owner decisions are recorded.
