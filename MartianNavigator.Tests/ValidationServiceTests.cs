using MartianNavigator.Services;
using NUnit.Framework;
using System;

namespace MartianNavigator.Tests
{
    public class ValidationServiceTests
    {
        private readonly IValidationService validationService;
        public ValidationServiceTests()
        {
            this.validationService = new ValidationService();
        }

        [Test]
        [TestCase("", typeof(ArgumentNullException), true)]
        [TestCase("TEST", typeof(FormatException), true)]
        [TestCase("5 3", typeof(FormatException), false)]
        public void ValidateRightUpperBoundStringFormat_Test(string value, Type exceptionType, bool shouldThrow)
        {
            if (shouldThrow)
                Assert.Throws(exceptionType, () => this.validationService.ValidateRightUpperBoundStringFormat(value));
            else
                Assert.DoesNotThrow(() => this.validationService.ValidateRightUpperBoundStringFormat(value));
        }

        [Test]
        [TestCase("", typeof(ArgumentNullException), true)]
        [TestCase("TEST", typeof(FormatException), true)]
        [TestCase("1 1 E", typeof(FormatException), false)]
        public void ValidatePositionAndOrientationStringFormat_Test(string value, Type exceptionType, bool shouldThrow)
        {
            if (shouldThrow)
                Assert.Throws(exceptionType, () => this.validationService.ValidatePositionAndOrientationStringFormat(value));
            else
                Assert.DoesNotThrow(() => this.validationService.ValidatePositionAndOrientationStringFormat(value));
        }

        [Test]
        [TestCase("", typeof(ArgumentNullException), true)]
        [TestCase("TEST", typeof(FormatException), true)]
        [TestCase("RFLRFFFR", typeof(FormatException), false)]
        public void ValidateInstructionsStringFormat_Test(string value, Type exceptionType, bool shouldThrow)
        {
            if (shouldThrow)
                Assert.Throws(exceptionType, () => this.validationService.ValidateInstructionsStringFormat(value));
            else
                Assert.DoesNotThrow(() => this.validationService.ValidateInstructionsStringFormat(value));
        }

        [Test]
        [TestCase(typeof(ArgumentOutOfRangeException), 213, true)]
        [TestCase(typeof(ArgumentOutOfRangeException), -1, true)]
        [TestCase(typeof(ArgumentOutOfRangeException), 12, false)]
        public void ValidateCoordinates_Test(Type exceptionType, int coordinate, bool shouldThrow)
        {
            if (shouldThrow)
                Assert.Throws(exceptionType, () => this.validationService.ValidateCoordinates(coordinate));
            else
                Assert.DoesNotThrow(() => this.validationService.ValidateCoordinates(coordinate));
        }
    }
}
