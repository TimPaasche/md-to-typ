using MdToTyp;

namespace CLI;

using MarkdownClasses;

internal class Program
{
    static void Main(string[] args)
    {
        Markdown md = MarkdownExtensions.Deserialize("./../../../../Test-Data/md/test.md");
        string typst = md.ToTypst();
        string typstWithTemplate = md.ToTypst("./template.typ");
        File.WriteAllText("./../../../../Test-Data/typst/test.typ", typst);
        File.WriteAllText("./../../../../Test-Data/typst/test_with_template.typ", typstWithTemplate);
    }
}
