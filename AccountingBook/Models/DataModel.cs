using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Models
{
    internal class DataModel
    {

        public static List<string> Category = new List<string>
        {
            "餐飲",
            "交通",
            "娛樂",
            "生活用品",
            "住宿",
            "醫療"
        };

        public static Dictionary<string, List<string>> TypeDetails = new Dictionary<string, List<string>>()
        {
            {
                "餐飲",new List<string>
                {
                    "早餐",
                    "午餐",
                    "晚餐",
                    "霄夜",
                    "外送",
                    "飲料",
                    "聚餐"
                }
            },
            {
                "生活用品",new List<string>
                {
                    "日用品",
                    "衣著",
                    "文具",
                    "家電"
                }

            },
            {
                "住宿",new List<string>
                {
                    "房租",
                    "旅館",
                    "民宿",
                    "水費",
                    "電費",
                    "網路費"
                }
            },
            {
                "交通",new List<string>
                {
                    "捷運",
                    "公車",
                    "高鐵",
                    "火車",
                    "計程車",
                    "油錢",
                    "停車費"

                }
            },
            {
                "娛樂",new List<string>
                {
                    "電影",
                    "遊戲",
                    "演唱會",
                    "展覽",
                    "平台訂閱",
                    "書籍",

                }

            },
            {
                "醫療",new List<string>
                {
                    "掛號費",
                    "藥品",
                    "看診",
                    "保健食品",
                    "健康檢查"
                }
            }
        };

        public static List<string> Target = new List<string>
        {
            "自己",
            "家人",
            "朋友",
            "公司"
        };

        public static List<string> Payment = new List<string>
        {
            "現金",
            "銀行轉帳",
            "行動支付",
            "信用卡"
        };
    }
}
