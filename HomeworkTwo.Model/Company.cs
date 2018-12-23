using HomeworkTwo.Comm;
using System;
using System.Collections.Generic;
using HomeworkTwo.Comm.AttributeExtents;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Model
{
    public class Company : BaseModel
    {
        [DisplayName("客户名称")]
        [Name("Name")]
        public string Name { get; set; }
        [DisplayName("创建时间")]
        [Name("CreateTime")]
        public DateTime CreateTime { get; set; }
        [DisplayName("创建人编号")]
        [Name("CreatorId")]
        public int CreatorId { get; set; }
        [DisplayName("最后修改人编号")]
        [Name("LastModifierId")]
        public int? LastModifierId { get; set; }
        [DisplayName("最后修改时间")]
        [Name("LastModifyTime")]
        public DateTime? LastModifyTime { get; set; } 
    }
}
