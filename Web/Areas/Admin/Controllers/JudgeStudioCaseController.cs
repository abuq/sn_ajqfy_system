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
    public class JudgeStudioCaseController : AdminBase<JudgeStudioCaseManage>
    {
        //
        // GET: /Admin/JudgeStudioCase/

        /// <summary>
        /// 结束案件
        /// </summary>
        /// <returns></returns>
        public ActionResult Over(int iJudgeStudioCaseId)
        {
            var judsCase = manage.Get(iJudgeStudioCaseId);
            return View(judsCase);
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

        public ActionResult Edit(int iJudgeStudioCaseId)
        {
            var judsCase = manage.Get(iJudgeStudioCaseId);
            ViewBag.roomList = manage.GetAllRoom();
            return View(judsCase);
        }


        /// <summary>
        /// 获取调解案件的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加调解案件
        /// </summary>
        /// <param name="judsCase"></param>
        [ValidateInput(false)]
        public void Insert(JudgeStudioCase judsCase)
        {
            if (!manage.DateCommon(judsCase, true))
            {
                if (manage.Add(judsCase) > 0)
                    result.success = true;
            }
            else
                result.info = "该法官工作室在选择的时间上存在冲突,请重新选择时间或者法官工作室";
        }


        /// <summary>
        /// 编辑调解案件
        /// </summary>
        /// <param name="law"></param>
        [ValidateInput(false)]
        public void Update(JudgeStudioCase judsCase, string RealName)
        { 
            if (!manage.DateCommon(judsCase, false))
            {
                if (manage.Edit(judsCase) > 0)
                    result.success = true;
            }
            else
                result.info = "该法官工作室在选择的时间上存在冲突,请重新选择时间或者法官工作室";
        }

        /// <summary>
        /// 删除调解案件
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
        /// 结束案件
        /// </summary>
        /// <param name="dAclStaTime"></param>
        /// <param name="dAclEndTime"></param>
        /// <param name="ID"></param>
        public void UpdateOver(DateTime dAclStaTime, DateTime dAclEndTime,int ID)
        {
            if (manage.UpdateOver(dAclStaTime, dAclEndTime, ID) > 0)
                result.success = true;
        }

    }
}
