namespace ModernRestApi.Domain.Common
{
    public static class ValidationErrorMessages
    {
        public const string NameEmpty = "Name can not be empty.";
        public const string NameMaxReached = "Only a maximum of 50 characters is allowed for the Name field.";
        public const string NameLettersSpacesOnly = "Name can only contain upper and lower case letters.";
        public const string AddressEmpty = "Address can not be empty.";
        public const string AddressMinLength = "Address must have at least 3 characters in length.";
        public const string AddressMaxReached = "Address can not exceed 50 characters in length.";
        public const string NoDuplicatesAllowed = "No duplicate names allowed.";
        public const string NoUserExists = "No user exists with this name.";
    }
}
