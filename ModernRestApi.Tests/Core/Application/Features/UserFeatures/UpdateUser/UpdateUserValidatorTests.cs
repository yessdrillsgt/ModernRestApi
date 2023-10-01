using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.UpdateUser;
using ModernRestApi.Domain.Common;

namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.CreateUser
{
    [TestClass]
    public class UpdateUserValidatorTests
    {
        private UpdateUserValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateUserValidator();
        }

        private void CommonUpdateUserRequestValidatorTest(string name, string address, bool expectedToBeValid, int expectedErrorCount, string expectedErrorMessage)
        {
            UpdateUserRequest request = new UpdateUserRequest(name, address);

            var result = _validator.Validate(request);

            Assert.IsTrue(expectedToBeValid ? result.IsValid : !result.IsValid);
            Assert.AreEqual(expectedErrorCount, result.Errors.Count);

            if (expectedErrorCount > 0)
            {
                Assert.IsTrue(result.Errors.Exists(x => x.ErrorMessage == expectedErrorMessage));
            }
        }

        [TestMethod]
        public void Name_Address_Valid()
        {
            CommonUpdateUserRequestValidatorTest("Valid Name", "123 Valid Address", true, 0, string.Empty);
        }

        [TestMethod]
        public void Name_Is_Empty_Invalid()
        {
            CommonUpdateUserRequestValidatorTest(string.Empty, "123 Valid Address", false, 1, ValidationErrorMessages.NameEmpty);
        }

        [TestMethod]
        public void Name_Too_Many_Characters_Invalid()
        {
            CommonUpdateUserRequestValidatorTest(new string('A', 51), "123 Valid Address", false, 1, ValidationErrorMessages.NameMaxReached);
        }

        [TestMethod]
        public void Name_Contains_Numbers_Invalid()
        {
            CommonUpdateUserRequestValidatorTest("Invalid Name 123", "123 Valid Address", false, 1, ValidationErrorMessages.NameLettersSpacesOnly);
        }

        [TestMethod]
        public void Name_Contains_Special_Characters_Invalid()
        {
            CommonUpdateUserRequestValidatorTest("Invalid Name !!!", "123 Valid Address", false, 1, ValidationErrorMessages.NameLettersSpacesOnly);
        }

        [TestMethod]
        public void Address_Is_Empty_Invalid()
        {
            CommonUpdateUserRequestValidatorTest("Valid Name", string.Empty, false, 2, ValidationErrorMessages.AddressEmpty);
        }

        [TestMethod]
        public void Address_Has_Too_Few_Characters_Invalid()
        {
            CommonUpdateUserRequestValidatorTest("Valid Name", "B1", false, 1, ValidationErrorMessages.AddressMinLength);
        }

        [TestMethod]
        public void Address_Has_Too_Many_Characters_Invalid()
        {
            CommonUpdateUserRequestValidatorTest("Valid Name", new string('A', 51), false, 1, ValidationErrorMessages.AddressMaxReached);
        }
    }
}
