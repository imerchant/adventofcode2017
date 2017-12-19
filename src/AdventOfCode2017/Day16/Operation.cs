using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day16
{
    public class Operation
    {
        public Action<Dancers> Op { get; }

        private Operation(Action<Dancers> op)
        {
            Op = op;
        }

        private static readonly Regex SpinRegex = new Regex(@"s(?'index'\d+)", RegexOptions.Compiled);
        private static readonly Regex ExchangeRegex = new Regex(@"x(?'position1'\d+)\/(?'position2'\d+)", RegexOptions.Compiled);
        private static readonly Regex PartnerRegex = new Regex(@"p(?'one'\w{1})\/(?'two'\w{1})", RegexOptions.Compiled);

        public static Operation Parse(string input)
        {
            var spinMatch = SpinRegex.Match(input);
            if (spinMatch.Success)
            {
                var index = int.Parse(spinMatch.Groups["index"].Value);
                return new Operation(d => d.Spin(index));
            }

            var exchangeMatch = ExchangeRegex.Match(input);
            if (exchangeMatch.Success)
            {
                var position1 = int.Parse(exchangeMatch.Groups["position1"].Value);
                var position2 = int.Parse(exchangeMatch.Groups["position2"].Value);
                return new Operation(d => d.Exchange(position1, position2));
            }

            var partnerMatch = PartnerRegex.Match(input);
            if (partnerMatch.Success)
            {
                var one = partnerMatch.Groups["one"].Value[0];
                var two = partnerMatch.Groups["two"].Value[0];
                return new Operation(d => d.Partner(one, two));
            }

            throw new Exception($"could not parse {input}");
        }
    }
}