﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Common;

namespace Web.HandleProgram
{
    /// <summary>
    /// 图片上传的一般处理程序
    /// </summary>
    public class PictureUpload : IHttpHandler
    {

        private int _iMaxSize = 2*1024;//文件最大不超过2M

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                HttpFileCollection ImgList = context.Request.Files;
                for (int i=0;i< ImgList.Count;i++)
                {
                    HttpPostedFile image = ImgList[i];
                    if (image != null && image.ContentLength > 0)
                    {
                        string[] sExtension = { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                        string dDate = DateTime.Now.ToString("yyyy-MM");

                        /*图片保存路径*/
                        string sPath = System.AppDomain.CurrentDomain.BaseDirectory + "Images\\" + dDate + "\\";
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }

                        string format = System.IO.Path.GetExtension(image.FileName);//后缀名
                        /*组装文件名*/
                        string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + format;

                        /*文件大小超出限制*/
                        if (image.ContentLength > (_iMaxSize * 1024))
                        {
                            context.Response.Write(C_Json.toJson(new result()
                            {
                                error = 1,
                                message = "文件超出大小限制2M!"
                            }));
                            return;
                        }
                        /*上传的文件后缀名格式错误*/
                        if (!sExtension.Contains(format))
                        {
                            context.Response.Write(C_Json.toJson(new result()
                            {
                                error = 1,
                                message = "上传的图片文件格式错误!"
                            }));
                            return;
                        }
                        /*保存图片到本地*/
                        image.SaveAs(sPath + sFileName);
                        context.Response.Write(C_Json.toJson(new result()
                        {
                            error = 0,
                            url = "/Images/" + dDate + "/" + sFileName
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                Logs.LogHelper.ErrorLog(e);
                context.Response.Write(C_Json.toJson(new result()
                {
                    error = 1,
                    message = "上传失败!"
                }));
            }
  
        }

        /// <summary>
        /// 返回上传到结果
        /// </summary>
        public class result
        {
            /// <summary>
            /// 返回的图片链接
            /// </summary>
            public string url
            {
                get; set;
            }
            /// <summary>
            /// 执行成功的标识 0-正确 1-错误
            /// </summary>
            public int error
            {
                get; set;
            }
            /// <summary>
            /// 错误的信息表述
            /// </summary>
            public string message
            {
                get; set;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}