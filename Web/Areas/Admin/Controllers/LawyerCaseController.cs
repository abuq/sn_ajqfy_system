using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;
using Web.App_Start.BaseController;
using Sevices;
using EFModels;
using Common;

namespace Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 律师案件控制器
    /// </summary>
    public class LawyerCaseController : AdminBase<LawyerCaseManage>
    {
        //
        // GET: /Admin/TjCase/

        /// <summary>
        /// 结束案件
        /// <param name="iTjCaseId"></param>
        /// <returns></returns>
        public ActionResult Over(int iLawyerCaseId)
        {
            var lawyerCase = manage.Get(iLawyerCaseId);
            return View(lawyerCase);
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.roomList = manage.GetAllRoom();
            return View();
        }

        public ActionResult Edit(int iLawyerCaseId)
        {
            ViewBag.roomList = manage.GetAllRoom();
            var lawyerCase = manage.Get(iLawyerCaseId);
            string sRealName = Resolve<LawyerManage>().Get(lawyerCase.iLawyerId).sLawyerName;
            ViewBag.sRealName = sRealName;
            return View(lawyerCase);
        }

        /// <summary>
        /// 获取所有律师数据
        /// </summary>
        /// <returns></returns>
        public void UserList(string searchText)
        {
            result.data = manage.GetAllUser(searchText);
            result.success = true;
        }


        /// <summary>
        /// 获取律师案件的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加律师案件
        /// </summary>
        /// <param name="lawyerCase"></param>
        [ValidateInput(false)]
        public void Insert(LawyerCase lawyerCase, string RealName)
        {
            var lawyer = Resolve<LawyerManage>().Get(lawyerCase.iLawyerId);
            if (lawyer.sLawyerName == RealName)
            {
                if (!manage.DateCommon(lawyerCase, true))
                {
                    if (manage.Add(lawyerCase) > 0)
                        result.success = true;
                }
                else result.info = "该接待室在选择的时间上存在冲突,请重新选择时间或者接待室";
            }
            else
            {
                result.info =string.Format("{0}律师不存在,请重新选择!", RealName);
            }
        }


        /// <summary>
        /// 编辑律师案件
        /// </summary>
        /// <param name="lawyerCase"></param>
        [ValidateInput(false)]
        public void Update(LawyerCase lawyerCase, string RealName)
        {
            var lawyer = Resolve<LawyerManage>().Get(lawyerCase.iLawyerId);
            if (lawyer.sLawyerName == RealName)
            {
                if (!manage.DateCommon(lawyerCase, false))
                {
                    if (manage.Edit(lawyerCase) > 0)
                        result.success = true;
                }
                else
                    result.info = "该接待室在选择的时间上存在冲突,请重新选择时间或者接待室";

            }
            else
            {
                result.info = string.Format("{0}律师不存在,请重新选择!", RealName);
            }
        }

        /// <summary>
        /// 删除律师案件
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            List<int> Ids_int = Ids.Split(',').ToList().Select(m => Convert.ToInt32(m)).ToList();
            if (manage.Cancel(Ids_int) > 0)
            {
                result.success = true;
            }
        }


        /// <summary>
        /// 结束律师案件
        /// </summary>
        /// <param name="dAclStaTime"></param>
        /// <param name="dAclEndTime"></param>
        /// <param name="ID"></param>
        public void UpdateOver(DateTime dAclStaTime, DateTime dAclEndTime, int ID)
        {
            if (manage.UpdateOver(dAclStaTime, dAclEndTime, ID) > 0)
                result.success = true;
        }
    }
}
