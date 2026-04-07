using AccountingBook.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(SingletonForm.GetForm("記一筆"));

            //56k數據機 撥接網路
            //ADSL 非對稱式數位網路 (下載/上傳)
            //10Mbps(bit) 20M/8 => 1.25MB
            //迅雷下載 => 邊撥邊下載
            //P2P(點對點傳輸) FOXY BT種子 彼特彗星
            //串流 Streaming => 存放到記憶體裡面 (Buffer 緩衝區)
            //m3u8
        }
    }
}
