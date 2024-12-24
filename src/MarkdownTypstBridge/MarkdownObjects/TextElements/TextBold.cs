using System.Runtime.CompilerServices;

namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextBold
{
    public string Content { get; private set; }

    public TextBold(string content)
    {
        this.Content = content;
    }
}
