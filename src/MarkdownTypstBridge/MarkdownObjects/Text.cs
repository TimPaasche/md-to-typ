using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using MarkdownTypstBridge.MarkdownObjects.TextElements;

namespace MarkdownTypstBridge.MarkdownObjects;

public class Text : MarkdownObject
{
    private const string REGEX_PATTERN = @"\$(.+?)\$|`(.+?)`|\*\*\*(.+?)\*\*\*|___(.+?)___|\*\*_(.+?)_\*\*|__\*(.+?)\*__|\*\*(.+?)\*\*|__(.+?)__|\*(.+?)\*|_(.+?)_|\[(.*?)\]\((.+?)\)|~~(.+?)~~";
    public MarkdownObject[] Content { get; private set; }

    public Text(string line)
    {
        Content = DestructLineIntoTextElements(line);
    }

    private MarkdownObject[] DestructLineIntoTextElements(string line)
    {
        var matches = Regex.Matches(line, REGEX_PATTERN);
        int positionInLine = 0;

        List<MarkdownObject> returnObjects = [];
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
            else if (!string.IsNullOrEmpty(match.Groups[3].Value) || !string.IsNullOrEmpty(match.Groups[4].Value) || !string.IsNullOrEmpty(match.Groups[5].Value) || !string.IsNullOrEmpty(match.Groups[6].Value))
            {
                string value = match.Groups[3].Value + match.Groups[4].Value + match.Groups[5].Value + match.Groups[6].Value;
                returnObjects.Add(new TextBoldItalic(value));
            }
            // Bold
            else if (!string.IsNullOrEmpty(match.Groups[7].Value) || !string.IsNullOrEmpty(match.Groups[8].Value))
            {
                returnObjects.Add(new TextBold(match.Groups[7].Value + match.Groups[8].Value));
            }
            // Italic
            else if (!string.IsNullOrEmpty(match.Groups[9].Value) || !string.IsNullOrEmpty(match.Groups[10].Value))
            {
                returnObjects.Add(new TextItalic(match.Groups[9].Value + match.Groups[10].Value));
            }
            // HyperRef
            else if (!string.IsNullOrEmpty(match.Groups[12].Value))
            {
                returnObjects.Add(new HyperRef(alias: match.Groups[11].Value, hyperRef: match.Groups[12].Value));
            }
            // Scrathced
            else if (!string.IsNullOrEmpty(match.Groups[13].Value))
            {
                returnObjects.Add(new TextScratched(match.Groups[13].Value));
            }

            positionInLine = match.Index + match.Length;
        }

        if (positionInLine < line.Length - 1)
        {
            returnObjects.Add(new TextNormal(line.Substring(positionInLine)));
        }

        return returnObjects.ToArray();
    }

    public new string Serialize()
    {
        return string.Concat(Content.Select(obj => obj.Serialize()));
    }

    public override string ToString()
    {
        return string.Concat(Content.Select(obj => obj.ToString()));
    }
}
