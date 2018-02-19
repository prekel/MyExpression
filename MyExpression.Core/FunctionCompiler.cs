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
	public class FunctionCompiler
	{
		CompilerResults compilerResults;

		/// <summary>
		/// Исходник, который будем компилировать
		/// </summary>
		string SourceFormat = @"
using System;

namespace Evaluation
{{
public class Evaluator
{{
public double Evaluate(double[] args)
{{
  return {0};
}}
}}
}}";
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="expression">Выражение, которое будем вычислять</param>
		public FunctionCompiler(string expression)
		{
			//var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
			var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v7.1" } };
			var provider = new CSharpCodeProvider(providerOptions);

			// Компиляция сборки с вычисляющим классом
			var compilerParams = CreateCompilerParameters();
			var src = String.Format(SourceFormat, expression);
			compilerResults = provider.CompileAssemblyFromSource(compilerParams, src);

			var sb = new StringBuilder();
			// Сбор ошибок компиляции
			foreach (CompilerError error in compilerResults.Errors)
			{
				sb.Append(error.ErrorText + "\n");
			}
			throw new Exception(sb.ToString());
		}

		/// <summary>
		/// Метод для проведения вычисления
		/// </summary>
		public double? Eval(object[] args)
		{
			//if (compilerResults != null && !compilerResults.Errors.HasErrors && compilerResults.CompiledAssembly != null)
			//{
			// загружаем сборку
			var assembly = compilerResults.CompiledAssembly;
			var type = assembly.GetType("Evaluation.Evaluator");

			// создаем экземпляр сгенерированного класса
			object instance = assembly.CreateInstance("Evaluation.Evaluator");

			// вызываем метод для вычисления нашей функции с заданными параметрами
			var method = type.GetMethod("Evaluate");
			var result = (double)method.Invoke(instance, args);

			// PROFIT
			return result;
			//}
			//return null;
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

