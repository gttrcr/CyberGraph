using CyberGraph;

namespace CyberGraph
{
    public class Program
    {
        private static Dictionary<string, Analyzer> _implementation = new Dictionary<string, Analyzer>(){
            {".csproj", new Analyzers.CSharpProject()},
            {".sln", new Analyzers.CSharpSolution()},
            {".cs", new Analyzers.CSharpFile()},
            {".cpp", new Analyzers.Cpp()},
        };

        private static bool PolicyToOpenFile(ref string input)
        {
            Logger.Get().WriteLine("Looking for a known extension in the " + input + " folder");
            Logger.Get().WriteLine("Implemented extensions are");
            _implementation.ToList().ForEach(x => Logger.Get().WriteLine("\t" + x.Key));
            List<FileInfo> fis = Directory.GetFiles(".", "*.*", SearchOption.AllDirectories).Where(x => _implementation.ContainsKey(new FileInfo(x).Extension)).Select(x => new FileInfo(x)).ToList();
            Logger.Get().WriteLine("Found " + fis.Count + " known files");
            if (fis.Count == 1)
            {
                input = fis[0].FullName;
                Logger.Get().WriteLine("Opening " + input);
                return true;
            }
            else
                Logger.Get().WriteLine("Nothing to do here");

            return false;
        }

        private static bool PolicyToOpenFileOrFolder(ref string input)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(input);

            if (attr.HasFlag(FileAttributes.Directory))
                return PolicyToOpenFile(ref input);
            else
                return true;
        }

        public static void Main(string[] args)
        {
            try
            {
                string input = ".";
                if (args.Length == 0)
                {
                    Logger.Get().WriteLine("No input argument");
                    if (!PolicyToOpenFile(ref input))
                        return;
                }

                input = args[0];
                if (!PolicyToOpenFileOrFolder(ref input))
                    return;

                FileInfo fi = new FileInfo(input);
                if (_implementation.ContainsKey(fi.Extension))
                {
                    Logger.Get(Path.GetFileNameWithoutExtension(fi.Name));
                    _implementation[fi.Extension].Prepare(input);
                    _implementation[fi.Extension].Start();
                    Result res = _implementation[fi.Extension].Result();
                }
                else
                    throw new NotImplementedException("Cannot process an input file with extension " + fi.Extension);

                Logger.Get().WriteLine("DONE! Press a key to close");
                Logger.Get().ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Get().WriteLine(ex.Message);
            }
        }
    }
}