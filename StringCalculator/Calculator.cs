namespace StringCalculator
{
    using System;
    using System.Linq;

    public class Calculator
    {
        public int Add(string numbers)
        {
            const string DetectDelimiterString = "//";

            var defaultDelimiters = new[] { ',', '\n' };
            var numbersOnly = numbers;
            var delimters = defaultDelimiters;

            if (numbers.StartsWith(DetectDelimiterString))
            {
                delimters = new[] { numbers.Skip(2).First() };
                numbersOnly = new string(numbers.SkipWhile(c => c != '\n').ToArray());
            }

            return numbersOnly.Split(delimters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum();
        }
    }
}