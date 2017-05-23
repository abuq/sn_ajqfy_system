using Common;
using EFModels;
using EFModels.MyModels;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{

    //法官工作室控制器
    public class JudgeStudioController : AdminBase<JudgeStudioManage>
    {
        //
        // GET: /Admin/JudgeStudio/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int iJudgeStudioId)
        {
            var Tjcase = manage.Get(iJudgeStudioId);
            string sRealName = Resolve<UserManage>().Get(Tjcase.iUserId).RealName;
            ViewBag.sRealName = sRealName;
            return View(manage.Get(iJudgeStudioId));
        }

        /// 获取法官工作室的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info, string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加法官工作室
        /// </summary>
        /// <param name="juds"></param>
        public void Insert(JudgeStudio juds,string RealName)
        {
            var user = Resolve<UserManage>().Get(juds.iUserId);
            if (user.RealName == RealName)
            {
                if (manage.Add(juds) > 0)
                    result.success = true;
            }
            result.info = string.Format("{0}法官不存在,请重新选择!", RealName);
        }


        /// <summary>
        /// 编辑法官工作室
        /// </summary>
        /// <param name="juds"></param>
        public void Update(JudgeStudio juds, string RealName)
        {
            var user = Resolve<UserManage>().Get(juds.iUserId);
            if (user.RealName == RealName)
            {
                if (manage.Edit(juds) > 0)
                    result.success = true;
            }
            result.info = string.Format("{0}法官不存在,请重新选择!", RealName);
        }

        /// <summary>
        /// 删除法官工作室
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
    }
}
