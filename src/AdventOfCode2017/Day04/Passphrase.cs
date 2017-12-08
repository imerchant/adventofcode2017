using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day04
{
    public class Passphrase
    {
        public string Phrase { get; }
        public List<string> Words { get; }

        public bool IsValidByWordCount => new HashSet<string>(Words).Count == Words.Count;

        public bool IsValidByAnagramDetection => CalculateValidByAnagram();


        public Passphrase(string phrase)
        {
            Phrase = phrase;
            Words = Phrase.SplitOn(' ').ToList();
        }

        private bool CalculateValidByAnagram()
        {
            var dict = new Dictionary<string, object>();
            foreach (var word in Words)
            {
                var orderedWord = word.OrderBy(c => c).AsString();
                if (dict.ContainsKey(orderedWord))
                    return false;
                dict.Add(orderedWord, null);
            }
            return true;
        }
    }
}
