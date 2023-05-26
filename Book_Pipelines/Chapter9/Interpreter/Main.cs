using System;
using System.Collections.Generic;
namespace Book_Pipelines.Chapter9.Interpreter
{
    
    public class InterpreterSection
    {
        public static void Main()
        {
            string expression = "2 3 1 - +";
            ExpressionParser parser = new ExpressionParser();
            IExpression expr = parser.Parse(expression);

            Console.WriteLine("The result is: " + expr.Interpret());
        }
    }
}
