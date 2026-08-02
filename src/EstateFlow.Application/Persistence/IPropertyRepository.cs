using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EstateFlow.Domain.Properties;

namespace EstateFlow.Application.Persistence;

public sealed record SearchPropertiesResult(IReadOnlyList<Property> Properties, int Page, int PageSize, int TotalCount);
public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Property>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Property>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Property aggregate, CancellationToken cancellationToken = default);

    Task UpdateAsync(Property aggregate, CancellationToken cancellationToken = default);

    Task DeleteAsync(Property aggregate, CancellationToken cancellationToken = default);

    Task<SearchPropertiesResult> SearchAsync(string? name, PropertyLifecycleState? status, int page, int pageSize, string? sortField, string? sortDirection, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PropertyAuditEntry>> GetPropertyAuditEntriesAsync(PropertyId id, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<PropertyAuditEntry>>(Array.Empty<PropertyAuditEntry>());
}
