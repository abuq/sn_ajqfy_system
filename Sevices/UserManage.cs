using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels.MyModels;

namespace Sevices
{
    /// <summary>
    /// 法官相关服务
    /// </summary>
    public class UserManage
    {


        /// <summary>
        /// 分页获取法官数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info,string searchText=null)
        {
            using (var db=new Entities())
            {
                int total = 0;

                var query = db.User.OrderByDescending(m=>m.ID);
                if (query.Count() > 0)
                {
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
        /// 根据ID获取法官
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public User Get(int ID)
        {
            using (var db = new Entities())
            {
                var user = db.User.Find(ID);
                return user;
            }
        }

        /// <summary>
        /// 添加法官
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(User user)
        {
            using (var db=new Entities())
            {
                db.User.Add(user);
                return  db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑法官
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Edit(User user)
        {
            using (var db = new Entities())
            {
                db.User.Add(user);
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
            using (var db=new Entities())
            {

                var userList = db.User.Where(m => Ids.Contains(m.ID));
                foreach(var user in userList)
                {
                    db.Entry<User>(user).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
