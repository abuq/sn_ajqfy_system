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
    public class LawsController : AdminBase<LawsManage>
    {
        //
        // GET: /Admin/Laws/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int iLawId)
        {
            return View(manage.Get(iLawId));
        }

        /// <summary>
        /// 获取法庭的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加法庭
        /// </summary>
        /// <param name="law"></param>
        public void Insert(Laws law)
        {
            if (!manage.check(law.sLawName))
            {
                if (manage.Add(law) > 0)
                    result.success = true;
            }
            else
            {
                result.info = string.Format("{0}法庭已存在,请重新命名", law.sLawName);
            }
        }


        /// <summary>
        /// 编辑法庭
        /// </summary>
        /// <param name="law"></param>
        public void Update(Laws law)
        {
            if (!manage.checkUpdate(law.sLawName,law.ID))
            {
                if (manage.Edit(law) > 0)
                    result.success = true;
            }
            else
            {
                result.info = string.Format("{0}法庭已存在,请重新命名", law.sLawName);
            }
        }

        /// <summary>
        /// 删除法庭
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
