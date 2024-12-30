using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class CodeBlock : MarkdownObject
{
    private const string REGEX_PATTERN = @"```(.*)\b[\n\r]+(.+)[\n\r]+```";

    public string Language { get; private set; }
    public string Code { get; private set; }

    public CodeBlock(string line)
    {
        Match match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Singleline);
        Language = match.Groups[1].Value;
        Code = match.Groups[2].Value;
    }

    public new string Serialize()
    {
        return $"```{Language}{Environment.NewLine}{Code}```{Environment.NewLine}";
    }
}
