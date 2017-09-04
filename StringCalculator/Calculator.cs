namespace StringCalculator
{
    using System;
    using System.Linq;

    public class Calculator
    {
        public int Add(string numbers)
        {
            return numbers
                .Split(new[] {',', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Sum();
        }
    }
}