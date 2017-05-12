﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{
    public class Result
    {
        /// <summary>
        /// 需要返回的数据
        /// </summary>
        public object data = null;

        /// <summary>
        /// 信息描述
        /// </summary>
        public string info = "操作成功";

        /// <summary>
        /// 成功标识
        /// </summary>
        public bool success = false;

        /// <summary>
        /// 登录是否过时标识
        /// </summary>
        public bool over = false;


        public string toJson()
        {
            return C_Json.toJson(this);
        }
    }
}
