namespace Test
{
    public class MainObject
    {
        private string Str;
        public int Field { get; private set; }

        public MainObject()
        {
            Field = 14;
            Str = "!$";
        }

        public void SetField(int field)
        {
            Field = field;
        }

        public override string ToString()
        {
            return "Field value is " + Field + " and Str value is " + Str;
        }
    }
}