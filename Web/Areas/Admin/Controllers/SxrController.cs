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
    public class SxrController : AdminBase<SxrManage>
    {
        //
        // GET: /Admin/Sxr/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int iSxrId)
        {
            return View(manage.Get(iSxrId));
        }

        /// <summary>
        /// 获取失信人的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info)
        {
            return Content(C_Json.toJson(manage.List(info)));
        }

        /// <summary>
        /// 添加失信人
        /// </summary>
        /// <param name="sxr"></param>
        public void Insert(Sxr sxr)
        {
            if (manage.Add(sxr) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑失信人
        /// </summary>
        /// <param name="sxr"></param>
        public void Update(Sxr sxr)
        {
            if (manage.Edit(sxr) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除失信人
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
