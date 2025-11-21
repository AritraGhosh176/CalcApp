using CalcCore;
using Xunit;

namespace CalcTests;

public class ExpressionEvaluatorTests
{
	[Theory]
	[InlineData("1+2", 3)]
	[InlineData("2*3", 6)]
	[InlineData("10/2", 5)]
	[InlineData("2+3*4", 14)]
	[InlineData("(2+3)*4", 20)]
	[InlineData("3+4*2/(1-5)", 1)] // 3 + (4*2)/(-4) = 3 -2 =1
	public void EvaluatesBasicExpressions(string expr, double expected)
	{
		var result = ExpressionEvaluator.Evaluate(expr);
		Assert.Equal(expected, result, precision: 5);
	}

	[Fact]
	public void DivisionByZeroThrows()
	{
		Assert.Throws<DivideByZeroException>(() => ExpressionEvaluator.Evaluate("1/0"));
	}

	[Fact]
	public void InvalidCharacterThrows()
	{
		Assert.Throws<FormatException>(() => ExpressionEvaluator.Evaluate("2 & 3"));
	}

	[Fact]
	public void MismatchedParenthesesThrows()
	{
		Assert.Throws<FormatException>(() => ExpressionEvaluator.Evaluate("(1+2"));
	}
}
