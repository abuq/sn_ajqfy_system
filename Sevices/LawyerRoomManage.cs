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
    /// 律师接待室管理相关业务
    /// </summary>
    public class LawyerRoomManage
    {
        /// <summary>
        /// 分页获取律师接待室相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = db.LawyerRoom.OrderByDescending(m=>m.ID);
                if(!string.IsNullOrEmpty(searchText))
                    query=query.Where(m=>m.sRoomName.Contains(searchText)).OrderByDescending(m => m.ID);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sRoomName": query = query.OrderBy(m => m.sRoomName); break;
                            case "iState": query = query.OrderBy(m => m.iState); break;
                            case "iRoomOrder": query = query.OrderBy(m => m.iRoomOrder); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sRoomName": query = query.OrderByDescending(m => m.sRoomName); break;
                            case "iState": query = query.OrderByDescending(m => m.iState); break;
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
        /// 检查律师接待室唯一标识
        /// </summary>
        /// <param name="iRoomOrder"></param>
        /// <returns></returns>
        public bool checkOrder(int iRoomOrder)
        {
            using (var db=new Entities())
            {
                return db.LawyerRoom.Any(m => m.iRoomOrder == iRoomOrder);
            }
        }


        /// <summary>
        /// 检查同名的律师接待室
        /// </summary>
        /// <param name="sLawyerName"></param>
        /// <returns></returns>
        public bool check(string sLawyerName)
        {
            using (var db = new Entities())
            {
                return db.LawyerRoom.Any(m => m.sRoomName == sLawyerName);
            }
        }

        /// <summary>
        /// 检查同名的律师接待室
        /// </summary>
        /// <param name="sLawyerName"></param>
        /// <returns></returns>
        public bool checkUpdate(string sLawyerName, int ID)
        {
            using (var db = new Entities())
            {
                return db.LawyerRoom.Any(m => m.sRoomName == sLawyerName && m.ID != ID);
            }
        }

        /// <summary>
        /// 根据ID获取律师接待室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LawyerRoom Get(int ID)
        {
            using (var db = new Entities())
            {
                var lawyerRoom = db.LawyerRoom.Find(ID);
                return lawyerRoom;
            }
        }

        /// <summary>
        /// 添加律师接待室
        /// </summary>
        /// <param name="lawyerRoom"></param>
        /// <returns></returns>
        public int Add(LawyerRoom lawyerRoom)
        {
            using (var db = new Entities())
            {
                db.LawyerRoom.Add(lawyerRoom);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑律师接待室
        /// </summary>
        /// <param name="lawyerRoom"></param>
        /// <returns></returns>
        public int Edit(LawyerRoom lawyerRoom)
        {
            using (var db = new Entities())
            {
                db.Entry<LawyerRoom>(lawyerRoom).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除律师接待室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var lawyerRoomList = db.LawyerRoom.Where(m => Ids.Contains(m.ID));
                foreach (var lawyerRoom in lawyerRoomList)
                {
                    db.Entry<LawyerRoom>(lawyerRoom).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
