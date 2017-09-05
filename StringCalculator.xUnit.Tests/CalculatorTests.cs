namespace StringCalculator.xUnit.Tests
{
    using System;
    using System.Linq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class CalculatorTests
    {
        [Theory, CalculatorTestConventions]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(Calculator sut, int count, Generator<int> generator)
        {
            var integers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", integers);
            var actual = sut.Add(numbers);
            Assert.Equal(integers.Sum(), actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddEmptyRetursCorrectResults(Calculator sut)
        {
            var numbers = string.Empty;
            var actual = sut.Add(numbers);
            Assert.Equal(0, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddIgnoresBigNumbers(Calculator sut, int smallSeed, int bigSeed)
        {
            var smallNumber = Math.Min(smallSeed, 1000);
            var largeNumber = bigSeed + 1000;
            var numbers = string.Join(",", smallSeed, largeNumber);

            var actual = sut.Add(numbers);

            Assert.Equal(smallNumber, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delmiter,
            int count,
            Generator<int> intGenerator)
        {
            var integers = intGenerator.Take(count + 2).ToArray();
            var numbers = $"//[{delmiter}]\n{string.Join(delmiter, integers)}";
            var expected = integers.Sum();

            var actual = sut.Add(numbers);

            Assert.Equal(expected, actual);
        }

        [Theory, AutoData]
        public void AddLineWithCustomDelimterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            var delimiter = charGenerator.Where(c => c != '-').First(c => !int.TryParse(c.ToString(), out var dummy));
            var integers = intGenerator.Take(count + 2).ToArray();
            var numbersWithDelimiter = string.Join(delimiter.ToString(), integers);
            var numbers = $"//{delimiter}\n{numbersWithDelimiter}";
            var expected = integers.Sum();

            var actual = sut.Add(numbers);

            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddLineWithMultipleCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delmiter1,
            string delmiter2,
            int count,
            Generator<int> intGenerator)
        {
            var first = intGenerator.First();
            var second = intGenerator.Skip(1).First();
            var third = intGenerator.Skip(2).First();

            var numbers = $"//[{delmiter1}][{delmiter2}]\n{first}{delmiter2}{second}{delmiter1}{third}";
            var expected = first + second + third;

            var actual = sut.Add(numbers);

            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddLineWithNegativeNumbersThowsCorrectException(
            Calculator sut,
            int firstInt,
            int secondInt,
            int thridInt)
        {
            var numbers = string.Join(",", -firstInt, secondInt, -thridInt);
            var exeption = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Add(numbers));
            Assert.True(exeption.Message.StartsWith("Negatives not allowed."));
            Assert.Contains((-firstInt).ToString(), exeption.Message);
            Assert.Contains((-thridInt).ToString(), exeption.Message);
        }

        [Theory, CalculatorTestConventions]
        public void AddSingleNumberReturnCorrectResult(Calculator sut, int expected)
        {
            var numbers = expected.ToString();
            var actual = sut.Add(numbers);
            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddTwoNumberReturnedCorrectResult(Calculator sut, int firstInt, int secondInt)
        {
            var numbers = string.Join(",", new[] { firstInt, secondInt });
            var actual = sut.Add(numbers);
            Assert.Equal(firstInt + secondInt, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddWithLineBreakCommaAsDelimitersReturnsCorrectResult(
            Calculator sut,
            int firstInt,
            int secondInt,
            int thridInt)
        {
            var numbers = $"{firstInt}\n{secondInt},{thridInt}";
            var actual = sut.Add(numbers);
            Assert.Equal(firstInt + secondInt + thridInt, actual);
        }
    }
}