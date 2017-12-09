namespace AdventOfCode2017.Day09
{
    public class GroupCounter
    {
        private readonly string _input;

        public GroupCounter(string input)
        {
            _input = input;
        }

        public (int groups, int scoreSum, int garbageChars) CountGroupsAndScore()
        {
            var groups = 0;
            var inGroup = 0;
            var inGarbage = false;
            var scoreSum = 0;
            var charsInGarbage = 0;
            for (var k = 0; k < _input.Length; ++k)
            {
                if (k >= _input.Length)
                    break;

                var c = _input[k];

                switch (c)
                {
                    case '{' when !inGarbage:
                        groups++;
                        inGroup++;
                        break;
                    case '}' when !inGarbage:
                        scoreSum += inGroup;
                        inGroup--;
                        break;
                    case '!':
                        ++k;
                        break;
                    case '<' when !inGarbage:
                        inGarbage = true;
                        break;
                    case '>' when inGarbage:
                        inGarbage = false;
                        break;
                    default:
                        if (inGarbage)
                            charsInGarbage++;
                        break;
                }
            }

            return (groups, scoreSum, charsInGarbage);
        }
    }
}
