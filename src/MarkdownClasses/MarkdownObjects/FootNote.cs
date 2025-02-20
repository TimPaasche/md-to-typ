using System;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class FootNote : MarkdownObject
{
    private const string REGEX_PATTERN = @"^\[\^(\d)\]:\s*(.*)$";

    public MarkdownObject Content { get; private set; }
    public int Number { get; private set; }

    public FootNote(string line) : base()
    {
        var match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Singleline);
        Number = int.Parse(match.Groups[1].Value);
        Content = match.Groups[2].Value.Deserialze();
    }

    public override string Serialize()
    {
        return $"[^{Number}]: {Content.Serialize()}{Environment.NewLine}";
    }
}