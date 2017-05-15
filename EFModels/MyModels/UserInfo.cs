using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{

    /// <summary>
    /// 后台session保存的数据
    /// <summary>
    [Serializable]
    public  class UserInfo
    {
        public int ID
        {
            get;
            set;
        }

     
        public string sUserName
        {
            get;
            set;
        }
    }
}
