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

        public static string ConvertMarkdownToTmp(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            string result = text;

            // Заголовки: #, ##, ###
            result = Regex.Replace(result, @"^# (.+)$", "<size=150%><b>$1</b></size>", RegexOptions.Multiline);
            result = Regex.Replace(result, @"^## (.+)$", "<size=130%><b>$1</b></size>", RegexOptions.Multiline);
            result = Regex.Replace(result, @"^### (.+)$", "<size=115%><b>$1</b></size>", RegexOptions.Multiline);

            // Ссылки [текст](url) → курсивный текст (можно кастомизировать)
            result = Regex.Replace(result, @"\[(.+?)\]\((.+?)\)", "<i>$1</i>");

            // Жирный: **text**
            result = Regex.Replace(result, @"\*\*(.+?)\*\*", "<b>$1</b>");

            // Всё остальное в *text* или _text_ → жирный (кроме уже заменённой ссылки)
            result = Regex.Replace(result, @"(?<!\*)\*(?!\*)(.+?)(?<!\*)\*(?!\*)", "<b>$1</b>");
            result = Regex.Replace(result, @"_(.+?)_", "<b>$1</b>");

            // Код: `text`
            result = Regex.Replace(result, @"`(.+?)`", "<color=#aaaaaa><i>$1</i></color>");

            // Списки: - item / * item / + item
            result = Regex.Replace(result, @"^(\s*)[-*+] (.+)$", "$1• $2", RegexOptions.Multiline);

            result = result.Replace("\\", "");

            return result;
        }
    }
}
