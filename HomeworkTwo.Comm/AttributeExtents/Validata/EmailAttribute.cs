using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents.Validata
{
    public class EmailAttribute : AbstractValidataAttribute
    {
        private static string _Reg = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public EmailAttribute():base(Rule)
        {

        }
        private static ValidataErrorModel Rule(object error)
        {
            ValidataErrorModel validataErrorModel = new ValidataErrorModel()
            {
                IsError = false,
                ErrorMsg = null
            };
            if (error is null)
            {
                validataErrorModel.IsError = true;
                validataErrorModel.ErrorMsg = "不能为空！";
            }
            else if (!Regex.IsMatch(error.ToString(), _Reg))
            {
                validataErrorModel.IsError = true;
                validataErrorModel.ErrorMsg = "格式不正确！";
            }
            return validataErrorModel;
        }
    }
}
