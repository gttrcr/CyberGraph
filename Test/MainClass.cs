using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CyberGraph;

namespace Test
{
    public class MainClass
    {
        public static void _Main(string[] args)
        {
            Logger.Get().WriteLine("I'm the main test");
            MainObject o = new MainObject();
            o.SetField(17);
            Logger.Get().WriteLine(o.ToString());
        }
    }

    public class CSharpFile : CyberGraph.Analyzer
    {
        public override void Prepare(string input)
        {
            FileInput = input;
            Logger.Get().WriteLine("Input file " + FileInput + " is a c# file");
            Source = File.ReadAllText(FileInput);
        }

        public override void Start()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(Source);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            List<SyntaxNode> dn = root.DescendantNodes().ToList();
            List<SyntaxNodeOrToken> dnts = root.DescendantNodesAndTokensAndSelf().ToList();

            string filename = Path.GetFileNameWithoutExtension(FileInput);
            Logger.Get().ClearFile(filename + "_stat.csv");
            Dictionary<int, List<SyntaxNode>> code_tree = new Dictionary<int, List<SyntaxNode>>();
            for (int i = 0; i < dnts.Count; i++)
            {
                code_tree[i] = dn.Where(x => x.DescendantNodesAndTokensAndSelf().Count() == i).ToList();
                Logger.Get().AppendFileLine(filename + "_stat.csv", i.ToString() + ", " + code_tree[i].Count.ToString());
            }
        }
    }
}