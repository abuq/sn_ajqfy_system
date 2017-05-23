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

    //律师控制器
    public class LawyerController : AdminBase<LawyerManage>
    {
        //
        // GET: /Admin/Lawyer/

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
        public ActionResult Edit(int iLawyerId)
        {
            return View(manage.Get(iLawyerId));
        }

        /// 获取律师的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info, string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加律师
        /// </summary>
        /// <param name="lawyer"></param>
        public void Insert(Lawyer lawyer)
        {
            if (manage.Add(lawyer) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑律师
        /// </summary>
        /// <param name="lawyer"></param>
        public void Update(Lawyer lawyer)
        {
            if (manage.Edit(lawyer) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除律师
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
