using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents.Validata
{
    public class RequiredAttribute : AbstractValidataAttribute
    {
        public RequiredAttribute()
            : base(Rule)
        {

        }
        public static ValidataErrorModel Rule(object error)
        {
            ValidataErrorModel validataErrorModel = new ValidataErrorModel()
            {
                IsError = false,
                ErrorMsg = null
            };
            if (error is null || string.IsNullOrWhiteSpace(error.ToString()))
            {
                validataErrorModel.IsError = true;
                validataErrorModel.ErrorMsg = "该属性为空";
            }
            return validataErrorModel;
        }
    }
}
