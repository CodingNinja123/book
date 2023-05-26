using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Interpreter
{
    public class NumberExpression : IExpression
    {
        private int number;

        public NumberExpression(int number)
        {
            this.number = number;
        }

        public int Interpret()
        {
            return number;
        }
    }
}
