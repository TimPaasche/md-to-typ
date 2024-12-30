using System;

namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextScratched : MarkdownObject
{
    public string Content { get; private set; }

    public TextScratched(string content)
    {
        this.Content = content;
    }

    public override string ToString()
    {
        return Content;
    }

    public new string Serialize()
    {
        throw new NotImplementedException();
    }
}
