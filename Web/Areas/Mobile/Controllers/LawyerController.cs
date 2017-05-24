using Common;
using EFModels.MyModels;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Mobile.Controllers
{
    public class LawyerController : Controller
    {
        //
        // GET: /Mobile/Lawyer/

        /// <summary>
        /// 根据接待室唯一标识获取律师案件
        /// </summary>
        /// <param name="iRoomOrder"></param>
        /// <returns></returns>
        public ActionResult Detail(int iRoomOrder=2)
        {
            LawyerCaseManage manage = new LawyerCaseManage();
            return View(manage.GetLayerCaseByiRoomOrder(iRoomOrder));
        }


        /// 获取律师的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info, string searchText)
        {
            LawyerManage manage = new LawyerManage();
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }


    }
}
