# MyExpression
|Branch|Build|
|:----:|:---:|
| `latest` | [![Build status](https://ci.appveyor.com/api/projects/status/y44s4vmjr7nvnyk0?svg=true)](https://ci.appveyor.com/project/prekel/myexpression) |
| `master` | [![Build status](https://ci.appveyor.com/api/projects/status/y44s4vmjr7nvnyk0/branch/master?svg=true)](https://ci.appveyor.com/project/prekel/myexpression/branch/master) |
| `develop` | [![Build status](https://ci.appveyor.com/api/projects/status/y44s4vmjr7nvnyk0/branch/develop?svg=true)](https://ci.appveyor.com/project/prekel/myexpression/branch/develop) |

Решение алгебраических уравнений любой степени с заданной точностью
```c#
var s = "x^3-2x^2-x+2";
var eps = 1e-8;
var p = Polynomial.Parse(s);
var e = new PolynomialEquation(p, eps);
e.Solve();
```
