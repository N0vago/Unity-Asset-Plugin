using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Chatcloud.CodeBase.Utils
{
    public static class TextUtils
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        public static string GenerateUserId(string tenantId)
        {
            int timestamp = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            string rnd = GenerateRandomString(10);
            return $"{tenantId}_{timestamp}_{rnd}";
        }

        public static string CombineTokensWithSpecialCheckHelper(string current, string[] tokens)
        {
            var result = new StringBuilder(current);
            foreach (var token in tokens)
            {
                if (result.Length > 0)
                    result.Append(" ");
                result.Append(token);
            }

            return result.ToString();
        }

        public static string ConvertMarkdownToHtml(string text)
        {
            // Convert markdown headers (# ... ######) into HTML header tags.
            text = Regex.Replace(text, @"^(#{1,6})\s*(.*)$", match =>
            {
                int level = match.Groups[1].Value.Length;
                string content = match.Groups[2].Value.Trim();
                return $"<h{level}>{content}</h{level}>";
            }, RegexOptions.Multiline);
            // Bold text.
            text = Regex.Replace(text, @"\*\*(.*?)\*\*", "<span style=\"font-weight:600;\">$1</span>");
            // Replace placeholder Ƿ with <br>.
            text = text.Replace("Ƿ", "<br>");
            // Newlines to <br>.
            text = Regex.Replace(text, @"(\r\n|\r|\n)", "<br>");
            // Markdown links.
            text = Regex.Replace(text, @"\[([^\]]+)\]\((https?:\/\/[^\)]+)\)",
                "<a href=\"$2\" target=\"_blank\" rel=\"noopener noreferrer\">$1</a>");
            // Bare URLs.
            text = Regex.Replace(text, @"(^|>)(\s*)((https?:\/\/|www\.)[^\s<]+)", match =>
            {
                string prefix = match.Groups[1].Value;
                string spacing = match.Groups[2].Value;
                string url = match.Groups[3].Value;
                if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    url = "http://" + url;
                return
                    $"{prefix}{spacing}<a href=\"{url}\" target=\"_blank\" rel=\"noopener noreferrer\">{match.Groups[3].Value}</a>";
            }, RegexOptions.IgnoreCase);
            // Emails.
            text = Regex.Replace(text, @"([\w\.-]+@[A-Za-z0-9\.-]+\.[A-Za-z]{2,})", "<a href=\"mailto:$1\">$1</a>");
            return text;
        }
    }
}
