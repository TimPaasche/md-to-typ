using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextEmoji : TextElement
{
    private readonly string MD_EMOJI_CSV_PATH;

    public string EmojiMarkdownString { get; private set; }
    public int[] EmojiUnichar { get; private set; }



    public TextEmoji(string line)
    {
        MD_EMOJI_CSV_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "md-emojisV2.csv");
        EmojiMarkdownString = line;
        EmojiUnichar = GetEmojiUnichar(EmojiMarkdownString);
    }

    private int[] GetEmojiUnichar(string emojiMarkdownString)
    {
        var test = Convert.ToInt32("1f634", 16);

        var lines = File.ReadAllLines(MD_EMOJI_CSV_PATH);
        string[] emojiUnichar = lines.Any(line => line.Contains(emojiMarkdownString))
            ? lines.First(line => line.Contains(emojiMarkdownString)).Split(',')[1].Split('-')
            : ["FFFD"];
        int[] emojiUnicharAsIntArray = emojiUnichar.Select(emoji => Convert.ToInt32(emoji.Trim(), 16)).ToArray();
        return emojiUnicharAsIntArray;
    }

    public override string ToString()
    {
        string emoji = "";
        foreach (int unichar in EmojiUnichar)
        {
            emoji += unichar.ToString("X");
        }
        return emoji;
    }

    public override string Serialize()
    {
        return $":{EmojiMarkdownString}:";
    }
}