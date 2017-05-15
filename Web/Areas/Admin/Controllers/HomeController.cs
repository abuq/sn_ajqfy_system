using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Web.App_Start;
using EFModels.MyModels;
using System.IO;
using Web.App_Start.BaseController;
using Sevices;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBase<UserManage>
    {
        //
        // GET: /Admin/Home/

        #region 后台首页相关视图
        public ActionResult Index()
        {
            return View(SessionAdminUser());
        }

        #endregion

 
        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
       // [NoLogin]
        //public void CheckLogin(string sUserName, string sPassWord,string sImgCode)
        //{
        //    if (sImgCode == Session[SESSION.ImgCode].ToString())
        //    {
        //        string sRoleName;
        //        var user = _server.Login(sUserName, sPassWord, out sRoleName);
        //        if (user != null)
        //        {
        //            if (user.bState)
        //            {
        //                Session[SESSION.User] = new UserInfo()
        //                {
        //                    ID = user.ID,
        //                    sUserName = user.sUserName,
        //                    sRoleId = user.sRoleID,
        //                    sRoleName = sRoleName,
        //                    Ip = Request.UserHostAddress
        //                };

        //                //缓存用户的二级菜单和按钮
        //                var menu = Resolve<MenusService>();
        //                var button = Resolve<ButtonService>();
        //                Session[SESSION.Menu] = menu.GetSecondMenus(user.sRoleID);
        //                Session[SESSION.Button] = button.GetButton(user.sRoleID);
        //                result.success = true;
        //            }
        //            else result.info = "该用户已被冻结,请联系管理员!";
        //        }
        //        else
        //            result.info = "用户名或密码错误!";
        //    }
        //    else
        //    {
        //        result.info = "验证码错误!";
        //    }
        //}
     

      
    }
}
