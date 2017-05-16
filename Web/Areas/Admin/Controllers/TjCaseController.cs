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
    public class TjCaseController : AdminBase<TjCaseManage>
    {
        //
        // GET: /Admin/TjCase/

        /// <summary>
        /// 结束调解
        /// </summary>
        /// <param name="iTjCaseId"></param>
        /// <returns></returns>
        public ActionResult Over(int iTjCaseId)
        {
            var Tjcase = manage.Get(iTjCaseId);
            return View(Tjcase);
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

        public ActionResult Edit(int iTjCaseId)
        {
            ViewBag.roomList = manage.GetAllRoom();
            var Tjcase= manage.Get(iTjCaseId);
            string sRealName = Resolve<UserManage>().Get(Tjcase.iUserId).RealName;
            ViewBag.sRealName = sRealName;
            return View(Tjcase);
        }

        /// <summary>
        /// 获取法官数据
        /// </summary>
        /// <returns></returns>
        public void UserList(string searchText)
        {
            result.data = manage.GetAllUser(searchText);
            result.success = true;
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
        /// <param name="tjcase"></param>
        [ValidateInput(false)]
        public void Insert(TjCase tjcase,string RealName)
        {
            var user = Resolve<UserManage>().Get(tjcase.iUserId);
            if (user.RealName == RealName)
            {
                if (!manage.DateCommon(tjcase, true))
                {
                    if (manage.Add(tjcase) > 0)
                        result.success = true;
                }
                else result.info = "该调解室在选择的时间上存在冲突,请重新选择时间或者法庭";
            }
            else
            {
                result.info =string.Format("{0}调解员不存在,请重新选择!", RealName);
            }
        }


        /// <summary>
        /// 编辑调解案件
        /// </summary>
        /// <param name="law"></param>
        [ValidateInput(false)]
        public void Update(TjCase tjcase, string RealName)
        {
            var user = Resolve<UserManage>().Get(tjcase.iUserId);
            if (user.RealName == RealName)
            {
                if (!manage.DateCommon(tjcase, false))
                {
                    if (manage.Edit(tjcase) > 0)
                        result.success = true;
                }
                else
                    result.info = "该调解室在选择的时间上存在冲突,请重新选择时间或者法庭";

            }
            else
            {
                result.info = string.Format("{0}调解员不存在,请重新选择!", RealName);
            }
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
        /// 结束调解案件
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
