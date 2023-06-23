using CyberGraph;

namespace Analyzers
{
    public class Cpp : CyberGraph.Analyzer
    {
        public override void Prepare(string input)
        {
            Logger.Get().WriteLine("Input file " + input + " is a cpp file");
            FileInput = input;
        }
    }
}