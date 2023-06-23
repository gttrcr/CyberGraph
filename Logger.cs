namespace CyberGraph
{
    public class Logger
    {
        private static Logger? _logInstance = null;
        public static Logger Get(string outputFolder)
        {
            if (_logInstance == null)
                _logInstance = new Logger(outputFolder);
            return _logInstance;
        }

        public static Logger Get()
        {
            if (_logInstance == null)
                throw new NullReferenceException("Instance of logger must be initialized with a valid outputFolder");
            return _logInstance;
        }

        public string OutputFolder { get; private set; }
        private Logger(string outputFolder)
        {
            OutputFolder = outputFolder;
            Directory.CreateDirectory(OutputFolder);
        }

        public void WriteLine(string? line)
        {
            Console.WriteLine(line);
        }

        public void AppendFileLine(string file, string line)
        {
            File.AppendAllLines(Path.Combine(OutputFolder, file), new List<string> { line });
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void ClearFile(string file)
        {
            File.WriteAllText(Path.Combine(OutputFolder, file), String.Empty);
        }
    }
}