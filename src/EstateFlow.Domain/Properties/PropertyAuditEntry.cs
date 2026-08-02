using System;

namespace EstateFlow.Domain.Properties;

public sealed class PropertyAuditEntry
{
    public Guid Id { get; private set; }
    public PropertyId PropertyId { get; private set; }
    public PropertyAuditOperation Operation { get; private set; }
    public DateTimeOffset OccurredAtUtc { get; private set; }
    public string? Details { get; private set; }

    private PropertyAuditEntry(Guid id, PropertyId propertyId, PropertyAuditOperation operation, DateTimeOffset occurredAtUtc, string? details)
    {
        Id = id;
        PropertyId = propertyId;
        Operation = operation;
        OccurredAtUtc = occurredAtUtc;
        Details = details;
    }

    public static PropertyAuditEntry Create(PropertyId propertyId, PropertyAuditOperation operation, string? details = null)
    {
        return new PropertyAuditEntry(Guid.NewGuid(), propertyId, operation, DateTimeOffset.UtcNow, details);
    }
}

public enum PropertyAuditOperation
{
    Create,
    Update,
    Delete
}
