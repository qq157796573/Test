using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents.Validata
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class AbstractValidataAttribute : Attribute
    {
        private Func<object, ValidataErrorModel> _Func = null;
        public AbstractValidataAttribute(Func<object, ValidataErrorModel> func)
        {
            this._Func = func;
        }
        public ValidataErrorModel Validata(object oValue)
        {
            return this._Func.Invoke(oValue);
        }
    }
    
}
