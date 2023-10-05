namespace Client.Shared.Utilities
{
    public static class GlobalAuthState
    {
        public static uint? UserId { get; set; }
        public static string? Name { get; set;}
        public static bool isAdmin { get; set; } = false;
    }
}
