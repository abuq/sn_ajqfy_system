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
    public class TjRoomController : AdminBase<Tj_RoomManage>
    {
        //
        // GET: /Admin/TjRoom/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int iRoomId)
        {
            return View(manage.Get(iRoomId));
        }

        /// <summary>
        /// 获取调解室的数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info)
        {
            return Content(C_Json.toJson(manage.List(info)));
        }

        /// <summary>
        /// 添加调解室
        /// </summary>
        /// <param name="room"></param>
        public void Insert(Tj_Room room)
        {
            if (manage.Add(room) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑调解室
        /// </summary>
        /// <param name="room"></param>
        public void Update(Tj_Room room)
        {
            if (manage.Edit(room) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除调解室
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
