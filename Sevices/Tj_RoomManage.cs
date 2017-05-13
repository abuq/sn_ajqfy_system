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
    /// 调解室相关业务
    /// </summary>
    public class Tj_RoomManage
    {
        /// <summary>
        /// 分页获取调解室相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = db.Tj_Room.OrderByDescending(m=>m.ID);
                if (query.Count() > 0)
                { 
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID":query = query.OrderBy(m => m.ID);break;
                            case "RoomName": query = query.OrderBy(m => m.RoomName); break;
                            case "RoomState": query = query.OrderBy(m => m.RoomState); break;
                            case "RoomOrder": query = query.OrderBy(m => m.RoomOrder); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "RoomName": query = query.OrderByDescending(m => m.RoomName); break;
                            case "RoomState": query = query.OrderByDescending(m => m.RoomState); break;
                            case "RoomOrder": query = query.OrderByDescending(m => m.RoomOrder); break;
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
        /// 根据ID获取调解室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Tj_Room Get(int ID)
        {
            using (var db = new Entities())
            {
                var sxr = db.Tj_Room.Find(ID);
                return sxr;
            }
        }

        /// <summary>
        /// 添加调解室
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int Add(Tj_Room room)
        {
            using (var db = new Entities())
            {
                db.Tj_Room.Add(room);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑调解室
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int Edit(Tj_Room room)
        {
            using (var db = new Entities())
            {
                db.Entry<Tj_Room>(room).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除调解室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var roomList = db.Tj_Room.Where(m => Ids.Contains(m.ID));
                foreach (var room in roomList)
                {
                    db.Entry<Tj_Room>(room).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
