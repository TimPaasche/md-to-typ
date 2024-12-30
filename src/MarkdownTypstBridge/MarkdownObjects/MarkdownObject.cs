using System;
using System.Linq;

namespace MarkdownTypstBridge.MarkdownObjects;

public class MarkdownObject
{
    public string Serialize() => null;
}

internal static class MarkdownObjectExtension
{
    internal static MarkdownObject Deserialze(this string line)
    {
        // Empty Line
        if (string.IsNullOrEmpty(line))
        {
            return new EmptyLine();
        }

        // Headline
        if (line.TrimStart().StartsWith("#"))
        {
            return new Headline(line);

        }

        // UnorderedList
        if (line.TrimStart().StartsWith("-")
            || line.TrimStart().StartsWith("*")
            || line.TrimStart().StartsWith("+"))
        {
            return new UnorderedList(line);
        }

        // Ordered List
        if (char.IsDigit(line.TrimStart().First()))
        {
            return new OrderedList(line);
        }

        // Horitontal Line
        if (line.TrimStart().StartsWith("---") || line.TrimStart().StartsWith("___"))
        {
            return new HorizontalLine();
        }

        // Table
        if (line.TrimStart().StartsWith("|"))
        {
            return new Table(line);
        }

        // Quote
        if (false)
        {
            throw new NotImplementedException();
        }

        // Math Block
        if (false)
        {
            throw new NotImplementedException();
        }

        // Code Block
        if (line.TrimStart().StartsWith("```"))
        {
            return new CodeBlock(line);
        }

        return new Text(line);
    }
}
