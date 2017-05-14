using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    /// <summary>
    /// 调解案件管理相关业务
    /// </summary>
    public class TjCaseManage
    {
        /// <summary>
        /// 分页获取调解案件相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = from a in db.TjCase
                            join b in db.Tj_Room
                            on a.iTjRoomId equals b.ID
                            join c in db.User on
                            a.iUserId equals c.ID
                            orderby a.dInsertTime descending
                            select new
                            {
                                a.ID,
                                a.sCaseName,
                                a.dInsertTime,
                                b.RoomName,
                                c.RealName,
                                c.UserIntroduce
                            };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.
                        Where(m => m.sCaseName.Contains(searchText) || m.RoomName.Contains(searchText) || m.sCaseName.Contains(searchText));
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "RoomName": query = query.OrderBy(m => m.RoomName); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                            case "sCaseName": query = query.OrderBy(m => m.sCaseName); break;
                            case "dInsertTime": query = query.OrderBy(m => m.dInsertTime); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "RoomName": query = query.OrderByDescending(m => m.RoomName); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
                            case "sCaseName": query = query.OrderByDescending(m => m.sCaseName); break;
                            case "dInsertTime": query = query.OrderByDescending(m => m.dInsertTime); break;
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
        /// 获取所有的法官数据
        /// </summary>
        /// <returns></returns>
        public object GetAllUser(string searchText)
        {
            using (var db=new Entities())
            {
             
                  var  query = from a in db.User
                                orderby a.ID
                                select new
                                {
                                    a.ID,
                                    a.RealName,
                                    a.UserPic
                                };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.RealName.Contains(searchText));
                if (query.Count() > 0)
                {
                    return query.ToList();
                }
                else
                {
                    return new List<object>();
                }
            }
        }

        /// <summary>
        /// 获取所有的调解室
        /// </summary>
        /// <returns></returns>
        public List<Tj_Room> GetAllRoom()
        {
            using (var db=new Entities())
            {
                var query = db.Tj_Room.OrderByDescending(m => m.ID).ToList();
                return query;
            }
        }

        /// <summary>
        /// 根据ID获取调解案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TjCase Get(int ID)
        {
            using (var db = new Entities())
            {
                var tjcase = db.TjCase.Find(ID);
                return tjcase;
            }
        }

        /// <summary>
        /// 添加调解案件
        /// </summary>
        /// <param name="tjcase"></param>
        /// <returns></returns>
        public int Add(TjCase tjcase)
        {
            using (var db = new Entities())
            {
                tjcase.dInsertTime = DateTime.Now;
                db.TjCase.Add(tjcase);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑调解案件
        /// </summary>
        /// <param name="tjcase"></param>
        /// <returns></returns>
        public int Edit(TjCase tjcase)
        {
            using (var db = new Entities())
            {
                db.Entry<TjCase>(tjcase).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除调解案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var tjcaseList = db.TjCase.Where(m => Ids.Contains(m.ID));
                foreach (var tjcase in tjcaseList)
                {
                    db.Entry<TjCase>(tjcase).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
