namespace AdventOfCode2017.Day09
{
    public class GroupCounter
    {
        private readonly string _input;

        public GroupCounter(string input)
        {
            _input = input;
        }

        public (int groups, int scoreSum) CountGroupsAndScore()
        {
            var groups = 0;
            var inGroup = 0;
            var inGarbage = false;
            var scoreSum = 0;

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
                    case '<':
                        inGarbage = true;
                        break;
                    case '>':
                        inGarbage = false;
                        break;
                    default:
                        break;
                }
            }

            return (groups, scoreSum);
        }
    }
}
