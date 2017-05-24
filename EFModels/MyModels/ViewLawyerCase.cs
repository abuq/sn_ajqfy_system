using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{
    public class ViewLawyerCase
    {
        public bool IsBusy { get; set; }//是否接待

        public string sLawyerName { get; set; }//法官名字

        public string sRoomName { get; set; }//接待室名称

        public string sPicture { get; set; }//律师图片

        public string sCaseIntroduce { get; set; }//接访事项

        public string sCaseName { get; set; }//事件名称

        public DateTime dPreStaTime { get; set; }//预计开始时间

        public DateTime dPreEndTime { get; set; }//预计结束时间


    }
}
