using System.DirectoryServices;
using System.Text.RegularExpressions;

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
            catch (Exception)
            {
                // Optional: log exception
                return null;
            }
        }

        public static (string? Mail, string? UserPrincipalName)? GetEmailFromAd(string userId, string ldapPath, string? bindUser = null, string? bindPassword = null)
        {
            try
            {
                using var entry = string.IsNullOrEmpty(bindUser)
                    ? new DirectoryEntry(ldapPath)
                    : new DirectoryEntry(ldapPath, bindUser, bindPassword);

                using var searcher = new DirectorySearcher(entry)
                {
                    Filter = $"(samAccountName={userId})"
                };

                var result = searcher.FindOne();

                if (result != null)
                {
                    string? mail = null;
                    string? upn = null;

                    if (result.Properties["mail"].Count > 0)
                        mail = result.Properties["mail"][0]?.ToString();

                    if (result.Properties["userPrincipalName"].Count > 0)
                        upn = result.Properties["userPrincipalName"][0]?.ToString();

                    return (mail, upn);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string? DumpUserProperties(string userId, string ldapPath, string? bindUser = null, string? bindPassword = null)
        {
            try
            {
                using var entry = string.IsNullOrEmpty(bindUser)
                    ? new DirectoryEntry(ldapPath)
                    : new DirectoryEntry(ldapPath, bindUser, bindPassword);

                using var searcher = new DirectorySearcher(entry)
                {
                    Filter = $"(samAccountName={userId})"
                };

                var result = searcher.FindOne();
                if (result != null)
                {
                    var allProps = new List<string>();

                    foreach (string propName in result.Properties.PropertyNames)
                    {
                        var values = string.Join(", ", result.Properties[propName].Cast<object>());
                        allProps.Add($"{propName}: {values}");
                    }

                    return string.Join("\n", allProps);
                }

                return "User not found";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string? ExtractPrimarySmtpFromPropertiesDump(string propertiesDump)
        {
            if (string.IsNullOrEmpty(propertiesDump))
                return null;

            var match = Regex.Match(propertiesDump, @"SMTP:([^\s,]+)");
            return match.Success ? match.Groups[1].Value : null;
        }



    }
}
