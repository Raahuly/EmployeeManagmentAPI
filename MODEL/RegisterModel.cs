using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class RegisterModel
    {
        public int ID { get; set; }
        public string USER { get; set; }
        public string PASSWORD { get; set; }
        public DateTime CREATED_BY { get; set; }
        public DateTime UPDATED_BY { get; set; }
    }
}
