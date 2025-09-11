using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain.ValueObjects
{
    /// <summary>
    /// Unit tests for ManufactureYear value object.
    /// Tests business rules in isolation without dependencies.
    /// </summary>
    public sealed class ManufactureYearTests
    {
        [Fact]
        public void Constructor_WithValidCurrentYear_ShouldCreateSuccessfully()
        {
            // Arrange
            var currentYear = DateTime.Now.Year;

            // Act
            var manufactureYear = new ManufactureYear(currentYear);

            // Assert
            Assert.Equal(currentYear, manufactureYear.Value);
            Assert.Equal(0, manufactureYear.AgeInYears);
            Assert.True(manufactureYear.IsEligibleForFleet);
        }

        [Fact]
        public void Constructor_WithValidYearWithin5Years_ShouldCreateSuccessfully()
        {
            // Arrange
            var validYear = DateTime.Now.Year - 3; // 3 years old

            // Act
            var manufactureYear = new ManufactureYear(validYear);

            // Assert
            Assert.Equal(validYear, manufactureYear.Value);
            Assert.Equal(3, manufactureYear.AgeInYears);
            Assert.True(manufactureYear.IsEligibleForFleet);
        }

        [Fact]
        public void Constructor_WithYearExactly5YearsOld_ShouldCreateSuccessfully()
        {
            // Arrange
            var validYear = DateTime.Now.Year - 5; // Exactly 5 years old

            // Act
            var manufactureYear = new ManufactureYear(validYear);

            // Assert
            Assert.Equal(validYear, manufactureYear.Value);
            Assert.Equal(5, manufactureYear.AgeInYears);
            Assert.True(manufactureYear.IsEligibleForFleet);
        }

        [Fact]
        public void Constructor_WithYearOlderThan5Years_ShouldThrowDomainException()
        {
            // Arrange
            var invalidYear = DateTime.Now.Year - 6; // 6 years old (too old)

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new ManufactureYear(invalidYear));
            Assert.Contains("exceeds maximum age of 5 years", exception.Message);
            Assert.Contains(invalidYear.ToString(), exception.Message);
        }

        [Fact]
        public void Constructor_WithFutureYear_ShouldThrowDomainException()
        {
            // Arrange
            var futureYear = DateTime.Now.Year + 1;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new ManufactureYear(futureYear));
            Assert.Contains("Invalid manufacture year", exception.Message);
        }

        [Fact]
        public void Constructor_WithYearTooOld_ShouldThrowDomainException()
        {
            // Arrange
            var tooOldYear = 1800;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new ManufactureYear(tooOldYear));
            Assert.Contains("Invalid manufacture year", exception.Message);
        }

        [Theory]
        [InlineData(2023, 2023, true)]
        [InlineData(2020, 2020, true)]
        [InlineData(2019, 2019, true)]
        public void Equals_WithSameYear_ShouldReturnTrue(int year1, int year2, bool expected)
        {
            // Arrange
            var manufactureYear1 = new ManufactureYear(year1);
            var manufactureYear2 = new ManufactureYear(year2);

            // Act
            var result = manufactureYear1.Equals(manufactureYear2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equals_WithDifferentYear_ShouldReturnFalse()
        {
            // Arrange
            var currentYear = DateTime.Now.Year;
            var manufactureYear1 = new ManufactureYear(currentYear);
            var manufactureYear2 = new ManufactureYear(currentYear - 1);

            // Act
            var result = manufactureYear1.Equals(manufactureYear2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ToString_ShouldReturnYearAsString()
        {
            // Arrange
            var year = DateTime.Now.Year;
            var manufactureYear = new ManufactureYear(year);

            // Act
            var result = manufactureYear.ToString();

            // Assert
            Assert.Equal(year.ToString(), result);
        }

        [Fact]
        public void GetHashCode_WithSameYear_ShouldReturnSameHashCode()
        {
            // Arrange
            var year = DateTime.Now.Year;
            var manufactureYear1 = new ManufactureYear(year);
            var manufactureYear2 = new ManufactureYear(year);

            // Act
            var hashCode1 = manufactureYear1.GetHashCode();
            var hashCode2 = manufactureYear2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }
    }
}

