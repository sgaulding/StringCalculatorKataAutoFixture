﻿namespace StringCalculator.xUnit.Tests
{
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

        [Theory, AutoData]
        public void AddLineWithCustomDelimterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            var delimiter = charGenerator.First(c => !int.TryParse(c.ToString(), out var dummy));
            var integers = intGenerator.Take(count + 2).ToArray();
            var numbersWithDelimiter = string.Join(delimiter.ToString(), integers);
            var numbers = $"//{delimiter}\n{numbersWithDelimiter}";
            var expected = integers.Sum();

            var actual = sut.Add(numbers);

            Assert.Equal(expected, actual);
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