namespace TelerikMvcApp3.Models
{
    public class 资料种类
    {
        private string name;
        private string sjkName;
        private int xh;

        public 资料种类(int myXh, string myName, string mySjkName)
        {
            this.name = myName;
            this.sjkName = mySjkName;
            this.xh = myXh;
        }

        public string 表名称
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string 数据库表名称
        {
            get
            {
                return this.sjkName;
            }
            set
            {
                this.sjkName = value;
            }
        }

        public int 序号
        {
            get
            {
                return this.xh;
            }
            set
            {
                this.xh = value;
            }
        }


    }
}
