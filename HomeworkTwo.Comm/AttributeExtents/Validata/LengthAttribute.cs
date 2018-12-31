using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents.Validata
{

    public class LengthAttribute : AbstractValidataAttribute
    {
        private static int _Min;
        private static int _Max;
        public LengthAttribute(int min, int max) : base(Rule)
        {
            _Min = min;
            _Max = max;
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
            else if (error.ToString().Length < _Min)
            {
                validataErrorModel.IsError = true;
                validataErrorModel.ErrorMsg = $"长度不能小于{_Min}个长度！";
            }
            else if (error.ToString().Length > _Max)
            {
                validataErrorModel.IsError = true;
                validataErrorModel.ErrorMsg = $"长度不能大于{_Max}个长度！";
            }
            return validataErrorModel;
        }

    }
}
