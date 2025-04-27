using System.Text.RegularExpressions;

namespace Chatcloud.CodeBase.Utils
{
    /// <summary>
    /// Provides utility methods for text processing.
    /// </summary>
    public static class TextUtils
    {
        /// <summary>
        /// Converts Markdown text to TextMeshPro-compatible markup.
        /// </summary>
        /// <param name="text">The Markdown text to convert.</param>
        /// <returns>The converted text with TMP markup.</returns>
        public static string ConvertMarkdownToTmp(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            string result = text;

            // Headers: #, ##, ###
            result = Regex.Replace(result, @"^# (.+)$", "<size=150%><b>$1</b></size>", RegexOptions.Multiline);
            result = Regex.Replace(result, @"^## (.+)$", "<size=130%><b>$1</b></size>", RegexOptions.Multiline);
            result = Regex.Replace(result, @"^### (.+)$", "<size=115%><b>$1</b></size>", RegexOptions.Multiline);

            // Links: [text](url) -> italicized text
            result = Regex.Replace(result, @"\[(.+?)\]\((.+?)\)", "<i>$1</i>");

            // Bold: **text**
            result = Regex.Replace(result, @"\*\*(.+?)\*\*", "<b>$1</b>");

            // Italic or bold: *text* or _text_ -> bold
            result = Regex.Replace(result, @"(?<!\*)\*(?!\*)(.+?)(?<!\*)\*(?!\*)", "<b>$1</b>");
            result = Regex.Replace(result, @"_(.+?)_", "<b>$1</b>");

            // Code: `text`
            result = Regex.Replace(result, @"`(.+?)`", "<color=#aaaaaa><i>$1</i></color>");

            // Lists: - item / * item / + item
            result = Regex.Replace(result, @"^(\s*)[-*+] (.+)$", "$1• $2", RegexOptions.Multiline);

            // Remove backslashes
            result = result.Replace("\\", "");

            return result;
        }
    }
}