using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using CyberGraph;

namespace Analyzers
{
    public class CSharpProject : CyberGraph.Analyzer
    {
        private Project? _project { get; set; }
        private Result _result { get; set; }

        public override void Prepare(string input)
        {
            FileInput = input;
            Logger.Get().WriteLine("Input file " + FileInput + " is a c# project");

            Logger.Get().WriteLine(FileInput + " loading...");
            MSBuildLocator.RegisterDefaults();
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            _project = workspace.OpenProjectAsync(FileInput).Result;
            Logger.Get().WriteLine(FileInput + " load completed");
            Logger.Get().WriteLine("Assembly name: " + _project.AssemblyName);
            Logger.Get().WriteLine("Language: " + _project.Language);
            Logger.Get().WriteLine("Found " + _project.Documents.Count() + " source files");
            _project.Documents.ToList().ForEach(x => Logger.Get().WriteLine(x.FilePath));
        }

        public override void Start()
        {
            _project?.Documents.ToList().ForEach(x =>
            {
                CSharpFile file = new CSharpFile();
                file.Prepare(x.FilePath);
                file.Start();
            });
        }

        public override Result Result()
        {
            return new Result();
        }
    }
}