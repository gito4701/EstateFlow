using EstateFlow.Domain.Exceptions;
using EstateFlow.Domain.UserInteractions;
using Xunit;

namespace EstateFlow.Domain.Tests;

public class UserInteractionTests
{
    [Fact]
    public void Create_ShouldCreateUserInteractionWithApprovedConcept()
    {
        var interaction = UserInteraction.Create(UserInteractionId.NewId(), "Sample Interaction", UserInteractionKind.Workflow);

        Assert.NotNull(interaction);
        Assert.Equal("Sample Interaction", interaction.Name);
        Assert.Equal(UserInteractionKind.Workflow, interaction.Kind);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        var ex = Assert.Throws<InvalidUserInteractionException>(() => UserInteraction.Create(UserInteractionId.NewId(), " ", UserInteractionKind.Workflow));
        Assert.Contains("required", ex.Message);
    }

    [Fact]
    public void Create_WithEmptyId_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() => UserInteraction.Create(default, "Sample Interaction", UserInteractionKind.Workflow));
        Assert.Contains("UserInteractionId", ex.Message);
    }
}
