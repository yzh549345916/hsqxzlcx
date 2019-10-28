using System;

namespace TelerikMvcApp3.Models
{
    public class SKViewModel
    {
        public int 统计个数
        {
            get;
            set;
        }
        public int 分钟个数
        {
            get;
            set;
        }
        public int 小时个数
        {
            get;
            set;
        }
        public string 名称 { get; set; }
        public string 种类 { get; set; }

        public DateTime? 时间
        {
            get;
            set;
        }


    }
}
