namespace WealthSync.Tests.Domain.Aggregates.Household;

using System;
using Xunit;
using WealthSync.Domain.Aggregates.Household;

public class HouseholdTests
{
    [Fact]
    public void HouseholdValidCreateTest()
    {
        var householdName = "Nelson's Household";
        
        var household = new Household(householdName);
        
        Assert.NotNull(household);
        Assert.Equal(householdName, household.Name);
        
        Assert.NotEqual(Guid.Empty, household.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void HouseholdInvalidNameTest(String invalidName)
    {
        var exceptionMensage = Assert.Throws<ArgumentException>(() =>
            new Household(invalidName));
        
        Assert.Equal("Invalid Household Name", exceptionMensage.Message);
    }
}