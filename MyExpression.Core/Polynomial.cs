using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyExpression.Core
{
	/// <summary>
	/// Многочлен
	/// </summary>
	public class Polynomial : IFunctionX, IDerivativable, IEnumerable<Monomial>
	{
		private SortedDictionary<double, Monomial> Data { get; set; } = new SortedDictionary<double, Monomial>();

		/// <summary>
		/// Степень
		/// </summary>
		public double Degree => Data.Last().Value.Degree;

		/// <summary>
		/// Одночлен многочлена по степени
		/// </summary>
		/// <param name="degree">Степень одночлена</param>
		public Monomial this[double degree]
		{
			get
			{
				if (Data.ContainsKey(degree))
				{
					return Data[degree];
				}
				else
				{
					return new Monomial(0, degree);
				}
			}
		}

		/// <summary>
		/// Создаёт нулевой многочлен
		/// </summary>
		public Polynomial()
		{
		}

		/// <summary>
		/// Создаёт копию многочлена
		/// </summary>
		/// <param name="a">Копируемый многочлен</param>
		public Polynomial(Polynomial a)
		{
			foreach (var i in a)
			{
				Data.Add(i.Degree, new Monomial(i.Coefficient, i.Degree));
			}
		}

		/// <summary>
		/// Создаёт многочлен со степенями от n - 1 до 0, где n - ко-во параметров
		/// </summary>
		/// <param name="v">Коэффиценты одночленов</param>
		public Polynomial(params double[] v)
		{
			for (var i = v.Length - 1; i >= 0; i--)
			{
				Add(new Monomial(v[v.Length - i - 1], i));
			}
		}

		/// <summary>
		/// Создаёт нулевой многочлен используя заданный режим вычисления
		/// </summary>
		/// <param name="mode">Режим вычисления</param>
		public Polynomial(CalculateMode mode) : this()
		{
			Mode = mode;
		}

		/// <summary>
		/// Создаёт копию многочлена используя заданный режим вычисления
		/// </summary>
		/// <param name="a">Копируемый многочлен</param>
		/// <param name="mode">Режим вычисления</param>
		public Polynomial(Polynomial a, CalculateMode mode) : this(a)
		{
			Mode = mode;
		}
		
		/// <summary>
		/// Создаёт многочлен со степенями от n - 1 до 0, где n - ко-во параметров используя заданный режим вычисления
		/// </summary>
		/// <param name="mode">Режим вычисления</param>
		/// <param name="v">Коэффиценты одночленов</param>
		public Polynomial(CalculateMode mode, params double[] v) : this(v)
		{
			Mode = mode;
		}

		/// <summary>
		/// Режим вычисления
		/// </summary>
		public enum CalculateMode
		{
			/// <summary>
			/// Вычисление значение в лоб
			/// </summary>
			Manual = 1,
			/// <summary>
			/// Компиляция в CodeDomEval
			/// </summary>
			Compile = 2
		}

		/// <summary>
		/// Режим вычисления
		/// </summary>
		public CalculateMode Mode { get; set; } = CalculateMode.Manual;

		/// <summary>
		/// Вычисляет значение в лоб
		/// </summary>
		/// <param name="x">Независимая переменная</param>
		/// <returns>Значение многочлена</returns>
		public double ManualCalculate(double x) => Data.Values.Sum(m => m.Calculate(x));

		private CodeDomEval Evaluator { get; set; }

		/// <summary>
		/// Является ли скомпилированным
		/// </summary>
		public bool IsCompiled { get; private set; }

		/// <summary>
		/// Компилирует многочлен
		/// </summary>
		public void Compile()
		{
			var s = String.Join(" + ", from i in Data.Values select $"({i.Coefficient.ToString(System.Globalization.CultureInfo.InvariantCulture)}*Math.Pow(x, {i.Degree.ToString(System.Globalization.CultureInfo.InvariantCulture)}))");
			Evaluator = new CodeDomEval(s);
			IsCompiled = true;
		}

		/// <summary>
		/// Вычисляет значение через CodeDomEval
		/// </summary>
		/// <param name="x">Независимая переменнная</param>
		/// <returns>Значение многочлена</returns>
		public double Evaluate(double x) => Evaluator.Calculate(x);

		/// <summary>
		/// Вычисляет значение многочлена
		/// </summary>
		/// <param name="x">Независимая переменная</param>
		/// <returns>Значение многочлена</returns>
		public double Calculate(double x)
		{
			if (Mode == CalculateMode.Compile)
			{
				if (!IsCompiled) Compile();
				return Evaluator.Calculate(x);
			}
			return ManualCalculate(x);
		}

		/// <summary>
		/// Удаляет нулевые одночлены
		/// </summary>
		public void DeleteZeros()
		{
			IsCompiled = false;

			//var zk = new List<double>();
			//foreach (var i in Data)
			//{
			//	if (i.Value.Coefficient == 0)
			//	{
			//		zk.Add(i.Key);
			//	}
			//}
			//foreach (var i in zk)
			//{
			//	Data.Remove(i);
			//}

			Data = new SortedDictionary<double, Monomial>(
				(from i in Data where i.Value.Coefficient != 0 select i)
				.ToDictionary(x => x.Value.Degree, y => y.Value)
			);
		}

		/// <summary>
		/// Прибавляет одночлен
		/// </summary>
		/// <param name="a">Одночлен</param>
		public void Add(Monomial a)
		{
			IsCompiled = false;
			if (Data.ContainsKey(a.Degree))
			{
				if (Data[a.Degree].Coefficient + a.Coefficient == 0)
				{
					Data.Remove(a.Degree);
				}
				else
				{
					Data[a.Degree].Add(a);
				}
			}
			else
			{
				Data.Add(a.Degree, a);
			}
		}

		/// <summary>
		/// Вычитает одночлен
		/// </summary>
		/// <param name="a">Одночлен</param>
		public void Sub(Monomial a)
		{
			IsCompiled = false;
			if (Data.ContainsKey(a.Degree))
			{
				if (Data[a.Degree].Coefficient - a.Coefficient == 0)
				{
					Data.Remove(a.Degree);
				}
				else
				{
					Data[a.Degree].Add(-a);
				}
			}
			else
			{
				Data.Add(a.Degree, -a);
			}
		}

		/// <summary>
		/// Производная
		/// </summary>
		public IFunctionX Derivative
		{
			get
			{
				var d = new Polynomial();
				foreach (var i in Data.Values)
				{
					d.Add((Monomial)i.Derivative);
				}
				return d;
			}
		}

		/// <summary>
		/// Получает многочлен из строки, например из x^5-6x^3-0.5x^2+1
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Polynomial Parse(string p)
		{
			if (p[0] != '-' && p[0] != '+') p = "+" + p;
			var s1 = p.Split(new char[] { '-', '+' }, StringSplitOptions.RemoveEmptyEntries);

			var j = 0;
			for (var i = 0; i < s1.Length; i++)
			{
				s1[i] = p[j] + s1[i];
				j += s1[i].Length;
			}

			var pl = new Polynomial();
			foreach (var i in s1)
			{
				pl.Add(Monomial.Parse(i));
			}
			return pl;
		}

		/// <summary>
		/// Складывает многочлены, a + b
		/// </summary>
		/// <param name="a">Многочлен</param>
		/// <param name="b">Многочлен</param>
		/// <returns>Сумма многочленов</returns>
		public static Polynomial operator +(Polynomial a, Polynomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in b)
			{
				p.Add(i);
			}
			return p;
		}

		/// <summary>
		/// Вычитает многочлены a - b
		/// </summary>
		/// <param name="a">Многочлен</param>
		/// <param name="b">Многочлен</param>
		/// <returns>Разность многочленов</returns>
		public static Polynomial operator -(Polynomial a, Polynomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in b)
			{
				p.Sub(i);
			}
			return p;
		}

		/// <summary>
		/// Складывает многочлен и одночлен, a + b
		/// </summary>
		/// <param name="a">Многочлен a</param>
		/// <param name="b">Одночлен b</param>
		/// <returns>Сумма</returns>
		public static Polynomial operator +(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			p.Add(b);
			return p;
		}
		
		/// <summary>
		/// Вычитает многочлен и одночлен, a - b
		/// </summary>
		/// <param name="a">Многочлен a</param>
		/// <param name="b">Одночлен b</param>
		/// <returns>Разность</returns>
		public static Polynomial operator -(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			p.Sub(b);
			return p;
		}

		/// <summary>
		/// Складывает одночлен и многочлен, b + a
		/// </summary>
		/// <param name="a">Одночлен b</param>
		/// <param name="b">Многочлен a</param>
		/// <returns>Сумма</returns>
		public static Polynomial operator +(Monomial b, Polynomial a)
		{
			return a + b;
		}
		
		/// <summary>
		/// Вычитает одночлен и многочлен (не доделано)
		/// </summary>
		/// <param name="a">Одночлен a</param>
		/// <param name="b">Многочлен b</param>
		/// <returns>Разность</returns>
		[Obsolete]
		public static Polynomial operator -(Monomial b, Polynomial a)
		{
			return a - b;
		}

		/// <summary>
		/// Унарный минус
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static Polynomial operator -(Polynomial a)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Coefficient *= -1;
			}
			return p;
		}

		public static Polynomial operator +(Polynomial a)
		{
			return a;
		}

		public static Polynomial operator *(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Multiply(b);
			}
			p.DeleteZeros();
			return p;
		}

		public static Polynomial operator *(Monomial a, Polynomial b)
		{
			return b * a;
		}

		public static Polynomial operator *(Polynomial a, double b)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Multiply(b);
			}
			p.DeleteZeros();
			return p;
		}

		public static Polynomial operator *(double a, Polynomial b)
		{
			return b * a;
		}

		public override string ToString()
		{
			var s = "";
			foreach (var i in Data.Values.OrderByDescending(i => i))
			{
				if (i.Coefficient >= 0 && s != "")
				{
					s += "+";
				}
				s += i.ToString();
			}
			return s;
		}

		public SquareEquation SquareEquation => ToSquareEquation();

		public SquareEquation ToSquareEquation()
		{
			if (Degree != 2)
				throw new InvalidOperationException();
			return new SquareEquation(this[2].Coefficient, this[1].Coefficient, this[0].Coefficient);
		}

		public IEnumerator<Monomial> GetEnumerator()
		{
			return Data.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Data.Values.GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Polynomial)) return false;
			var p = (Polynomial)obj;
			if (p.Degree != p.Degree) return false;
			for (var i = 0d; i <= Math.Max(Degree, p.Degree); i++)
			{
				if (this[i].Coefficient != p[i].Coefficient)
					return false;
			}
			return true;
		}

		public bool Equals(Polynomial p, double epscoef = 0)
		{
			if (p.Degree != p.Degree) return false;
			for (var i = 0d; i <= Math.Max(Degree, p.Degree); i++)
			{
				if (Math.Abs(this[i].Coefficient - p[i].Coefficient) > epscoef)
					return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
