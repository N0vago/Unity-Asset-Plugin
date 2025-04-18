using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Chatcloud.CodeBase.Utils
{
    public static class TextUtils
    {

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
            text = Regex.Replace(text, @"^(#{1,6})\s*(.*)$", match =>
            {
                int level = match.Groups[1].Value.Length;
                string content = match.Groups[2].Value.Trim();
                return $"<h{level}>{content}</h{level}>";
            }, RegexOptions.Multiline);
            
            text = Regex.Replace(text, @"\*\*(.*?)\*\*", "<span style=\"font-weight:600;\">$1</span>");
            
            text = text.Replace("Ƿ", "<br>");
            
            text = Regex.Replace(text, @"(\r\n|\r|\n)", "<br>");
            
            text = Regex.Replace(text, @"\[([^\]]+)\]\((https?:\/\/[^\)]+)\)",
                "<a href=\"$2\" target=\"_blank\" rel=\"noopener noreferrer\">$1</a>");
            
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
