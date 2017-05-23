using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    /// <summary>
    /// 法官工作室相关业务
    /// </summary>
    public class JudgeStudioManage
    {
        /// <summary>
        /// 分页获取法官工作室相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = from a in db.JudgeStudio   //db.JudgeStudio.OrderByDescending(m=>m.ID);
                            join b in db.User
                            on a.iUserId equals b.ID
                            orderby a.ID descending
                            select new
                            {
                                a.ID,
                                a.iRoomOrder,
                                a.iState,
                                a.iUserId,
                                a.sJobName,
                                b.RealName
                            };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.sJobName.Contains(searchText) || m.RealName.Contains(searchText)).OrderByDescending(m => m.ID);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sJobName": query = query.OrderBy(m => m.sJobName); break;
                            case "iState": query = query.OrderBy(m => m.iState); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                            case "iRoomOrder": query = query.OrderBy(m => m.iRoomOrder); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sJobName": query = query.OrderByDescending(m => m.sJobName); break;
                            case "iState": query = query.OrderByDescending(m => m.iState); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
                            case "iRoomOrder": query = query.OrderByDescending(m => m.iRoomOrder); break;
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
        /// 根据ID获取法官工作室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JudgeStudio Get(int ID)
        {
            using (var db = new Entities())
            {
                var juds = db.JudgeStudio.Find(ID);
                return juds;
            }
        }

        /// <summary>
        /// 添加法官工作室
        /// </summary>
        /// <param name="lawyer"></param>
        /// <returns></returns>
        public int Add(JudgeStudio juds)
        {
            using (var db = new Entities())
            { 
                db.JudgeStudio.Add(juds);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑法官工作室
        /// </summary>
        /// <param name="juds"></param>
        /// <returns></returns>
        public int Edit(JudgeStudio juds)
        {
            using (var db = new Entities())
            {
                db.Entry<JudgeStudio>(juds).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除法官工作室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var judsList = db.JudgeStudio.Where(m => Ids.Contains(m.ID));
                foreach (var juds in judsList)
                {
                    db.Entry<JudgeStudio>(juds).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
