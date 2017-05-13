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

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int ID)
        {
            return View(manage.Get(ID));
        }

        /// <summary>
        ///  查看评价
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Appraise(int iUserId)
        {
            return View(iUserId);
        }

        /// <summary>
        /// 分页获取法官评价列表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult AppraiseList(PageInfo info,int iUserId)
        {
            return Content(C_Json.toJson(manage.AppraiseList(info, iUserId)));
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
            user.UserType = 1;
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
