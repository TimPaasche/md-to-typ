using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class UnorderedList : MarkdownObject
{
    private const string REGEX_PATTERN = @"^( *)[-+*] *(.*)";

    public int Indent { get; private set; }
    public MarkdownObject Content { get; private set; }

    public UnorderedList(string line)
    {
        var match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);
        Indent = match.Groups[1].Value.Length;
        Content = match.Groups[2].Value.Deserialze();
    }

    public override string Serialize()
    {
        return $"{new string(' ', Indent)}- {Content.Serialize()}{Environment.NewLine}";
    }
}
