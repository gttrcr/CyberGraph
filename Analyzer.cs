namespace CyberGraph
{
    public abstract class Analyzer
    {
        public string Source { get; protected set; }
        public string FileInput { get; protected set; }

        public Analyzer()
        {
            Source = "";
            FileInput = "";
        }

        public virtual void Prepare(string input)
        {
            throw new NotImplementedException("Prepare not implemented");
        }

        public virtual void Start()
        {
            throw new NotImplementedException("Start not implemented");
        }

        public virtual Result Result()
        {
            throw new NotImplementedException("Result not implemented");
        }
    }
}