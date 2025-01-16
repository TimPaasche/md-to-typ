using MarkdownClasses.MarkdownObjects;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarkdownClasses;

public class MarkdownObject
{
    public virtual string Serialize() => null;
}

internal static class MarkdownObjectExtension
{
    private const string REGEX_PATTERN = @"^(#+\W.*|#+.*)$|^(\*{3}|_{3}|-{3})$|^( *\+ .*$| *\*\W.*| *- .*)$|^( *\d+\. *.*)$|^(\|.*\|)$|^( *\>+.*)$|^(\$\$.*)$|^(```.*)$|^(\[.*\]\(.+\))$|^(\!\[.*\]\(.+\))$";
    
    internal static MarkdownObject Deserialze(this string line, bool newLine = false)
    {
        Match match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);
        
        // Empty Line
        if (string.IsNullOrEmpty(line.Trim()))
        {
            return new EmptyLine();
        }

        // Headline
        if (string.IsNullOrEmpty(match.Groups[1].Value) == false)
        {
            return new Headline(line);

        }

        // Horitontal Line
        if (string.IsNullOrEmpty(match.Groups[2].Value) == false)
        {
            return new HorizontalLine();
        }

        // UnorderedList
        if (string.IsNullOrEmpty(match.Groups[3].Value) == false)
        {
            return new UnorderedList(line);
        }

        // Ordered List
        if (string.IsNullOrEmpty(match.Groups[4].Value) == false)
        {
            return new OrderedList(line);
        }

        // Table
        if (string.IsNullOrEmpty(match.Groups[5].Value) == false)
        {
            return new Table(line);
        }

        // Quote
        if (string.IsNullOrEmpty(match.Groups[6].Value) == false)
        {
            return new Quote(line);
        }

        // Math Block
        if (string.IsNullOrEmpty(match.Groups[7].Value) == false)
        {
            return new MathBlock(line);
        }

        // Code Block
        if (string.IsNullOrEmpty(match.Groups[8].Value) == false)
        {
            return new CodeBlock(line);
        }

        // Link
        if (string.IsNullOrEmpty(match.Groups[9].Value) == false)
        {
            return new HyperRef(line, false);
        }

        // Image
        if (string.IsNullOrEmpty(match.Groups[10].Value) == false)
        {
            return new Image(line, false);
        }

        return new Text(line, newLine);
    }
}
