using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernRestApi.Application.Features.UserFeatures.DeleteUser;
using ModernRestApi.Domain.Common;

namespace ModernRestApi.Tests.Core.Application.Features.UserFeatures.DeleteUser
{
    [TestClass]
    public class DeleteUserValidatorTests
    {
        private DeleteUserValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new DeleteUserValidator();
        }

        private void CommonDeleteUserRequestValidatorTest(string name, bool expectedToBeValid, int expectedErrorCount, string expectedErrorMessage)
        {
            DeleteUserRequest request = new DeleteUserRequest(name);

            var result = _validator.Validate(request);

            Assert.IsTrue(expectedToBeValid ? result.IsValid : !result.IsValid);
            Assert.AreEqual(expectedErrorCount, result.Errors.Count);

            if (expectedErrorCount > 0)
            {
                Assert.IsTrue(result.Errors.Exists(x => x.ErrorMessage == expectedErrorMessage));
            }
        }

        [TestMethod]
        public void Name_Valid()
        {
            CommonDeleteUserRequestValidatorTest("Valid Name", true, 0, string.Empty);
        }

        [TestMethod]
        public void Name_Is_Empty_Invalid()
        {
            CommonDeleteUserRequestValidatorTest(string.Empty, false, 1, ValidationErrorMessages.NameEmpty);
        }

        [TestMethod]
        public void Name_Too_Many_Characters_Invalid()
        {
            CommonDeleteUserRequestValidatorTest(new string('A', 51), false, 1, ValidationErrorMessages.NameMaxReached);
        }

        [TestMethod]
        public void Name_Contains_Numbers_Invalid()
        {
            CommonDeleteUserRequestValidatorTest("Invalid Name 123", false, 1, ValidationErrorMessages.NameLettersSpacesOnly);
        }

        [TestMethod]
        public void Name_Contains_Special_Characters_Invalid()
        {
            CommonDeleteUserRequestValidatorTest("Invalid Name !!!", false, 1, ValidationErrorMessages.NameLettersSpacesOnly);
        }
    }
}
