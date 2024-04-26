using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class EmployeeModel
    {
        public string PARAM { get; set; } = string.Empty;
        public Int32 RowNum { get; set; }
        public int ID { get; set; }
        public string NAME { get; set; } = string.Empty;
        public int AGE { get; set; }
        public string NUMBER { get; set; } = string.Empty;
        public string ADDRESS { get; set; } = string.Empty;
        public int ACTIVE { get; set; }
    }
}
