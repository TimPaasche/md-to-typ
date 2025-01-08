using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextEmoji : TextElement
{
    private readonly string MD_EMOJI_CSV_PATH;

    public string EmojiMarkdownString { get; private set; }
    public int EmojiUnichar { get; private set; }



    public TextEmoji(string line)
    {
        MD_EMOJI_CSV_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "md-emojis.csv");
        Console.WriteLine(MD_EMOJI_CSV_PATH);
        EmojiMarkdownString = line;
        EmojiUnichar = GetEmojiUnichar(EmojiMarkdownString);
    }

    private int GetEmojiUnichar(string emojiMarkdownString)
    {


        var lines = File.ReadAllLines(MD_EMOJI_CSV_PATH);
        var emojiUnichar = lines.First(line => line.Contains(emojiMarkdownString)).Split(',')[1].Trim(['U', '+']);
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
}