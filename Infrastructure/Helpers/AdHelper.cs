using System.DirectoryServices;

namespace ClassRegistrationApplication2025.Infrastructure.Helpers
{
    public static class AdHelper
    {
        public static string? GetDisplayNameFromAd(string userId, string ldapPath)
        {
            try
            {
                using var entry = new DirectoryEntry(ldapPath);
                using var searcher = new DirectorySearcher(entry)
                {
                    Filter = $"(samAccountName={userId})"
                };

                searcher.PropertiesToLoad.Add("cn");

                var result = searcher.FindOne();
                if (result != null && result.Properties["cn"].Count > 0)
                {
                    return result.Properties["cn"][0]?.ToString();
                }

                return null;
            }
            catch (Exception ex)
            {
                // Optional: log exception
                return null;
            }
        }
    }
}
