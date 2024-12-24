using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MarkdownTypstBridge.MarkdownObjects;

namespace MarkdownTypstBridge;

public class Markdown
{
    public string Title { get; private set; }

    public object[] Body { get; private set; } = [];

    public Markdown(string title, IEnumerable<string> lines)
    {
        this.Title = title;
        this.Body = DisasambleContent(lines.ToArray());
    }
    public Markdown(string title, IEnumerable<object> body)
    {
        this.Title = title;
        this.Body = body.ToArray();
    }

    private object[] DisasambleContent(string[] lines)
    {
        return DisasambleContent(lines, 0, []).ToArray();
    }
    private List<object> DisasambleContent(string[] lines, uint position, List<object> body)
    {

        string line = lines[position];
        if (line.StartsWith("```"))
        {
            position++;

            while (true)
            {
                line += "\n" + lines[position];
                position++;

                if (lines[position - 1].StartsWith("```"))
                {
                    break;
                }
            }
        }

        body.Add(GetMarkdownObject(line));
        position++;
        if (position >= lines.Length)
        {
            return body;
        }

        return DisasambleContent(lines, position, body);
    }
    private object GetMarkdownObject(string line)
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
        if (false)
        {
            throw new NotImplementedException();
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
