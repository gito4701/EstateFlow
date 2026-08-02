# Deployment Operations Runbook

## Purpose
This runbook defines the current operational deployment expectations for EstateFlow based on the validated runtime, configuration, health, and deployment posture. It is a documentation-only runbook and does not authorize implementation work, new infrastructure automation, CI/CD automation, authentication, authorization, or secrets-vault integration.

## Scope and Boundaries
This runbook applies to the current EstateFlow baseline for:
- local deployment validation
- container-based deployment review
- runtime startup verification
- basic operational diagnostics

This runbook does not cover:
- CI/CD pipelines
- cloud resources
- authentication or authorization implementation
- secrets-vault integration
- infrastructure automation
- business-feature delivery

## Deployment Preparation
### Required environment configuration
Before deployment, confirm the target environment has the required runtime values available:
- ASPNETCORE_ENVIRONMENT should be set to the intended environment name such as Development, Test, or Production.
- The persistence provider must be set explicitly through the Persistence:Provider setting.
- If SqlServer is selected, a connection string must be provided through the Persistence:ConnectionString setting.
- The ConnectionStrings:DefaultConnection value should be kept aligned with the persistence value for local and review environments.

### Required runtime settings
The current runtime baseline expects the following configuration values to be available:
- ApiBehavior:Title
- ApiBehavior:Detail
- Persistence:Provider
- Persistence:ConnectionString when using SqlServer

### Configuration sources
The current runtime configuration order is:
1. appsettings.json
2. appsettings.{Environment}.json
3. environment variables

Operator guidance:
- Prefer runtime environment variables for values that should not be stored in the repository.
- Keep repository files limited to safe defaults and environment-specific examples.
- Do not treat repository content as the source of truth for production secrets.

### Startup validation expectations
The application should be considered ready only when:
- the API process starts without configuration validation errors
- the health endpoint responds successfully
- the readiness endpoint responds successfully
- request logging and correlation metadata are emitted

## Deployment Process
### Local deployment flow
1. Restore the solution dependencies.
2. Build the solution.
3. Start the API with the intended environment configuration.
4. Verify the root endpoint and health/readiness endpoints.
5. Review logging output for startup and request processing.

### Container deployment flow
1. Build the container image from the repository Dockerfile.
2. Start the container with the intended environment variables.
3. Ensure the container exposes the expected port and that the runtime configuration is supplied through the environment.
4. Verify the health and readiness endpoints from the containerized runtime.
5. Review log output for startup and operational behavior.

### Startup verification
Expected startup signals:
- the API host starts without a startup exception
- the configured middleware pipeline is active
- logging output begins to record requests

### Health verification
Use the health endpoint to verify the process is responsive.
- Health endpoint: /health
- Readiness endpoint: /health/ready

### Readiness verification
The readiness endpoint should respond successfully when the runtime is available for request handling. If the readiness endpoint does not return success, review the configuration, persistence provider selection, and startup logs before proceeding.

## Operational Procedures
### Reviewing logs
Review logs for:
- startup exceptions
- configuration validation failures
- middleware failures
- request-processing failures

Log review should focus on:
- the startup sequence
- exception messages
- correlation identifiers
- persistence-related errors

### Correlation ID troubleshooting
When an incident or request issue is reported:
1. Locate the request correlation ID in the log entry.
2. Search for the same correlation ID across the relevant runtime logs.
3. Review the full request lifecycle around that correlation ID.
4. Correlate the request with any startup, persistence, or middleware errors.

### Audit inspection
The current baseline provides request logging and middleware-based diagnostics, which are suitable for basic audit-style review. Operators should inspect:
- request completion status
- request trace context
- error responses
- startup activity

### Common startup failures
Common startup issues include:
- missing required configuration values
- invalid persistence provider selection
- missing SqlServer connection string for SqlServer mode
- invalid or malformed environment variables

### Configuration failures
Configuration failures are typically the result of:
- missing values in the target environment
- mismatched provider and connection-string values
- environment variable names that do not match the expected configuration structure

## Maintenance
### Environment promotion guidance
Promotion between environments should be handled by reviewing the configuration file and runtime environment values for each environment:
- Development: suitable for local and review use
- Test: suitable for automated validation and isolated testing
- Production: should be treated as controlled runtime configuration only

### Configuration change process
Configuration changes should follow this order:
1. Review the change request against the current configuration governance boundaries.
2. Update the target environment values or configuration files as appropriate.
3. Validate startup and health/readiness behavior after the change.
4. Review logs and request traces for regressions.

### Rollback considerations
If a configuration change causes startup or runtime issues:
1. Restore the previous environment values.
2. Restart the application.
3. Re-run health and readiness verification.
4. Review logs for the previous successful state.

### Operational ownership expectations
Operational ownership for this baseline should remain with the team responsible for runtime support and environment configuration. The current runbook assumes:
- runtime configuration is maintained by the deployment or operations owner
- application startup validation is performed before release or promotion
- environment-specific secrets are managed outside the repository

## Known Limitations
- Docker validation remains environment-dependent and may not be available in every deployment environment.
- Authentication is not implemented.
- Authorization is not implemented.
- Secrets management implementation remains pending.

## Operational Exit Criteria
The deployment can be considered operationally acceptable for this review stage when:
- the application starts successfully
- configuration validation passes
- health and readiness endpoints respond
- logs and correlation identifiers are available for troubleshooting
- no unexpected startup or configuration errors are present
