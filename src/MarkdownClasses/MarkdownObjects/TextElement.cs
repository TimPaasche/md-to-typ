using MarkdownClasses.MarkdownObjects.TextElements;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class TextElement : MarkdownObject
{

}

public static class TextElementExtensions
{
    // private const string REGEX_PATTERN = @"\$(.+?)\$|`(.+?)`|\*\*\*(.+?)\*\*\*|___(.+?)___|\*\*_(.+?)_\*\*|__\*(.+?)\*__|\*\*(.+?)\*\*|__(.+?)__|\*(.+?)\*|_(.+?)_|\[(.*?)\]\((.+?)\)|~~(.+?)~~|!\[(.*?)\]\((.+?)\)|:([\d\w_]+?):";
    private const string REGEX_PATTERN = @"\$(.+?)\$|`(.+?)`|([_\*]{3}.+?[_\*]{3})|(\*{2}.+?\*{2}|_{2}.+?_{2})|(\*.+?\*|_.+?_)|(?<!!)(\[(?:!\[[^\]]*\]\([^\)]+\)|[^\]]+)\]\([^\)]+\))|~~(.+?)~~|(!\[(?:!\[[^\]]*\]\([^\)]+\)|[^\]]+)\]\([^\)]+\))|:([\d\w_]+?):|\[\^(\d)\]";

    public static TextElement[] ToTextElements(this string line, bool newline = false)
    {
        var matches = Regex.Matches(line, REGEX_PATTERN);
        int positionInLine = 0;

        List<TextElement> returnObjects = [];
        foreach (Match match in matches)
        {
            returnObjects.Add(new TextNormal(line.Substring(positionInLine, match.Index - positionInLine)));

            // Inline Math
            if (!string.IsNullOrEmpty(match.Groups[1].Value))
            {
                returnObjects.Add(new TextInlineMath(match.Groups[1].Value));
            }
            // Inline Code
            else if (!string.IsNullOrEmpty(match.Groups[2].Value))
            {
                returnObjects.Add(new TextInlineCode(match.Groups[2].Value));
            }
            // Bold & Italic
            else if (!string.IsNullOrEmpty(match.Groups[3].Value))
            {
                returnObjects.Add(new TextBoldItalic(match.Groups[3].Value.Trim(['*', '_'])));
            }
            // Bold
            else if (!string.IsNullOrEmpty(match.Groups[4].Value))
            {
                returnObjects.Add(new TextBold(match.Groups[4].Value.Trim(['*', '_'])));
            }
            // Italic
            else if (!string.IsNullOrEmpty(match.Groups[5].Value) )
            {
                returnObjects.Add(new TextItalic(match.Groups[5].Value.Trim(['*', '_'])));
            }
            // HyperRef
            else if (!string.IsNullOrEmpty(match.Groups[6].Value))
            {
                returnObjects.Add(new HyperRef(match.Groups[6].Value, true));
            }
            // Scrathced
            else if (!string.IsNullOrEmpty(match.Groups[7].Value))
            {
                returnObjects.Add(new TextStrikethrough(match.Groups[7].Value));
            }
            // Image
            else if (!string.IsNullOrEmpty(match.Groups[8].Value))
            {
                returnObjects.Add(new Image(match.Groups[8].Value, true));
            }
            // Emoji
            else if (!string.IsNullOrEmpty(match.Groups[9].Value))
            {
                returnObjects.Add(new TextEmoji(match.Groups[9].Value));
            }
            // FootNote
            else if (!string.IsNullOrEmpty(match.Groups[10].Value))
            {
                returnObjects.Add(new FootNoteMarker(match.Groups[10].Value));
            }

            positionInLine = match.Index + match.Length;
        }
        if (positionInLine < line.Length)
        {
            returnObjects.Add(new TextNormal(line.Substring(positionInLine)));
        }
        if (newline)
        {
            returnObjects.Add(new EmptyLine());
        }
        return returnObjects.ToArray();
    }
}
