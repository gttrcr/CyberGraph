using CyberGraph;

namespace Analyzers
{
    public class CSharpSolution : CyberGraph.Analyzer
    {
        public override void Prepare(string input)
        {
            FileInput = input;
            Logger.Get().WriteLine("Input file " + FileInput + " is a c# solution");
        }
    }
}