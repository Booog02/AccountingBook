using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Models
{
    internal class DropdownModel
    {
        public List<string> Categories { get; set; }
        public List<string> Targets { get; set; }
        public List<string> Payments { get; set; }
    }
}
