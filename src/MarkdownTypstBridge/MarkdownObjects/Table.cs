using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class Table : MarkdownObject
{
    private string REGEX_PATTERN = @"(?<=\|).*?(?=\|)";
    private string REGEX_PATTERN_ALIGNMENT_IDENTIFYER = @"(?<=\|)[-:]*?(?=\|)";

    public int Height { get; private set; }
    public int Width { get; private set; }
    public int Count { get; private set; }

    /// <summary>
    /// call like: example[row, cell] = value
    /// </summary>
    public Text[] Cells { get; private set; }

    /// <summary>
    /// Enum for assigning an allignment to column 
    /// </summary>
    /// <value>Left = 0, Center = 1, Right = 2</value>
    public TableCellAlignment[] Alignments { get; private set; }

    /// <summary>
    /// a constructor, which parses a markdown table as string into an object structure
    /// </summary>
    /// <param name="line">a string that contains all lines of the table</param>
    public Table(string line)
    {
        SetTableWidthAndAlignment(line);
        SetTableCells(line);
    }

    private void SetTableCells(string line)
    {
        List<Match> matchCells = Regex.Matches(line, REGEX_PATTERN).Cast<Match>().ToList();
        matchCells.RemoveRange(Width, Width);
        this.Count = matchCells.Count;
        this.Height = this.Count / this.Width;
        this.Cells = new Text[Count];

        for (int ii = 0; ii < this.Count; ii++)
        {
            this.Cells[ii] = new Text(matchCells[ii].Value.Trim(), false);
        }
    }

    private void SetTableWidthAndAlignment(string line)
    {
        MatchCollection matchesAllignment = Regex.Matches(line, REGEX_PATTERN_ALIGNMENT_IDENTIFYER);
        this.Width = matchesAllignment.Count;
        Alignments = new TableCellAlignment[this.Width];
        for (int ii = 0; ii < this.Width; ii++)
        {
            var match = matchesAllignment[ii];
            if (match.Value.StartsWith(":") && match.Value.EndsWith(":"))
            {
                Alignments[ii] = TableCellAlignment.Center;
            }
            else if (match.Value.StartsWith(":"))
            {
                Alignments[ii] = TableCellAlignment.Left;
            }
            else if (match.Value.EndsWith(":"))
            {
                Alignments[ii] = TableCellAlignment.Right;
            }
            else
            {
                Alignments[ii] = TableCellAlignment.Center;
            }
        }
    }

    public override string Serialize()
    {
        var table = string.Empty;
        for (int ii = 0; ii < Count; ii++)
        {
            table += "|" + Cells[ii];
            if ((ii + 1) % Width == 0)
            {
                table += ii != Count - 1
                    ? "|" + Environment.NewLine
                    : "|";
            }

            // Add alignment row after the first row
            if (ii == Width - 1)
            {
                table += "|";
                for (int column = 0; column < Width; column++)
                {
                    string alignment = Alignments[column] switch
                    {
                        TableCellAlignment.Left => ":---",
                        TableCellAlignment.Center => ":---:",
                        TableCellAlignment.Right => "---:",
                        _ => "---",
                    };
                    table += alignment + "|";
                }
                table += Environment.NewLine;
            }
        }

        return table;
    }
}

public enum TableCellAlignment
{
    Left,
    Center,
    Right
}