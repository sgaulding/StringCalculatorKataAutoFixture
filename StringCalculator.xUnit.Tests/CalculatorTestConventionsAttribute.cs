namespace StringCalculator.xUnit.Tests
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Xunit2;

    public class CalculatorTestConventionsAttribute : AutoDataAttribute
    {
        public CalculatorTestConventionsAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}