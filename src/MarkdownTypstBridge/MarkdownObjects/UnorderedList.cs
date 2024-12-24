using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class UnorderedList
{
    private const string REGEX_PATTERN = @"^( *)[-+*] *(.*)";

    public uint Indent { get; private set; }
    public string Content { get; private set; }

    public UnorderedList(string line)
    {
        var match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);
        Indent = (uint)match.Groups[1].Value.Length;
        Content = match.Groups[2].Value;
    }
}
