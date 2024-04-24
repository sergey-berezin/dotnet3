using Flee.PublicTypes;
using System;
using System.Linq;

namespace Evaluator
{
    public static class Evaluator
    {
        public static (double[], double[]) EvaluateSeries(string expression)
        {
            ExpressionContext context = new ExpressionContext();
            context.Variables["x"] = double.NaN;
            IGenericExpression<double> e = context.CompileGeneric<double>(expression);

            var x = Enumerable.Range(0, 101).Select(i => -1 + i / 50.0).ToArray();
            var y = x.Select(xx =>
            {
                context.Variables["x"] = xx;
                return e.Evaluate();
            }).ToArray();


            return (x, y);
        }

    }
}
