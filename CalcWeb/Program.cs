using CalcCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "CalcWeb running. Use /calc?expr=1+2*3");
app.MapGet("/calc", (string expr) =>
{
	try
	{
		var result = ExpressionEvaluator.Evaluate(expr);
		return Results.Ok(new { expression = expr, result });
	}
	catch (DivideByZeroException)
	{
		return Results.BadRequest(new { error = "Division by zero" });
	}
	catch (Exception ex)
	{
		return Results.BadRequest(new { error = ex.Message });
	}
});

app.Run();
