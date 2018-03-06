// Copyright (c) 2018 Vladislav Prekel
// Copyright http://megadarja.blogspot.fr/2010/03/eval-net.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace MyExpression.Core
{
	public class CodeDomEval : IFunctionX
	{
		public CompilerResults CompilerResults { get; private set; }

		public bool IsSuccessfulBuild { get; set; }

		/// <summary>
		/// Исходник, который будем компилировать
		/// </summary>
		private const string SourceFormat = @"
using System;

namespace Evaluation
{
	public class Evaluator
	{
		public double Evaluate(double x)
		{
			return [|<expression>|];
		}
	}
}";
		private string ReformExpression(string e)
		{
			var sb = new StringBuilder(e);
			sb.Replace(" ", "");
			sb.Replace("abs", "Math.Abs");
			sb.Replace("acos", "Math.Acos");
			sb.Replace("asin", "Math.Asin");
			sb.Replace("atan", "Math.Atan");
			sb.Replace("atan2", "Math.Atan2");
			sb.Replace("bigmul", "Math.BigMul");
			sb.Replace("ceiling", "Math.Ceiling");
			sb.Replace("cos", "Math.Cos");
			sb.Replace("cosh", "Math.Cosh");
			sb.Replace("divrem", "Math.DivRem");
			sb.Replace("e", "Math.E");
			sb.Replace("exp", "Math.Exp");
			sb.Replace("floor", "Math.Floor");
			sb.Replace("ieeeremainder", "Math.IEEERemainder");
			sb.Replace("log", "Math.Log");
			sb.Replace("log10", "Math.Log10");
			sb.Replace("max", "Math.Max");
			sb.Replace("min", "Math.Min");
			sb.Replace("pi", "Math.PI");
			sb.Replace("pow", "Math.Pow");
			sb.Replace("round", "Math.Round");
			sb.Replace("sign", "Math.Sign");
			sb.Replace("sin", "Math.Sin");
			sb.Replace("sinh", "Math.Sinh");
			sb.Replace("sqrt", "Math.Sqrt");
			sb.Replace("tan", "Math.Tan");
			sb.Replace("tanh", "Math.Tanh");
			sb.Replace("truncate", "Math.Truncate");
			return sb.ToString();
		}
	
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="expression">Выражение, которое будем вычислять</param>
		public CodeDomEval(string expression)
		{
			//var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
			var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };
			//var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v7.1" } };
			var provider = new CSharpCodeProvider(providerOptions);

			// Компиляция сборки с вычисляющим классом
			var compilerParams = CreateCompilerParameters();
			//var src = String.Format(SourceFormat, expression);
			var rfex = ReformExpression(expression);
			var src = SourceFormat.Replace("[|<expression>|]", rfex);
			CompilerResults = provider.CompileAssemblyFromSource(compilerParams, src);

			if (CompilerResults.Errors.Count == 0)
			{
				IsSuccessfulBuild = true;
			}
			else
			{
				var sb = new StringBuilder();
				// Сбор ошибок компиляции
				foreach (CompilerError error in CompilerResults.Errors)
				{
					sb.Append(error + "\n");
				}
				throw new ApplicationException("Ошибка сборки\n" + sb);
			}

			if (!IsSuccessfulBuild) throw new Exception("Ошибка сборки");
			// загружаем сборку
			var assembly = CompilerResults.CompiledAssembly;
			var type = assembly.GetType("Evaluation.Evaluator");

			// создаем экземпляр сгенерированного класса
			Instance = assembly.CreateInstance("Evaluation.Evaluator");

			// вызываем метод для вычисления нашей функции с заданными параметрами
			Method = type.GetMethod("Evaluate");
		}

		// TODO: возможно надо сделать приватными
		public object Instance { get; private set; }
		public MethodInfo Method { get; private set; }

		/// <summary>
		/// Метод для проведения вычисления
		/// </summary>
		public double Calculate(double x)
		{
			return (double)Method.Invoke(Instance, new object[] { x });
		}

		/// <summary>
		/// Создание параметров компиляции
		/// </summary>
		private CompilerParameters CreateCompilerParameters()
		{
			var compilerParams = new CompilerParameters()
			{
				CompilerOptions = "/target:library /optimize",
				GenerateExecutable = false,
				GenerateInMemory = true,
				IncludeDebugInformation = false
			};
			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");

			return compilerParams;
		}
	}
}

