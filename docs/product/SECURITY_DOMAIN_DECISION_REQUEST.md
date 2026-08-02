# Security Domain Decision Request

## Purpose

This document formalizes the Product Owner decision request for the EstateFlow security and identity product baseline. The request is documentation-only and is intended to establish the approval boundary for future authentication and authorization implementation.

## Requested Decisions

The following decisions require Product Owner review and approval before engineering may implement security capabilities:
- Authentication approach
- User/account model
- Roles
- Permissions
- Administrative access
- Tenant/owner access expectations
- API protection expectations
- Audit/security requirements

## Decision Context

EstateFlow currently has a documented platform baseline for Property and operational hardening, but it does not yet have an approved security governance baseline. This request creates the required decision package so future implementation can proceed only after explicit product approval.

## Constraints

This request does not authorize implementation of:
- authentication
- authorization
- users table
- identity provider integration
- JWT/session handling
- roles/permissions code
- API security changes
- database changes

## Review Status

Pending Product Owner decision.
