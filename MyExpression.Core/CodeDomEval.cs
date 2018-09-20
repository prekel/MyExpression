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
            sb.Replace("abs", "temp00");
            sb.Replace("acos", "temp01");
            sb.Replace("acosh", "temp02");
            sb.Replace("asin", "temp03");
            sb.Replace("asinh", "temp04");
            sb.Replace("atan", "temp05");
            sb.Replace("atan2", "temp06");
            sb.Replace("atanh", "temp07");
            sb.Replace("bigmul", "temp08");
            sb.Replace("cbrt", "temp09");
            sb.Replace("ceiling", "temp10");
            sb.Replace("clamp", "temp11");
            sb.Replace("cos", "temp12");
            sb.Replace("cosh", "temp13");
            sb.Replace("divrem", "temp14");
            sb.Replace("equals", "temp15");
            sb.Replace("exp", "temp16");
            sb.Replace("floor", "temp17");
            sb.Replace("gethashcode", "temp18");
            sb.Replace("gettype", "temp19");
            sb.Replace("ieeeremainder", "temp20");
            sb.Replace("log", "temp21");
            sb.Replace("log10", "temp22");
            sb.Replace("max", "temp23");
            sb.Replace("min", "temp24");
            sb.Replace("pow", "temp25");
            sb.Replace("round", "temp26");
            sb.Replace("sign", "temp27");
            sb.Replace("sin", "temp28");
            sb.Replace("sinh", "temp29");
            sb.Replace("sqrt", "temp30");
            sb.Replace("tan", "temp31");
            sb.Replace("tanh", "temp32");
            sb.Replace("tostring", "temp33");
            sb.Replace("truncate", "temp34");
            sb.Replace("temp00", "Math.Abs");
            sb.Replace("temp01", "Math.Acos");
            sb.Replace("temp02", "Math.Acosh");
            sb.Replace("temp03", "Math.Asin");
            sb.Replace("temp04", "Math.Asinh");
            sb.Replace("temp05", "Math.Atan");
            sb.Replace("temp06", "Math.Atan2");
            sb.Replace("temp07", "Math.Atanh");
            sb.Replace("temp08", "Math.BigMul");
            sb.Replace("temp09", "Math.Cbrt");
            sb.Replace("temp10", "Math.Ceiling");
            sb.Replace("temp11", "Math.Clamp");
            sb.Replace("temp12", "Math.Cos");
            sb.Replace("temp13", "Math.Cosh");
            sb.Replace("temp14", "Math.DivRem");
            sb.Replace("temp15", "Math.Equals");
            sb.Replace("temp16", "Math.Exp");
            sb.Replace("temp17", "Math.Floor");
            sb.Replace("temp18", "Math.GetHashCode");
            sb.Replace("temp19", "Math.GetType");
            sb.Replace("temp20", "Math.IEEERemainder");
            sb.Replace("temp21", "Math.Log");
            sb.Replace("temp22", "Math.Log10");
            sb.Replace("temp23", "Math.Max");
            sb.Replace("temp24", "Math.Min");
            sb.Replace("temp25", "Math.Pow");
            sb.Replace("temp26", "Math.Round");
            sb.Replace("temp27", "Math.Sign");
            sb.Replace("temp28", "Math.Sin");
            sb.Replace("temp29", "Math.Sinh");
            sb.Replace("temp30", "Math.Sqrt");
            sb.Replace("temp31", "Math.Tan");
            sb.Replace("temp32", "Math.Tanh");
            sb.Replace("temp33", "Math.ToString");
            sb.Replace("temp34", "Math.Truncate");

            return sb.ToString();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="expression">Выражение, которое будем вычислять</param>
        public CodeDomEval(string expression)
        {
            //var providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } };
            var providerOptions = new Dictionary<string, string>() {{"CompilerVersion", "v4.0"}};
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
            return (double) Method.Invoke(Instance, new object[] {x});
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