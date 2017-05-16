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
    public class LawsCaseController : AdminBase<LawsCaseManage>
    {
        //
        // GET: /Admin/LawsCase/

        /// <summary>
        /// 结束开庭
        /// </summary>
        /// <returns></returns>
        public ActionResult Over(int iLawsCaseId)
        {
            var lawsCase = manage.Get(iLawsCaseId);
            return View(lawsCase);
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

        public ActionResult Edit(int iLawsCaseId)
        {
            var lawsCase = manage.Get(iLawsCaseId);
            ViewBag.roomList = manage.GetAllRoom();
            string sRealName = Resolve<UserManage>().Get(lawsCase.iUserId.Value).RealName;
            ViewBag.sRealName = sRealName;
            return View(lawsCase);
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
        /// <param name="lawsCase"></param>
        [ValidateInput(false)]
        public void Insert(LawsCase lawsCase,string RealName)
        {
            var user = Resolve<UserManage>().Get(lawsCase.iUserId.Value);
            if (user.RealName == RealName)
            {
                if (!manage.DateCommon(lawsCase,true))
                {
                    if (manage.Add(lawsCase) > 0)
                        result.success = true;
                }
                else
                    result.info = "该法庭在选择的时间上存在冲突,请重新选择时间或者法庭";
            }
            else
            {
                result.info = string.Format("{0}法官不存在,请重新选择!", RealName);
            }
        }


        /// <summary>
        /// 编辑调解案件
        /// </summary>
        /// <param name="law"></param>
        [ValidateInput(false)]
        public void Update(LawsCase lawsCase,string RealName)
        {
            var user = Resolve<UserManage>().Get(lawsCase.iUserId.Value);
            if (user.RealName == RealName)
            {
                if (!manage.DateCommon(lawsCase,false))
                {
                    if (manage.Edit(lawsCase) > 0)
                        result.success = true;
                }
                else
                    result.info = "该法庭在选择的时间上存在冲突,请重新选择时间或者法庭";
            }
            else
            {
                result.info = string.Format("{0}法官不存在,请重新选择!", RealName);
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
        /// 结束开庭案件
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
