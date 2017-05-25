using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Mobile.Controllers
{
    public class JudgeStudioController : Controller
    {
        //
        // GET: /Mobile/JudgeStudio/
    
        /// <summary>
        /// 根据法官工作室序号获取详情
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Detail(int iRoomOrder)
        {
            JudgeStudioCaseManage manage = new JudgeStudioCaseManage();
            return View(manage.GetCaseByiRoomOrder(iRoomOrder));
        }
    }
}
