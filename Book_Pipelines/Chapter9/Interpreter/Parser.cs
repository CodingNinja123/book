using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Interpreter
{
    public class ExpressionParser
    {
        public IExpression Parse(string expression)
        {
            Stack<IExpression> stack = new Stack<IExpression>();

            string[] tokens = expression.Split(' ');

            for (int i = 0; i < tokens.Length; i++)
            {
                switch (tokens[i])
                {
                    case "+":
                        IExpression rightPlus = stack.Pop();
                        IExpression leftPlus = stack.Pop();
                        IExpression plus = new PlusOperator(leftPlus, rightPlus);
                        stack.Push(plus);
                        break;
                    case "-":
                        IExpression rightMinus = stack.Pop();
                        IExpression leftMinus = stack.Pop();
                        IExpression minus = new MinusOperator(leftMinus, rightMinus);
                        stack.Push(minus);
                        break;
                    default:
                        stack.Push(new NumberExpression(int.Parse(tokens[i])));
                        break;
                }
            }
            return stack.Pop();
        }
    }
}
