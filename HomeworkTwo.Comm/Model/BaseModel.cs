using HomeworkTwo.Comm;
using HomeworkTwo.Comm.AttributeExtents;
using System;
using System.Collections.Generic;    
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.Model
{
    public class BaseModel
    {
        [DisplayName("编号")]
        [Name("Id")]
        public int Id { get; set; }
    }
}
