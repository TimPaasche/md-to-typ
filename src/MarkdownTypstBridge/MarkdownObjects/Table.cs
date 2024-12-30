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
    public Text[,] Cells { get; private set; }

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
        this.Cells = new Text[Width, Height];

        int ii = 0;
        for (int hCounter = 0; hCounter < this.Height; hCounter++)
        {
            for (int wCounter = 0; wCounter < this.Width; wCounter++)
            {
                this.Cells[wCounter, hCounter] = new Text(matchCells[ii].Value.Trim());
                ii++;
            }
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

    public new string Serialize()
    {
        var table = new StringBuilder();

        // Build the table row by row
        for (int row = 0; row < Height; row++)
        {
            // Add the row cells
            table.Append("|");
            for (int column = 0; column < Width; column++)
            {
                table.Append(Cells[row, column] + "|");
            }
            table.AppendLine();

            // Add alignment row after the first row
            if (row == 0)
            {
                table.Append("|");
                for (int column = 0; column < Width; column++)
                {
                    string alignment = Alignments[column] switch
                    {
                        TableCellAlignment.Left => ":---",
                        TableCellAlignment.Center => ":---:",
                        TableCellAlignment.Right => "---:",
                        _ => "---",
                    };
                    table.Append(alignment + "|");
                }
                table.AppendLine();
            }
        }

        return table.ToString();
    }
}

public enum TableCellAlignment
{
    Left,
    Center,
    Right
}