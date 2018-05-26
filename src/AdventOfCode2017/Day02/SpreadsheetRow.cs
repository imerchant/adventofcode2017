using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day02
{
    public class SpreadsheetRow
    {
        public IList<int> Cells { get; }

        public int Max => Cells.Max();

        public int Min => Cells.Min();

        public int Difference => Max - Min;

        public int DivisibleResult => CalculateDivisibleResult();

        public SpreadsheetRow(IList<int> cells)
        {
            Cells = cells;
        }

        private int CalculateDivisibleResult()
        {
            for (var k = 0; k < Cells.Count; ++k)
            {
                for (var j = 0; j < Cells.Count; ++j)
                {
                    if (k == j || Cells[j] < Cells[k])
                        continue;

                    if (Cells[j] % Cells[k] == 0)
                        return Cells[j] / Cells[k];
                }
            }
            throw new Exception("Could not locate divisible pair.");
        }
    }
}
