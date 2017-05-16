using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels.MyModels;
using Common;

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
                var query = db.User.OrderBy(m =>m.DeptOrder).ThenBy(m=>m.UserOrder);
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.RealName.Contains(searchText)).OrderBy(m => m.DeptOrder).ThenBy(m => m.UserOrder);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {//排序
                            case "UserPic": query = query.OrderBy(m => m.UserPic); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                            case "Dept": query = query.OrderBy(m => m.Dept); break;
                            case "Job": query = query.OrderBy(m => m.Job); break;
                            case "Mobile": query = query.OrderBy(m => m.Mobile); break;
                            case "DeptOrder": query = query.OrderBy(m => m.DeptOrder); break;
                            case "UserOrder": query = query.OrderBy(m => m.UserOrder); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {//排序
                            case "UserPic": query = query.OrderByDescending(m => m.UserPic); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
                            case "Dept": query = query.OrderByDescending(m => m.Dept); break;
                            case "Job": query = query.OrderByDescending(m => m.Job); break;
                            case "Mobile": query = query.OrderByDescending(m => m.Mobile); break;
                            case "DeptOrder": query = query.OrderByDescending(m => m.DeptOrder); break;
                            case "UserOrder": query = query.OrderByDescending(m => m.UserOrder); break;
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
        /// 分页获取法官评价列表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public object AppraiseList(PageInfo info, int sUserId)
        {
            using (var db = new Entities())
            {
                int total = 0;
                int manyi=0,yiban=0,bumanyi=0;
                var query = db.UserComment.Where(m => m.UserID == sUserId).OrderByDescending(m => m.CommentDate);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "PJType": query = query.OrderBy(m => m.PJType); break;
                            case "Mobile": query = query.OrderBy(m => m.Mobile); break;
                            case "caseTypes": query = query.OrderBy(m => m.caseTypes); break;
                            case "CommentDate": query = query.OrderBy(m => m.CommentDate); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "PJType": query = query.OrderByDescending(m => m.PJType); break;
                            case "Mobile": query = query.OrderByDescending(m => m.Mobile); break;
                            case "caseTypes": query = query.OrderByDescending(m => m.caseTypes); break;
                            case "CommentDate": query = query.OrderByDescending(m => m.CommentDate); break;
                        }
                    }
                    manyi = query.Where(m => m.PJType == 1.ToString()).Count();
                    yiban = query.Where(m => m.PJType == 2.ToString()).Count();
                    bumanyi =query.Where(m => m.PJType == 3.ToString()).Count();
                    total = query.Count();
                    var row = query.Skip(info.rows * (info.page - 1)).Take(info.rows);
                    return new
                    {
                        manyi= manyi,
                        yiban = yiban,
                        bumanyi = bumanyi,
                        total = total,
                        rows = row.ToList(),
                        page = info.page
                    };
                }
                else
                {
                    return new
                    {
                        manyi = manyi,
                        yiban = yiban,
                        bumanyi = bumanyi,
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
                db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// 删除评价
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int CancelComment(List<int> Ids)
        {
            using (var db = new Entities())
            {

                var commentList = db.UserComment.Where(m => Ids.Contains(m.ID));
                foreach (var comment in commentList)
                {
                    db.Entry<UserComment>(comment).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 后台会员登录验证
        /// </summary>
        /// <param name="sAccount"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public Admin checkLogin(string sAccount, string sPassword)
        {
            using (var db=new Entities())
            {
                sPassword = C_Security.MD5(sPassword);
                var admin = db.Admin.Where(m => m.Password == sPassword && m.UserName == sAccount).FirstOrDefault();
                return admin;
            }
        }

    }
}
