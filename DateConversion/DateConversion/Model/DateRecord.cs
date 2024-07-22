using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateConversion.Model
{
    public class DateRecord
    {       
        public int Id { get; set; }
        public DateTime OriginalDate { get; set; }
        public string? DiffDate { get; set; }
    }
}
