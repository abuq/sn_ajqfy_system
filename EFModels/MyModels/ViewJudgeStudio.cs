using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{
    public class ViewJudgeStudio
    {
        public string Mobile { get; set; }//法官联系电话

        public string sJobName { get; set; }//法官工作室名字

        public bool IsBusy { get; set; }//是否空闲

        public string UserPic { get; set; }//法官头像

        public string RealName { get; set; }//法官姓名

        public string sCaseIntroduce { get; set; }//调解事项

        public string sCaseName { get; set; }//时间名称

        public DateTime dPreStaTime { get; set; }//预计开始时间

        public DateTime dPreEndTime { get; set; }//预计结束时间
    }
}
