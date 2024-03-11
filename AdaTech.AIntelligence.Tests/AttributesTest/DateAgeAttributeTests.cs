using AdaTech.AIntelligence.Service.Attributes;
using FluentAssertions;

namespace AdaTech.AIntelligence.Tests.AttributesTest
{
    public class DateAgeAttributeTests
    {
        [Fact]
        public void IsValid_Should_Return_True_When_Age_Is_Equal_To_MinimumAge()
        {
            //Arrange
            var sut = new DateAgeAttribute(18);
            var dateOfBirth = new DateOnly(DateTime.Today.Year - 18, DateTime.Today.Month, DateTime.Today.Day);

            //act
            var result = sut.IsValid(dateOfBirth);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_Should_Return_True_When_Age_Is_Valid()
        {
            //Arrange
            var sut = new DateAgeAttribute(18);
            var dateOfBirth = new DateOnly(DateTime.Today.Year - 19, DateTime.Today.Month, DateTime.Today.Day);
            //act
            var result = sut.IsValid(dateOfBirth);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_Should_Return_False_When_Age_Is_Less_Than_MinimumAge()
        {
            //Arrange
            var sut = new DateAgeAttribute(18);
            var dateOfBirth = new DateOnly(DateTime.Today.Year - 17, DateTime.Today.Month, DateTime.Today.Day);

            //act
            var result = sut.IsValid(dateOfBirth);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_Should_Return_False_When_Value_Is_Not_DateOnly()
        {
            //Arrange
            var sut = new DateAgeAttribute(18);
            var value = "11 de março de 2024";
            //act
            var validationResult = sut.IsValid(value);
            // Assert
            validationResult.Should().BeFalse();
        }
    }
}
