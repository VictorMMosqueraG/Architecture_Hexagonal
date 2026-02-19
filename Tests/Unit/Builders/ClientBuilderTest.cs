namespace Tests.Unit.Builders;

using Core.Constants;
using FluentAssertions;
using Tests.Builders;
using Xunit;

public class ClientBuilderTests
{
    [Fact]
    public void Build_WithAllFields_ShouldSetCorrectly()
    {
        var client = new ClientBuilder()
            .WithId("123")
            .WithName("Carlos")
            .WithEmail("carlos@example.com")
            .WithDocumentNumber("999")
            .WithPhone("3001111111")
            .WithStatus(ClientStatus.Active)
            .Build();

        client.Id.Should().Be("123");
        client.Name.Should().Be("Carlos");
        client.Email.Should().Be("carlos@example.com");
        client.DocumentNumber.Should().Be("999");
        client.Phone.Should().Be("3001111111");
        client.Status.Should().Be(ClientStatus.Active);
    }

    [Fact]
    public void Default_ShouldHaveActiveStatus()
    {
        var client = ClientBuilder.Default();

        client.Status.Should().Be(ClientStatus.Active);
    }

    [Fact]
    public void Build_WithSuspendedStatus_ShouldOverrideDefault()
    {
        var client = new ClientBuilder()
            .WithStatus(ClientStatus.Suspended)
            .Build();

        client.Status.Should().Be(ClientStatus.Suspended);
    }

    [Fact]
    public void Build_WithNullPhone_ShouldAllowNull()
    {
        var client = new ClientBuilder()
            .WithPhone(null)
            .Build();

        client.Phone.Should().BeNull();
    }

    [Fact]
    public void Default_ShouldHaveNonEmptyId()
    {
        var client = ClientBuilder.Default();

        client.Id.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Default_ShouldHaveValidEmail()
    {
        var client = ClientBuilder.Default();

        client.Email.Should().Contain("@");
    }
}