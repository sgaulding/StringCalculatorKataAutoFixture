namespace StringCalculator
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Calculator
    {
        public int Add(string numbers)
        {
            string[] ExtractDelimiters(MatchCollection matchCollection)
            {
                return (from Match match in matchCollection select match.Groups[1].Value).ToArray();
            }

            const string DetectDelimiterString = "//";
            var delimterPatter = new Regex(@"\[(.+?)\]", RegexOptions.Multiline);

            var defaultDelimiters = new[] { ",", "\n" };
            var numbersOnly = numbers;
            var delimters = defaultDelimiters;

            if (numbers.StartsWith(DetectDelimiterString))
            {
                var matchCollection = delimterPatter.Matches(numbers);
                delimters = matchCollection.Count == 0
                                ? new[] { numbers.Skip(2).First().ToString() }
                                : ExtractDelimiters(matchCollection);
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