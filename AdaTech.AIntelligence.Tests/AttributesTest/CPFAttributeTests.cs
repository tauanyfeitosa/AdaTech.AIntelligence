using AdaTech.AIntelligence.Service.Attributes;
using FluentAssertions;

namespace AdaTech.AIntelligence.Tests.AttributesTest
{
    public class CPFAttributeTests
    {
        private readonly CPFAttribute _sut;

        public CPFAttributeTests()
        {
            _sut = new CPFAttribute();
        }

        [Fact]
        public void CPFAttribute_WithValidCPF_ShouldReturnTrue()
        {
            // Act
            var result = _sut.IsValid("71704834082");

            // Assert  
            result.Should().BeTrue();
        }

        [Fact]
        public void CPFAttribute_WithInvalidCPF_ShouldReturnFalse()
        {

            // Act
            var result = _sut.IsValid("12345678900");

            // Assert
            result.Should().BeFalse();

        }

        [Fact]
        public void CPFAttribute_WithNullCPF_ShouldReturnFalse()
        {
            // Act
            var result = _sut.IsValid(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CPFAttribute_WithEmptyCPF_ShouldReturnFalse()
        {

            // Act
            var result = _sut.IsValid("");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123456789012")]
        public void CPFAttribute_WithInvalidCPFFormat_ShouldReturnFalse(string cpf)
        {
            // Act
            var result = _sut.IsValid(cpf);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CPFAttribute_WithValidCPFWithDashes_ShouldReturnTrue()
        {
            // Act
            var result = _sut.IsValid("717.048.340-82");

            // Assert  
            result.Should().BeTrue();
        }

        [Fact]
        public void CPFAttribute_WithInvalidCPFCharacter_ShouldReturnFalse()
        {
            // Act
            var result = _sut.IsValid("1234567890a");

            // Assert
            result.Should().BeFalse();
        }

    }
}
