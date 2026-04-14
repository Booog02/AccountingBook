using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Models.DTO
{
    internal class RecordModelDTO
    {
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
        public string Detail { get; set; }
        public string Target { get; set; }
        public string Payment { get; set; }
        public Image ImagePath1 { get; set; }
        public Image ImagePath2 { get; set; }
    }
}
