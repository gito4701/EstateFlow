using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.Properties;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class PropertyTests
{
    [Fact]
    public void Create_ShouldCreatePropertyWithDraftState()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");

        Assert.NotNull(property);
        Assert.Equal(PropertyLifecycleState.Draft, property.State);
    }

    [Fact]
    public void Create_WithoutNameAndAddress_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidPropertyException>(() => Property.Create(PropertyId.NewId(), " ", " "));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Activate_ShouldTransitionFromDraftToActive()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");

        property.Activate();

        Assert.Equal(PropertyLifecycleState.Active, property.State);
    }

    [Fact]
    public void Archive_ShouldTransitionFromActiveToArchived()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        property.Activate();

        property.Archive();

        Assert.Equal(PropertyLifecycleState.Archived, property.State);
    }

    [Fact]
    public void Archive_FromDraft_ShouldBeAllowed()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");

        property.Archive();

        Assert.Equal(PropertyLifecycleState.Archived, property.State);
    }

    [Fact]
    public void Activate_FromArchived_ShouldThrow()
    {
        var property = Property.Create(PropertyId.NewId(), "Sample Property", "123 Main St");
        property.Archive();

        var ex = Assert.Throws<InvalidPropertyStateException>(() => property.Activate());
        Assert.Contains("cannot transition", ex.Message);
    }

    [Fact]
    public void Create_WithInvalidNameOrAddress_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidPropertyException>(() => Property.Create(PropertyId.NewId(), "", ""));
        Assert.Contains("Name", ex.Message);
    }
}
