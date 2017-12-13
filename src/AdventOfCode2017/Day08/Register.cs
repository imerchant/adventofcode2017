namespace AdventOfCode2017.Day08
{
    public class Register
    {
        public string Id { get; }
        public int Value { get; set; }

        public Register(string id)
        {
            Id = id;
            Value = 0;
        }
    }
}
