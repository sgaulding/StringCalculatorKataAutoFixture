namespace StringCalculator
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Calculator
    {
        public int Add(string numbers)
        {
            const string DetectDelimiterString = "//";
            var delimterPatter = new Regex($"{DetectDelimiterString}\\[(.+?)\\]", RegexOptions.Multiline);

            var defaultDelimiters = new[] { ",", "\n" };
            var numbersOnly = numbers;
            var delimters = defaultDelimiters;

            if (numbers.StartsWith(DetectDelimiterString))
            {
                var match = delimterPatter.Match(numbers);
                var value = match.Success ? match.Groups[1].Value : numbers.Skip(2).First().ToString();
                delimters = new[] { value };
                numbersOnly = new string(numbers.SkipWhile(c => c != '\n').ToArray());
            }

            var numbersToSum = numbersOnly.Split(delimters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToArray();

            var negativeNumbers = numbersToSum.Where(num => num < 0).ToArray();
            if (negativeNumbers.Any())
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numbers),
                    $"Negatives not allowed. Negatives in numbers:\n{string.Join("\n", negativeNumbers)}");
            }

            return numbersToSum.Where(i => i < 1000).Sum();
        }
    }
}