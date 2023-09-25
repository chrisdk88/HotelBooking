namespace Client.Shared.Utilities
{
    public static class GlobalAuthState
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static int UserId { get; set; } = -1; // Example: You can store the user ID if needed
    }
}
