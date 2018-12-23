using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm
{
    public class EnumComm
    {
        public enum UserState
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Name("正常")]
            Normal = 0,
            /// <summary>
            /// 冻结
            /// </summary>
            [Name("冻结")]
            Frozen = 1,
            /// <summary>
            /// 删除
            /// </summary>
            [Name("删除")]
            Delete = 2
        }
    }
}
