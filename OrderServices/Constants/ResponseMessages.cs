namespace OrderServices.Constants
{
    public class ResponseMessages
    {
        public const string userRegister = "User Registered Successfully.";
        public const string emailExists = "User with this email already exists.";
        public const string failToLoadJWT = "Failed to generate JWT token";
        public const string productNotFound = "Product not found.";
        public const string userNotFound = "User not found.";
        public const string addedToCart = "Product added to cart.";
        public const string removeItem = "Item removed successfully.";
        public const string itemNotFound = "Cart item not found.";
        public const string internalServerErrorMessage = "Internal Server Error.";
        public const string secretKeyCntBeNull = "Secret key cannot be null or empty.";
    }
}
