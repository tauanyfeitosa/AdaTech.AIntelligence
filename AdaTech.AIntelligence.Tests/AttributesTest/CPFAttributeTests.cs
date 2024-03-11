using AdaTech.AIntelligence.Service.Attributes;
using FluentAssertions;

namespace AdaTech.AIntelligence.Tests.AttributesTest
{
    public class CPFAttributeTests
    {
        private readonly CPFAttribute _sut = new CPFAttribute();

        [Fact]
        public void CPFAttribute_WithValidCPF_ShouldReturnSuccess()
        {
            // Act
            var result = _sut.IsValid("71704834082");

            // Assert  
            result.Should().BeTrue();
        }

        [Fact]
        public void CPFAttribute_WithInvalidCPF_ShouldReturnError()
        {

            // Act
            var result = _sut.IsValid("12345678900");

            // Assert
            result.Should().BeFalse();

        }

        [Fact]
        public void CPFAttribute_WithNullCPF_ShouldReturnError()
        {
            // Act
            var result = _sut.IsValid(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CPFAttribute_WithEmptyCPF_ShouldReturnError()
        {

            // Act
            var result = _sut.IsValid("");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CPFAttribute_WithInvalidCPFFormat_ShouldReturnError()
        {

            // Act
            var result = _sut.IsValid("123");

            // Assert
            result.Should().BeFalse();
        }
    }
}
