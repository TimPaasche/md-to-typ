using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class OrderedList : MarkdownObject
{
    private const string REGEX_PATTERN = @"( *)(\d+). *(.*)";

    public int Indent { get; private set; }
    public int Number { get; private set; }
    public string Content { get; private set; }

    public OrderedList(string line)
    {
        var match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);
        Indent = match.Groups[1].Value.Length;
        Number = int.Parse(match.Groups[2].Value);
        Content = match.Groups[3].Value;
    }

    public new string Serialize()
    {
        return $"{new string(' ', Indent * 2)}{Number}.  {Content}{Environment.NewLine}";
    }
}