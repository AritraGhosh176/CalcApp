using CalcCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve index.html and static assets
app.UseDefaultFiles();
app.UseStaticFiles();

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
