﻿using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents.Validata
{
    public class MobileAttribute : AbstractValidataAttribute
    {
        private static string _Reg = "^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\\d{8}$";
        public MobileAttribute()
            : base(Rule)
        {

        }
        /// <summary>
        /// 可以用户自定义正则
        /// </summary>
        /// <param name="reg">自定义正则</param>
        public MobileAttribute(string reg)
          : base(Rule)
        {
            _Reg = reg;
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
                validataErrorModel.ErrorMsg = "为空！";
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
