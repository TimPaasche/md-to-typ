using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using MarkdownClasses.MarkdownObjects;

namespace MarkdownClasses;

public class Markdown
{
    public string Title { get; private set; } = "New Markdown Document";

    public MarkdownObject[] Body { get; private set; } = [];
    
    public Dictionary<int, MarkdownObject> FootNotes { get; private set; } = new();
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

    public string Serialize()
    {
        StringBuilder md = new();
        foreach (MarkdownObject item in Body)
        {
            md.Append(item.Serialize());
        }
        return md.ToString();
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
        GetAllLinesForMathBlock(lines, ref position, ref line);
        var mdObject = line.Deserialze(newLine: true);
        if (mdObject is FootNote fn)
        {
            FootNotes.Add(fn.Number, fn.Content);
        }
        body.Add(mdObject);
        position++;
        if (position >= lines.Length)
        {
            return body;
        }

        return DisasambleContent(lines, position, body);
    }

    private static void GetAllLinesForMathBlock(string[] lines, ref uint position, ref string line)
    {
        if (line.StartsWith("$$"))
        {
            position++;

            while (true)
            {
                line += "\n" + lines[position];

                if (position >= lines.Length - 1 || lines[position].StartsWith("$$"))
                {
                    break;
                }
                position++;
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

                if (position >= lines.Length - 1 || !lines[position + 1].StartsWith("|"))
                {
                    break;
                }

                position++;
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

                if (position >= lines.Length - 1 || lines[position].StartsWith("```"))
                {
                    break;
                }
                position++;
            }
        }
    }
}
