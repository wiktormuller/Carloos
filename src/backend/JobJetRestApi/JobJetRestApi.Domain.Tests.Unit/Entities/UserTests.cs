using FluentAssertions;
using JobJetRestApi.Domain.Entities;
using Xunit.Sdk;

namespace JobJetRestApi.Domain.Tests.Unit.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_WhenCorrectDataIsPassed_ShouldCreateUser()
    {
        // Arrange
        var someEmail = "some@email.com";
        var someUsername = "someUsername";

        // Act
        var user = new User(someEmail, someUsername);

        // Assert
        Assert.Equal("some@email.com", user.Email);
        Assert.Equal("someUsername", user.UserName);
    }

    [Fact]
    public void CreateUser_WhenPassNullEmail_ShouldThrowsException()
    {
        // Arrange
        var nullEmail = (string)null;
        var someUsername = "someUsername";

        // Act
        var action = () => new User(nullEmail, someUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("email");
    }
    
    [Fact]
    public void CreateUser_WhenPassEmptyEmail_ShouldThrowsException()
    {
        // Arrange
        var emptyEmail = string.Empty;
        var someUsername = "someUsername";

        // Act
        var action = () => new User(emptyEmail, someUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("email");
    }
    
    [Fact]
    public void CreateUser_WhenPassNullUsername_ShouldThrowsException()
    {
        // Arrange
        var someEmail = "some@email.com";
        var nullUsername = (string)null;

        // Act
        var action = () => new User(someEmail, nullUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("username");
    }
    
    [Fact]
    public void CreateUser_WhenPassEmptyUsername_ShouldThrowsException()
    {
        // Arrange
        var someEmail = "some@email.com";
        var emptyUsername = string.Empty;

        // Act
        var action = () => new User(someEmail, emptyUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("username");
    }
    
    [Fact]
    public void UpdateUsername_WhenPassNullUsername_ShouldThrowsException()
    {
        // Arrange
        var nullUsername = (string)null;
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.UpdateUsername(nullUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("username");
    }
    
    [Fact]
    public void UpdateUsername_WhenPassEmptyUsername_ShouldThrowsException()
    {
        // Arrange
        var emptyUsername = string.Empty;
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.UpdateUsername(emptyUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("username");
    }
}