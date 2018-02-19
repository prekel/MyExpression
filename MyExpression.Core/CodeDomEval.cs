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
	public class CodeDomEval
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
			var src = SourceFormat.Replace("[|<expression>|]", expression);
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
					sb.Append(error.ErrorText + "\n");
				}
				throw new Exception("Ошибка сборки\n" + sb);
			}
		}

		/// <summary>
		/// Метод для проведения вычисления
		/// </summary>
		public double Eval(double x)
		{
			if (!IsSuccessfulBuild) throw new Exception("Ошибка сборки");
			// загружаем сборку
			var assembly = CompilerResults.CompiledAssembly;
			var type = assembly.GetType("Evaluation.Evaluator");

			// создаем экземпляр сгенерированного класса
			object instance = assembly.CreateInstance("Evaluation.Evaluator");

			// вызываем метод для вычисления нашей функции с заданными параметрами
			var method = type.GetMethod("Evaluate");
			var result = (double)method.Invoke(instance, new object[] { x });

			// PROFIT
			return result;
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

