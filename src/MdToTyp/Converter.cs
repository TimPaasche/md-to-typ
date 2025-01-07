using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MarkdownClasses;
using MarkdownClasses.MarkdownObjects;
using MarkdownClasses.MarkdownObjects.TextElements;

namespace MdToTyp;

public static class Converter
{
    public static string ToTypst(this Markdown md)
    {
        string typst = string.Empty;
        foreach (var item in md.Body)
        {
            typst += item.ToTypst();
        }
        return typst;
    }
    
    public static string ToTypst(this Markdown md, string pathTemplate)
    {
        if(File.Exists(pathTemplate) == false)
        {
            throw new FileNotFoundException("Template file not found", pathTemplate);
        }

        string template = File.ReadAllText(pathTemplate);
        return template + Environment.NewLine + md.ToTypst();
    }
    
    public static string ToTypst(this MarkdownObject mdObj) =>
        mdObj switch
        {
            CodeBlock codeBlock => codeBlock.ToTypst(),
            EmptyLine emptyLine=> emptyLine.ToTypst(),
            Headline headline => headline.ToTypst(),
            HorizontalLine horizontalLine => horizontalLine.ToTypst(),
            HyperRef hyperRef => hyperRef.ToTypst(),
            Image image => image.ToTypst(),
            MathBlock mathBlock => mathBlock.ToTypst(),
            OrderedList orderedList => orderedList.ToTypst(),
            UnorderedList unorderedList => unorderedList.ToTypst(),
            Quote quote => quote.ToTypst(),
            Table table => table.ToTypst(),
            Text text => text.ToTypst(),
            TextBold textBold => textBold.ToTypst(),
            TextItalic textItalic => textItalic.ToTypst(),
            TextBoldItalic textBoldItalic => textBoldItalic.ToTypst(),
            TextStrikethrough textStrikethrough => textStrikethrough.ToTypst(),
            TextEmoji textEmoji => textEmoji.ToTypst(),
            TextInlineCode textInlineCode => textInlineCode.ToTypst(),
            TextInlineMath textInlineMath => textInlineMath.ToTypst(),
            TextNormal textNormal => textNormal.ToTypst(),
            _ => throw new EvaluateException("Unknown MarkdownObject type")
        };
    
    
    private static string ToTypst(this CodeBlock codeBlock)
    {
        return $"```{codeBlock.Language}{Environment.NewLine}{codeBlock.Code}{Environment.NewLine}```{Environment.NewLine}";
    }
    
    private static string ToTypst(this EmptyLine emptyLine)
    {
        return Environment.NewLine;
    }
    
    private static string ToTypst(this Headline headline)
    {
        return $"{new string('=', headline.Level)} {headline.Content}{Environment.NewLine}";
    }
    
    private static string ToTypst(this HorizontalLine horizontalLine)
    {
        return $"#line(length: 100%){Environment.NewLine}";
    }
    
    private static string ToTypst(this HyperRef hyperRef)
    {
        return $"#link(\"{hyperRef.Link}\")[{hyperRef.Alias.ToTypst()}]{Environment.NewLine}";
    }
    
    private static string ToTypst(this Image image)
    {
        return $"#figure(image(alt: \"{image.Alias.ToTypst()}\",\"{image.Link}\"), caption: []){Environment.NewLine}";
    }
    
    private static string ToTypst(this MathBlock mathBlock)
    {
        return $"```latex{Environment.NewLine}{mathBlock.Content}{Environment.NewLine}```{Environment.NewLine}";
    }
    
    private static string ToTypst(this OrderedList orderedList)
    {
        return $"{new string(' ', orderedList.Indent)}+ {orderedList.Content.ToTypst()}{Environment.NewLine}";
    }
    
    private static string ToTypst(this UnorderedList unorderedList)
    {
        return $"{new string(' ', unorderedList.Indent)}- {unorderedList.Content.ToTypst()}{Environment.NewLine}";
    }
    
    private static string ToTypst(this Quote quote)
    {
        return Enumerable.Range(0, quote.NestedDepth).Aggregate(quote.Content.ToTypst().Trim(), (q, ii) => $"#block(fill: luma({240 - (ii * 10)}),inset: 4pt,radius: 1pt,width: 90%,[{q}])") + Environment.NewLine;
    }
    
    private static string ToTypst(this Table table)
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
            tableStr += $"  [{cell.ToTypst()}]," + Environment.NewLine;
        }
        
        tableStr += $")){Environment.NewLine}";
        return tableStr;
    }
    
    private static string ToTypst(this Text text)
    {
        return string.Concat(text.Content.Select(obj => obj.ToTypst()));
    }
    
    private static string ToTypst(this TextBold textBold)
    {
        return $"*{textBold.Content.ToTypst()}*";
    }
    
    private static string ToTypst(this TextItalic textItalic)
    {
        return $"_{textItalic.Content.ToTypst()}_";
    }
    
    private static string ToTypst(this TextBoldItalic textBoldItalic)
    {
        return $"_*{textBoldItalic.Content.ToTypst()}*_";
    }
    
    private static string ToTypst(this TextStrikethrough textStrikethrough)
    {
        return $"#strike[{textStrikethrough.Content.ToTypst()}]";
    }
    
    private static string ToTypst(this TextEmoji textEmoji)
    {
        return $"\\u{textEmoji.EmojiUnichar:X}";
    }
    
    private static string ToTypst(this TextInlineCode textInlineCode)
    {
        return $"`{textInlineCode.Content}`";
    }
    
    private static string ToTypst(this TextInlineMath textInlineMath)
    {
        return $"`${textInlineMath.Content.Trim(' ')}$`";
    }
    
    private static string ToTypst(this TextNormal textNormal)
    {
        return textNormal.Content;
    }
}