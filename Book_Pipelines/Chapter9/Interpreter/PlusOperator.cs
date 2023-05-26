using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Interpreter
{
    public class PlusOperator : IExpression
    {
        private IExpression leftExpression;
        private IExpression rightExpression;

        public PlusOperator(IExpression leftExpression, IExpression rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public int Interpret()
        {
            return leftExpression.Interpret() + rightExpression.Interpret();
        }
    }
}
