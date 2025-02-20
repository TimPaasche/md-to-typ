using System;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class FootNoteMarker : TextElement
{
    public int Number { get; private set; }

    public FootNoteMarker(string line) : base()
    {
        Number = int.Parse(line);
    }

    public override string Serialize()
    {
        return $"[^{Number}]";
    }
}