using CalcCore;

if (args.Length == 0)
{
	Console.WriteLine("Usage: calccli <expression>");
	return;
}

var expression = string.Join(" ", args);
try
{
	var result = ExpressionEvaluator.Evaluate(expression);
	Console.WriteLine(result.ToString(System.Globalization.CultureInfo.InvariantCulture));
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error: {ex.Message}");
	Environment.ExitCode = 1;
}
