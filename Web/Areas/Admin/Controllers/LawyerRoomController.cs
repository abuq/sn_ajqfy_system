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
    /// 律师接待室控制器
    /// </summary>
    public class LawyerRoomController : AdminBase<LawyerRoomManage>
    {
        //
        // GET: /Admin/LawyerRoom/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int iLawyerRoomId)
        {
            return View(manage.Get(iLawyerRoomId));
        }

        /// <summary>
        /// 获取律师接待室的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(C_Json.toJson(manage.List(info, searchText)));
        }

        /// <summary>
        /// 添加律师接待室
        /// </summary>
        /// <param name="law"></param>
        public void Insert(LawyerRoom lawyerRoom)
        {
            if (!manage.check(lawyerRoom.sRoomName))
            {
                if (!manage.checkOrder(lawyerRoom.iRoomOrder))
                {
                    if (manage.Add(lawyerRoom) > 0)
                        result.success = true;
                }
                else
                    result.info = string.Format("序号{0}律师接待室已存在,请重新设置", lawyerRoom.iRoomOrder);
            }
            else
            {
                result.info = string.Format("{0}律师接待室已存在,请重新命名", lawyerRoom.sRoomName);
            }
        }


        /// <summary>
        /// 编辑律师接待室
        /// </summary>
        /// <param name="law"></param>
        public void Update(LawyerRoom lawyerRoom)
        {
            if (!manage.checkUpdate(lawyerRoom.sRoomName, lawyerRoom.ID))
            {
                if (manage.Edit(lawyerRoom) > 0)
                    result.success = true;
            }
            else
            {
                result.info = string.Format("{0}律师接待室已存在,请重新命名", lawyerRoom.sRoomName);
            }
        }

        /// <summary>
        /// 删除律师接待室
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
