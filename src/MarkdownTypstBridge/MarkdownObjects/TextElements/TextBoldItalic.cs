using System;

namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextBoldItalic
{
    public string Content { get; private set; }
    public TextBoldItalic(string content)
    {
        this.Content = content;
    }
}
