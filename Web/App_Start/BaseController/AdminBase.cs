﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using EFModels;
using EFModels.MyModels;
using Common;

namespace Web.App_Start.BaseController
{
    public class AdminBase<T>: Controller where T:class,new ()
    {

        //返回结果集
        protected Result result = new Result();
       
        //接口
        protected T manage;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public AdminBase()
        {
            manage = Resolve<T>();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected M Resolve<M>() where M:class,new()
        {
            return new M();
        }

        /// <summary>
        /// 获取后台AdminUserSession
        /// </summary>
        /// <returns></returns>
        protected UserInfo SessionAdminUser()
        {
            return Session[SESSION.AdminUser] as UserInfo;
        }

        /// <summary>
        /// 在Action之前调用
        /// tip:主要来验证用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1))
            {//有NoLogin属性;不判断登录
                if (SessionAdminUser() == null)
                {
                    /*登录过时,session过期*/
                    if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                    {
                        /*跳转到登录过期提示页面*/
                        filterContext.Result = new RedirectResult("/Admin/User/Login");
                    }
                    else
                    {
                        result.over = true;//登录过时
                        ContentResult res = new ContentResult();
                        res.Content = result.toJson();
                        filterContext.Result = res;
                    }
                }
                else
                {//判断会员状态是否正常

                }
            }
        }

        /// <summary>
        /// 在Action之后调用
        /// 主要处理返回的数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var actionMethod = filterContext.Controller
              .GetType()
              .GetMethod(filterContext.ActionDescriptor.ActionName);//获取访问方法   
            if (actionMethod.ReturnType.Name.ToString() == "Void" && request.IsAjaxRequest() && request.HttpMethod.ToUpper() == "POST")
            {//POST的返回结果处理
                filterContext.Result = Content(result.toJson()); /**统一处理ajax的返回结果**/
            }
            filterContext.Controller.ViewData["Mulu"] = C_Config.ReadAppSetting("XUNIMULU");
        }

        /// <summary>
        /// 捕捉异常
        /// 统一处理错误日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Logs.LogHelper.ErrorLog(filterContext.Exception);
        }
    }
}