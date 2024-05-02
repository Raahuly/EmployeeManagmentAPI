using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class MapTableModel
    {
        public Int32 RowNum { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int Active { get; set; }
        public bool isChecked { get; set; } = false;
        public int USERID { get; set;}
    }
}
