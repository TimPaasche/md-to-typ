using System;

namespace MarkdownTypstBridge.MarkdownObjects;

public class HyperRef : MarkdownObject
{
    public string Alias { get; private set; }
    public string Link { get; private set; }

    public HyperRef(string alias, string hyperRef)
    {
        this.Alias = alias;
        this.Link = hyperRef;
    }

    public override string ToString()
    {
        return Link;
    }

    public new string Serialize()
    {
        return $"[{Alias}]({Link}){Environment.NewLine}";
    }
}
