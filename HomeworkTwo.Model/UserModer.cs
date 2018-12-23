using HomeworkTwo.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Model
{
    [Name("User")]
    public class UserModer : BaseModel
    {
        [DisplayName("用户名")]
        [Name("Name")]
        public string Name { get; set; }
        [DisplayName("账号")]
        [Name("Account")]
        public string Account { get; set; }
        [DisplayName("密码")]
        [Name("Password")]
        public string Password { get; set; }
        [DisplayName("邮箱")]
        [Name("Email")]
        public string Email { get; set; }
        [DisplayName("手机号码")]
        [Name("Mobile")]
        public string Mobile { get; set; }
        [DisplayName("客户编号")]
        [Name("CompanyId")]
        public int? CompanyId { get; set; }
        [DisplayName("客户名称")]
        [Name("CompanyName")]
        public string CompanyName { get; set; }
        [DisplayName("用户状态")]
        [Name("State")]
        public int State { get; set; }
        [DisplayName("用户类型")]
        [Name("UserType")]
        public int UserType { get; set; }
        [DisplayName("最后登录时间")]
        [Name("LastLoginTime")]
        public DateTime? LastLoginTime { get; set; }
        [DisplayName("创建时间")]
        [Name("CreateTime")]
        public DateTime CreateTime { get; set; }
        [DisplayName("创建人编号")]
        [Name("CreatorId")]
        public int CreatorId { get; set; }
        [DisplayName("最有修改人编号")]
        [Name("LastModifierId")]
        public int? LastModifierId { get; set; }
        [DisplayName("最后修改时间")]
        [Name("LastModifyTime")]
        public DateTime? LastModifyTime { get; set; }
    }
}
