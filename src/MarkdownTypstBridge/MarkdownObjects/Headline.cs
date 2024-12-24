using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class Headline
{
    private const string REGEX_PATTERN = @"(#+)\W(.*)|(#+)(.*)";

    public string Content { get; private set; }
    public uint Level { get; private set; }
    public string Label { get; private set; }

    public Headline(string line)
    {
        var match = Regex.Match(line, REGEX_PATTERN);
        Level = (uint)match.Groups[1].Value.Length;
        Content = match.Groups[2].Value;
        Label = match.Groups[2].Value.Replace(" ", "-").ToLower();
    }
}