using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingBook.Attributes;
using AccountingBook.Models;
using CSVLibrary;

namespace CSVLibrary
{
    public class RecordModel
    {
        [DisplayName("日期")]
        public string Date { get; set; }

        [DisplayName("金額")]
        public string Amount { get; set; }

        [DisplayName("類型")]
        [ComboBoxColumn]
        public string Category { get; set; }

        [DisplayName("細項")]
        [ComboBoxColumn]
        public string Detail { get; set; }

        [DisplayName("對象")]
        [ComboBoxColumn]
        public string Target { get; set; }

        [DisplayName("付款方式")]
        [ComboBoxColumn]
        public string Payment { get; set; }

        [DisplayName("圖片1")]
        [ImageColumn]
        public string ImagePath1 { get; set; }

        [DisplayName("圖片2")]
        [ImageColumn]
        public string ImagePath2 { get; set; }

    }
}
