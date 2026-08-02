# Artifact Storage Strategy

## Purpose
This document defines the approved strategy for storing CI-generated artifacts before any artifact repository, external storage, or infrastructure-backed storage implementation is introduced. It is a documentation-only governance artifact and does not authorize artifact registries, package feeds, container registries, cloud storage, deployment workflows, environment promotion, release automation, secrets integration, authentication, authorization, or infrastructure automation.

## Governance Structure
Each decision below is recorded with one of the following statuses:
- Approved
- Deferred
- Explicitly excluded

## Artifact storage requirements
### Storage purpose
- Status: Approved
- Definition: Storage exists to preserve validated workflow artifacts and supporting evidence for review, traceability, and operational recovery.
- Boundary: Storage is a governance and evidence-management concern only.

### Artifact accessibility expectations
- Status: Approved
- Definition: Stored artifacts should be accessible to the responsible engineering and operations roles that require them for review and release-readiness assessment.
- Boundary: Accessibility is limited to authorized review use and does not imply public or shared distribution.

### Artifact discovery expectations
- Status: Approved
- Definition: Artifact storage should support straightforward discovery by workflow run, build identifier, commit reference, and release association.
- Boundary: Discovery expectations remain simple and reviewable.

### Artifact naming conventions
- Status: Approved
- Definition: Artifacts should use clear names that preserve build context, workflow identity, and release-readiness traceability.
- Boundary: Naming conventions remain lightweight and human-readable.

### Artifact immutability expectations
- Status: Approved
- Definition: Once an artifact is associated with a release-readiness decision or validated evidence record, it should be treated as immutable for that review context.
- Boundary: Immutability is a governance expectation and not a storage-engine feature.

## Storage ownership
### Responsible owner
- Status: Approved
- Definition: Engineering remains responsible for the validated build artifact baseline, while Operations remains responsible for using the artifact in release-readiness and operational review.
- Boundary: Ownership remains a governance role and does not authorize storage-platform implementation.

### Access responsibility
- Status: Approved
- Definition: Access responsibility remains with the accountable engineering and operations roles involved in review and release readiness.
- Boundary: Access responsibility is not delegated to unaffiliated users or automation systems.

### Approval responsibility
- Status: Approved
- Definition: Storage usage for release evidence remains subject to documented engineering and operations approval before it is treated as release evidence.
- Boundary: Approval responsibility remains a human-led governance control.

## Storage options
### CI workflow artifacts
- Status: Approved
- Definition: Workflow artifacts are the selected direction for the current governance baseline because they preserve workflow-level traceability without introducing a new storage platform.
- Boundary: This direction remains limited to the current CI workflow capability and does not authorize external storage or registry adoption.

### Package registry
- Status: Explicitly excluded
- Definition: A package registry is not part of the approved storage strategy for this stage.
- Boundary: Package registry adoption remains outside current scope.

### Container registry
- Status: Explicitly excluded
- Definition: A container registry is not part of the approved storage strategy for this stage.
- Boundary: Container registry adoption remains outside current scope.

### Cloud object storage
- Status: Deferred
- Definition: Cloud object storage may be considered in a future stage if the governance owners approve a broader delivery-storage model.
- Boundary: It remains deferred and not implemented in this governance package.

### Self-hosted storage
- Status: Deferred
- Definition: Self-hosted storage may be considered in a future stage if the governance owners approve a platform-backed storage model.
- Boundary: It remains deferred and not implemented in this governance package.

## Release integration
### How releases reference stored artifacts
- Status: Approved
- Definition: Releases should reference stored artifacts through the workflow run, build identifier, and commit reference that produced the validated artifact.
- Boundary: Release reference remains a review and traceability mechanism only.

### How evidence links to artifacts
- Status: Approved
- Definition: Release evidence should link to the stored artifact by referencing the workflow run ID, build identifier, and commit identifier that produced the validated evidence set.
- Boundary: Evidence linking is a governance and documentation expectation, not a new automation feature.

### How artifact identity is preserved
- Status: Approved
- Definition: Artifact identity should be preserved through clear naming, build metadata, and commit traceability records.
- Boundary: Identity preservation remains a documentation and workflow metadata requirement.

## Security expectations
### Access expectations
- Status: Approved
- Definition: Access to stored artifacts should remain limited to the responsible engineering and operations roles required for review and release readiness.
- Boundary: Access expectations remain governance-only.

### Integrity expectations
- Status: Approved
- Definition: Stored artifacts should retain the integrity of the validated build output and the associated metadata without unauthorized modification.
- Boundary: Integrity is a governance expectation and not a storage-platform implementation requirement.

### Prohibited storage practices
- Status: Explicitly excluded
- Definition: Publishing artifacts to package feeds, container registries, cloud object storage, or any external storage platform is explicitly excluded from this authorization package.
- Boundary: These practices remain outside the approved scope.

## Implementation authorization status
This governance package authorizes documentation and strategy definition only. It does not authorize artifact registries, package feeds, container registries, cloud storage, deployment workflows, environment promotion, release automation, secrets integration, authentication, authorization, or infrastructure automation.

## Promotion governance reference
The release-promotion governance model for this stage is maintained in [docs/engineering/RELEASE_PROMOTION_GOVERNANCE.md](docs/engineering/RELEASE_PROMOTION_GOVERNANCE.md). That package defines the approved promotion sequence, environment progression expectations, evidence requirements, ownership responsibilities, and rollback governance that rely on the artifacts and evidence preserved under this storage strategy.
