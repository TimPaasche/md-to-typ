using MarkdownClasses;
using MarkdownClasses.MarkdownObjects;
using MarkdownClasses.MarkdownObjects.TextElements;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace MdToTyp;

public static class Converter
{
    public static string ToTypst(this Markdown md)
    {
        string typst = string.Empty;
        foreach (var item in md.Body)
        {
            typst += item.ToTypst(md);
        }
        return typst;
    }

    public static string ToTypst(this Markdown md, string pathTemplate)
    {
        if (File.Exists(pathTemplate) == false)
        {
            throw new FileNotFoundException("Template file not found", pathTemplate);
        }

        string template = File.ReadAllText(pathTemplate);
        return template + Environment.NewLine + md.ToTypst();
    }

    public static string ToTypst(this MarkdownObject mdObj, Markdown md) =>
        mdObj switch
        {
            CodeBlock codeBlock => codeBlock.ToTypst(md),
            EmptyLine emptyLine => emptyLine.ToTypst(md),
            Headline headline => headline.ToTypst(md),
            HorizontalLine horizontalLine => horizontalLine.ToTypst(md),
            HyperRef hyperRef => hyperRef.ToTypst(md),
            Image image => image.ToTypst(md),
            MathBlock mathBlock => mathBlock.ToTypst(md),
            OrderedList orderedList => orderedList.ToTypst(md),
            UnorderedList unorderedList => unorderedList.ToTypst(md),
            Quote quote => quote.ToTypst(md),
            Table table => table.ToTypst(md),
            Text text => text.ToTypst(md),
            TextBold textBold => textBold.ToTypst(md),
            TextItalic textItalic => textItalic.ToTypst(md),
            TextBoldItalic textBoldItalic => textBoldItalic.ToTypst(md),
            TextStrikethrough textStrikethrough => textStrikethrough.ToTypst(md),
            TextEmoji textEmoji => textEmoji.ToTypst(md),
            TextInlineCode textInlineCode => textInlineCode.ToTypst(md),
            TextInlineMath textInlineMath => textInlineMath.ToTypst(md),
            TextNormal textNormal => textNormal.ToTypst(md),
            FootNote footNote => footNote.ToTypst(md),
            FootNoteMarker footNoteMarker => footNoteMarker.ToTypst(md),
            _ => throw new EvaluateException("Unknown MarkdownObject type")
        };


    private static string ToTypst(this CodeBlock codeBlock, Markdown md)
    {
        return $"```{codeBlock.Language}{Environment.NewLine}{codeBlock.Code}{Environment.NewLine}```{Environment.NewLine}";
    }

    private static string ToTypst(this EmptyLine emptyLine, Markdown md)
    {
        return Environment.NewLine;
    }

    private static string ToTypst(this Headline headline, Markdown md)
    {
        return $"{new string('=', headline.Level)} {headline.Content}{Environment.NewLine}";
    }

    private static string ToTypst(this HorizontalLine horizontalLine, Markdown md)
    {
        return $"#line(length: 100%){Environment.NewLine}";
    }

    private static string ToTypst(this HyperRef hyperRef, Markdown md)
    {
        return $"#link(\"{hyperRef.Url}\")[{hyperRef.Alias.ToTypst(md)}]{Environment.NewLine}";
    }

    private static string ToTypst(this Image image, Markdown md)
    {
        return $"#figure(image(alt: \"{image.Alias.ToTypst(md)}\",\"{image.ImageRefrence}\"), caption: []){Environment.NewLine}";
    }

    private static string ToTypst(this MathBlock mathBlock, Markdown md)
    {
        return $"$ {ConvertTexToTypst(mathBlock.Content.Trim())} $";
    }

    private static string ToTypst(this OrderedList orderedList, Markdown md)
    {
        return $"{new string(' ', orderedList.Indent)}+ {orderedList.Content.ToTypst(md)}{Environment.NewLine}";
    }

    private static string ToTypst(this UnorderedList unorderedList, Markdown md)
    {
        return $"{new string(' ', unorderedList.Indent)}- {unorderedList.Content.ToTypst(md)}{Environment.NewLine}";
    }

    private static string ToTypst(this Quote quote, Markdown md)
    {
        //TODO: Quotes have to be in blocks, but the current implementation is not working pretty
        return Enumerable.Range(0, quote.NestedDepth).Aggregate(quote.Content.ToTypst(md).Trim(), (q, ii) => $"#block(fill: luma({240 - (ii * 10)}),inset: 4pt,radius: 1pt,width: 100%,[{q}])") + Environment.NewLine;
    }

    private static string ToTypst(this Table table, Markdown md)
    {
        string tableStr = "#figure(caption: [], table(" + Environment.NewLine;
        tableStr += $"  columns: {table.Width}," + Environment.NewLine;
        tableStr += $"  align: ({string.Join(",", table.Alignments.Select(alignment => alignment switch
        {
            TableCellAlignment.Left => "left",
            TableCellAlignment.Center => "center",
            TableCellAlignment.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, "Invalid alignment value")
        }))})," + Environment.NewLine;

        foreach (var cell in table.Cells)
        {
            tableStr += $"  [{cell.ToTypst(md)}]," + Environment.NewLine;
        }

        tableStr += $")){Environment.NewLine}";
        return tableStr;
    }

    private static string ToTypst(this Text text, Markdown md)
    {
        return string.Concat(text.Content.Select(obj => obj.ToTypst(md)));
    }

    private static string ToTypst(this TextBold textBold, Markdown md)
    {
        return $"*{textBold.Content.ToTypst(md)}*";
    }

    private static string ToTypst(this TextItalic textItalic, Markdown md)
    {
        return $"_{textItalic.Content.ToTypst(md)}_";
    }

    private static string ToTypst(this TextBoldItalic textBoldItalic, Markdown md)
    {
        return $"_*{textBoldItalic.Content.ToTypst(md)}*_";
    }

    private static string ToTypst(this TextStrikethrough textStrikethrough, Markdown md)
    {
        return $"#strike[{textStrikethrough.Content.ToTypst(md)}]";
    }

    private static string ToTypst(this TextEmoji textEmoji, Markdown md)
    {
        string returnStr = string.Empty;
        foreach (var emoji in textEmoji.EmojiUnichar)
        {
            returnStr += $"\\u{{{emoji:X}}}";
        }
        return returnStr;
    }

    private static string ToTypst(this TextInlineCode textInlineCode, Markdown md)
    {
        return $"`{textInlineCode.Content}`";
    }

    private static string ToTypst(this TextInlineMath textInlineMath, Markdown md)
    {
        return $"${ConvertTexToTypst(textInlineMath.Content.Trim(' '))}$";
    }

    private static string ToTypst(this TextNormal textNormal, Markdown md)
    {
        return textNormal.Content;
    }

    private static string ToTypst(this FootNote footNote, Markdown md)
    {
        return $"";
    }
    private static string ToTypst(this FootNoteMarker footNoteMarker, Markdown md)
    {
        return $"#footnote[{md.FootNotes[footNoteMarker.Number].ToTypst(md)}]";
    }
    
    public static string ConvertTexToTypst(string tex)
    {
        return TexToTypstDotNet.TexToTypst.Convert(tex);
    }
}