using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class OrderedList
{
    private const string REGEX_PATTERN = @"( *)(\d+). *(.*)";

    public uint Indent { get; private set; }
    public string Content { get; private set; }

    public OrderedList(string line)
    {
        var match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);
        Indent = (uint)match.Groups[1].Value.Length;
        Content = match.Groups[3].Value;
    }
}
