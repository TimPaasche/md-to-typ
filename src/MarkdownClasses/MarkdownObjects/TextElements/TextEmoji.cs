using System;
using System.IO;
using System.Linq;
using System.Net;

namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextEmoji : MarkdownObject
{
    private const string MD_EMOJI_CSV_PATH = @"./md-emojis.csv";
    
    public string EmojiMarkdownString { get; private set; }
    public int EmojiUnichar { get; private set; }
    
    public TextEmoji(string line)
    {
        EmojiMarkdownString = line;
        EmojiUnichar = GetEmojiUnichar(EmojiMarkdownString);
    }

    private int GetEmojiUnichar(string emojiMarkdownString)
    {
        var lines = File.ReadAllLines(MD_EMOJI_CSV_PATH);
        var emojiUnichar = lines.First(line => line.Contains(emojiMarkdownString)).Split(',')[1].Trim(['U','+']);
        return Convert.ToInt32(emojiUnichar, 16);
    }

    public override string ToString()
    {
        return EmojiUnichar.ToString("X");
    }

    public override string Serialize()
    {
        return $":{EmojiMarkdownString}:";
    }
    
    public override string ToTypst()
    {
        return $"\\u{EmojiUnichar:X}";
    }
}