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
    public class UserController : AdminBase<UserManage>
    {
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取法官的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info)
        {
            return Content(C_Json.toJson(manage.List(info)));
        }

        /// <summary>
        /// 添加法官
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            if (manage.Add(user) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑法官
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            if (manage.Edit(user) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除法官
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
