﻿using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    /// <summary>
    /// 失信人相关业务
    /// </summary>
    public class SxrManage
    {
        /// <summary>
        /// 分页获取失信人相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = db.Sxr.OrderByDescending(m => m.ID);
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.RealName.Contains(searchText) || m.IDCard.Contains(searchText)).OrderByDescending(m => m.ID);       
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {//排序
                            case "UserPic": query = query.OrderBy(m => m.UserPic); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                            case "IDCard": query = query.OrderBy(m => m.IDCard); break;
                            case "CaseNum": query = query.OrderBy(m => m.CaseNum); break;
                            case "CaseBiaodi": query = query.OrderBy(m => m.CaseBiaodi); break;
                            case "Adds": query = query.OrderBy(m => m.Adds); break;
                            case "UserOrder": query = query.OrderBy(m => m.UserOrder); break;
                            case "iExecuteState": query = query.OrderBy(m => m.iExecuteState); break;
                            case "iAges": query = query.OrderBy(m => m.iAges); break;
                            case "bSex": query = query.OrderBy(m => m.bSex); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {//排序
                            case "UserPic": query = query.OrderByDescending(m => m.UserPic); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
                            case "IDCard": query = query.OrderByDescending(m => m.IDCard); break;
                            case "CaseNum": query = query.OrderByDescending(m => m.CaseNum); break;
                            case "CaseBiaodi": query = query.OrderByDescending(m => m.CaseBiaodi); break;
                            case "Adds": query = query.OrderByDescending(m => m.Adds); break;
                            case "UserOrder": query = query.OrderByDescending(m => m.UserOrder); break;
                            case "iExecuteState": query = query.OrderByDescending(m => m.iExecuteState); break;
                            case "iAges": query = query.OrderByDescending(m => m.iAges); break;
                            case "bSex": query = query.OrderByDescending(m => m.bSex); break;
                        }
                    }
                    total = query.Count();
                    var row = query.Skip(info.rows * (info.page - 1)).Take(info.rows);
                    return new
                    {
                        total = total,
                        rows = row.ToList(),
                        page = info.page
                    };
                }
                else
                {
                    return new
                    {
                        total = total,
                        rows = new List<object>(),
                        page = info.page
                    };
                }

            }
        }

        /// <summary>
        /// 根据ID获取失信人
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Sxr Get(int ID)
        {
            using (var db = new Entities())
            {
                var sxr = db.Sxr.Find(ID);
                return sxr;
            }
        }

        /// <summary>
        /// 添加失信人
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(Sxr sxr)
        {
            using (var db = new Entities())
            {
                sxr.UserOrder = 0;
                sxr.UserState = true;
                db.Sxr.Add(sxr);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑失信人
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Edit(Sxr sxr)
        {
            using (var db = new Entities())
            {
                db.Entry<Sxr>(sxr).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {

                var sxrList = db.Sxr.Where(m => Ids.Contains(m.ID));
                foreach (var sxr in sxrList)
                {
                    db.Entry<Sxr>(sxr).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
