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
    /// 法庭管理相关业务
    /// </summary>
    public class LawsManage
    {
        /// <summary>
        /// 分页获取法庭相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = db.Laws.OrderByDescending(m=>m.ID);
                if(!string.IsNullOrEmpty(searchText))
                    query=query.Where(m=>m.sLawName.Contains(searchText)).OrderByDescending(m => m.ID);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sLawName": query = query.OrderBy(m => m.sLawName); break;
                            case "bLawState": query = query.OrderBy(m => m.bLawState); break;
                            case "iLawOrder": query = query.OrderBy(m => m.iLawOrder); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sLawName": query = query.OrderByDescending(m => m.sLawName); break;
                            case "bLawState": query = query.OrderByDescending(m => m.bLawState); break;
                            case "iLawOrder": query = query.OrderByDescending(m => m.iLawOrder); break;
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
        /// 检查同名的法庭
        /// </summary>
        /// <param name="RoomName"></param>
        /// <returns></returns>
        public bool check(string sLawName)
        {
            using (var db = new Entities())
            {
                return db.Laws.Any(m => m.sLawName == sLawName);
            }
        }

        /// <summary>
        /// 检查同名的法庭
        /// </summary>
        /// <param name="RoomName"></param>
        /// <returns></returns>
        public bool checkUpdate(string sLawName,int ID)
        {
            using (var db = new Entities())
            {
                return db.Laws.Any(m => m.sLawName == sLawName && m.ID != ID);
            }
        }

        /// <summary>
        /// 根据ID获取法庭
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Laws Get(int ID)
        {
            using (var db = new Entities())
            {
                var law = db.Laws.Find(ID);
                return law;
            }
        }

        /// <summary>
        /// 添加法庭
        /// </summary>
        /// <param name="law"></param>
        /// <returns></returns>
        public int Add(Laws law)
        {
            using (var db = new Entities())
            {
                if (law.sLawPicture == null)
                    law.sLawPicture = string.Empty;
                db.Laws.Add(law);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑法庭
        /// </summary>
        /// <param name="law"></param>
        /// <returns></returns>
        public int Edit(Laws law)
        {
            using (var db = new Entities())
            {
                if (law.sLawPicture == null)
                    law.sLawPicture = string.Empty;
                db.Entry<Laws>(law).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除法庭
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var lawList = db.Laws.Where(m => Ids.Contains(m.ID));
                foreach (var law in lawList)
                {
                    db.Entry<Laws>(law).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
