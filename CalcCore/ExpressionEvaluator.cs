using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalcCore;

public static class ExpressionEvaluator
{
	public static double Evaluate(string expression)
	{
		if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentException("Expression is empty", nameof(expression));
		var tokens = Tokenize(expression);
		var rpn = ToRpn(tokens);
		return EvalRpn(rpn);
	}

	private static IEnumerable<string> Tokenize(string expr)
	{
		int i = 0;
		while (i < expr.Length)
		{
			char c = expr[i];
			if (char.IsWhiteSpace(c)) { i++; continue; }
			if (char.IsDigit(c) || c == '.')
			{
				int start = i;
				while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
				yield return expr[start..i];
				continue;
			}
			if (c is '+' or '-' or '*' or '/' or '(' or ')')
			{
				yield return c.ToString();
				i++;
				continue;
			}
			throw new FormatException($"Unexpected character '{c}' at position {i}");
		}
	}

	private static Queue<string> ToRpn(IEnumerable<string> tokens)
	{
		var output = new Queue<string>();
		var ops = new Stack<string>();
		foreach (var t in tokens)
		{
			if (double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
			{
				output.Enqueue(t);
			}
			else if (IsOperator(t))
			{
				while (ops.Count > 0 && IsOperator(ops.Peek()) && ((IsLeftAssociative(t) && Precedence(t) <= Precedence(ops.Peek())) || (!IsLeftAssociative(t) && Precedence(t) < Precedence(ops.Peek()))))
				{
					output.Enqueue(ops.Pop());
				}
				ops.Push(t);
			}
			else if (t == "(")
			{
				ops.Push(t);
			}
			else if (t == ")")
			{
				while (ops.Count > 0 && ops.Peek() != "(")
				{
					output.Enqueue(ops.Pop());
				}
				if (ops.Count == 0 || ops.Pop() != "(") throw new FormatException("Mismatched parentheses");
			}
			else
			{
				throw new FormatException($"Unexpected token '{t}'");
			}
		}
		while (ops.Count > 0)
		{
			var op = ops.Pop();
			if (op is "(" or ")") throw new FormatException("Mismatched parentheses");
			output.Enqueue(op);
		}
		return output;
	}

	private static double EvalRpn(Queue<string> rpn)
	{
		var stack = new Stack<double>();
		while (rpn.Count > 0)
		{
			var t = rpn.Dequeue();
			if (double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out var num))
			{
				stack.Push(num);
			}
			else if (IsOperator(t))
			{
				if (stack.Count < 2) throw new FormatException("Insufficient operands");
				var b = stack.Pop();
				var a = stack.Pop();
				stack.Push(t switch
				{
					"+" => a + b,
					"-" => a - b,
					"*" => a * b,
					"/" => b == 0 ? throw new DivideByZeroException() : a / b,
					_ => throw new InvalidOperationException("Unknown operator")
				});
			}
			else
			{
				throw new FormatException($"Unexpected token in RPN '{t}'");
			}
		}
		if (stack.Count != 1) throw new FormatException("Invalid expression");
		return stack.Pop();
	}

	private static bool IsOperator(string t) => t is "+" or "-" or "*" or "/";
	private static int Precedence(string op) => op switch { "+" or "-" => 1, "*" or "/" => 2, _ => 0 };
	private static bool IsLeftAssociative(string op) => true; // all defined operators left associative
}
