using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MarkdownTypstBridge.MarkdownObjects;

namespace MarkdownTypstBridge;

public class Markdown
{
    public string Title { get; private set; } = "New Markdown Document";

    public MarkdownObject[] Body { get; private set; } = [];

    public Markdown() { }
    public Markdown(string title, IEnumerable<string> lines)
    {
        Deserialize(lines, title);
    }
    public Markdown(string title, IEnumerable<MarkdownObject> body)
    {
        this.Title = title;
        this.Body = body.ToArray();
    }

    public void Deserialize(IEnumerable<string> lines, string title)
    {
        this.Title = title;
        this.Body = DisasambleContent(lines.ToArray());
    }

    private MarkdownObject[] DisasambleContent(string[] lines)
    {
        return DisasambleContent(lines, 0, []).ToArray();
    }
    private List<MarkdownObject> DisasambleContent(string[] lines, uint position, List<MarkdownObject> body)
    {

        string line = lines[position];

        GetAllLinesForCodeBlock(lines, ref position, ref line);
        GetAllLinesForTable(lines, ref position, ref line);
        GetAllLinesForFormula(lines, ref position, ref line);
        body.Add(line.Deserialze());
        position++;
        if (position >= lines.Length)
        {
            return body;
        }

        return DisasambleContent(lines, position, body);
    }

    private static void GetAllLinesForFormula(string[] lines, ref uint position, ref string line)
    {
        if (line.StartsWith("$$"))
        {
            position++;

            while (true)
            {
                line += "\n" + lines[position];
                position++;

                if (lines[position - 1].StartsWith("$$"))
                {
                    break;
                }
            }
        }
    }

    private static void GetAllLinesForTable(string[] lines, ref uint position, ref string line)
    {
        if (line.StartsWith("|"))
        {
            position++;

            while (true)
            {
                line += "\n" + lines[position];
                position++;

                if (!lines[position].StartsWith("|"))
                {
                    break;
                }
            }
        }
    }

    private static void GetAllLinesForCodeBlock(string[] lines, ref uint position, ref string line)
    {
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
    }

    // private object GetMarkdownObject(string line)
    // {
    //     // Empty Line
    //     if (string.IsNullOrEmpty(line))
    //     {
    //         return new EmptyLine();
    //     }

    //     // Headline
    //     if (line.TrimStart().StartsWith("#"))
    //     {
    //         return new Headline(line);
    //     }

    //     // UnorderedList
    //     if (line.TrimStart().StartsWith("-")
    //         || line.TrimStart().StartsWith("*")
    //         || line.TrimStart().StartsWith("+"))
    //     {
    //         return new UnorderedList(line);
    //     }

    //     // Ordered List
    //     if (char.IsDigit(line.TrimStart().First()))
    //     {
    //         return new OrderedList(line);
    //     }

    //     // Horitontal Line
    //     if (line.TrimStart().StartsWith("---") || line.TrimStart().StartsWith("___"))
    //     {
    //         return new HorizontalLine();
    //     }

    //     // Table
    //     if (line.TrimStart().StartsWith("|"))
    //     {
    //         return new Table(line);
    //     }

    //     // Quote
    //     if (false)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     // Math Block
    //     if (false)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     // Code Block
    //     if (line.TrimStart().StartsWith("```"))
    //     {
    //         return new CodeBlock(line);
    //     }

    //     return new Text(line);
    // }
}
