using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Interpreter
{
    public class Parser
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
                        // it's an addition, pop two elements from the stack, perform the addition, push result back
                        IExpression rightPlus = stack.Pop();
                        IExpression leftPlus = stack.Pop();
                        IExpression plus = new Plus(leftPlus, rightPlus);
                        stack.Push(plus);
                        break;

                    case "-":
                        // it's a subtraction, pop two elements from the stack, perform the subtraction, push result back
                        IExpression rightMinus = stack.Pop();
                        IExpression leftMinus = stack.Pop();
                        IExpression minus = new Minus(leftMinus, rightMinus);
                        stack.Push(minus);
                        break;

                    default:
                        // it's a number, push it to the stack
                        stack.Push(new Number(int.Parse(tokens[i])));
                        break;
                }
            }

            return stack.Pop();
        }
    }
}
