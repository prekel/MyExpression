using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MyExpression.Core;

namespace MyExpression.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolynomialEquationController : ControllerBase
    {
        private readonly ILogger<PolynomialEquationController> _logger;

        public PolynomialEquationController(ILogger<PolynomialEquationController> logger) => _logger = logger;

        [HttpGet]
        public IEnumerable<double> Index(string equation, double eps)
        {
            var eq = new PolynomialEquation(Polynomial.Parse(equation), eps);
            eq.Solve();
            _logger.LogInformation($"{equation}; eps: {eps}; Roots: {String.Join(", ", eq.Roots)}");
            return eq.Roots;
        }
    }
}
