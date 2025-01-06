using System;
using System.Linq;
using MarkdownClasses.MarkdownObjects;

namespace MarkdownClasses;

public class MarkdownObject
{
    public virtual string Serialize() => null;
}

internal static class MarkdownObjectExtension
{
    internal static MarkdownObject Deserialze(this string line, bool newLine = false)
    {
        // Empty Line
        if (string.IsNullOrEmpty(line.Trim()))
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
        if (line.TrimStart().StartsWith(">"))
        {
            return new Quote(line);
        }

        // Math Block
        if (line.TrimStart().StartsWith("$$"))
        {
            return new MathBlock(line);
        }

        // Code Block
        if (line.TrimStart().StartsWith("```"))
        {
            return new CodeBlock(line);
        }

        // Link
        if (line.TrimStart().StartsWith("["))
        {
            return new HyperRef(line, false);
        }
        
        // Image
        if (line.TrimStart().StartsWith("!["))
        {
            return new Image(line, false);
        }

        return new Text(line, newLine);
    }
}
