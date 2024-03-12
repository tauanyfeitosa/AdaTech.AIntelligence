using System.ComponentModel.DataAnnotations;
using FluentAssertions;

namespace AdaTech.AIntelligence.Service.Attributes.Tests
{
    public class StrongPasswordAttributeTests
    {
        [Fact]
        public void IsValid_WhenPasswordIsNull_ShouldReturnValidationResultWithErrorMessage()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(null, validationContext);

            // Assert
            result.Should().NotBeNull();
            result.ErrorMessage.Should().Be("A senha não pode ser vazia");
        }

        [Fact]
        public void IsValid_WhenPasswordIsLessThanMinimumLength_ShouldReturnValidationResultWithErrorMessage()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "abc123";
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().NotBeNull();
            result.ErrorMessage.Should().Be("A senha deve ter no minimo 8 caracteres");
        }

        [Fact]
        public void IsValid_WhenPasswordDoesNotContainRequiredCombinations_ShouldReturnValidationResultWithErrorMessage()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "onlylowercase";
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().NotBeNull();
            result.ErrorMessage.Should().Be("A senha deve conter ao menos 3 das seguintes combinações: letra maiúscula, letra minúscula, caractere especial ou número.");
        }


        [Fact]
        public void HasRequiredCombinations_ShouldReturnTrueIfPasswordContainsAtLeastThreeCombinations()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "StrongPass123$";

            // Act
            var hasRequiredCombinations = attribute.HasRequiredCombinations(password);

            // Assert
            hasRequiredCombinations.Should().BeTrue();
        }

        [Fact]
        public void HasRequiredCombinations_ShouldReturnFalseIfPasswordDoesNotContainAtLeastThreeCombinations()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "onlylowercase";

            // Act
            var hasRequiredCombinations = attribute.HasRequiredCombinations(password);

            // Assert
            hasRequiredCombinations.Should().BeFalse();
        }

        [Fact]
        public void IsValid_WhenPasswordIsValid_ShouldReturnValidationResultSuccess()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "StrongPass123$";
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().BeEquivalentTo(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_WhenPasswordHasMinimumLengthAndContainsLowercase_ShouldReturnValidationResultSuccess()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "password1$"; 
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().BeEquivalentTo(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_WhenPasswordHasMinimumLengthAndContainsSpecialCharacter_ShouldReturnValidationResultSuccess()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "password123!"; 
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().BeEquivalentTo(ValidationResult.Success);
        }

        [Fact]
        public void IsValid_WhenPasswordHasMinimumLengthAndContainsDigit_ShouldReturnValidationResultSuccess()
        {
            // Arrange
            var attribute = new StrongPasswordAttribute(8);
            var password = "passwd$1"; 
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(password, validationContext);

            // Assert
            result.Should().BeEquivalentTo(ValidationResult.Success);
        }
    }

}
