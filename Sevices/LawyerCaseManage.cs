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
    ///律师案件管理相关业务
    /// </summary>
    public class LawyerCaseManage
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
                var query = from a in db.LawyerCase
                            join b in db.LawyerRoom
                            on a.iLawyerRoomId equals b.ID
                            join c in db.Lawyer on
                            a.iLawyerId equals c.ID
                            orderby a.ID descending
                            select new
                            {
                                a.ID,
                                a.sCaseName,
                                a.sCaseIntroduce,
                                a.dPreStaTime,
                                a.dPreEndTime,
                                a.dAclStaTime,
                                a.dAclEndTime,
                                b.sRoomName,
                                c.sLawyerName,
                            };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.
                        Where(m => m.sRoomName.Contains(searchText) || m.sLawyerName.Contains(searchText));
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sRoomName": query = query.OrderBy(m => m.sRoomName); break;
                            case "sLawyerName": query = query.OrderBy(m => m.sLawyerName); break;
                            case "sCaseName": query = query.OrderBy(m => m.sCaseName); break;
                            case "dAclStaTime": query = query.OrderBy(m => m.dAclStaTime); break;
                            case "dAclEndTime": query = query.OrderBy(m => m.dAclEndTime); break;
                            case "dPreStaTime": query = query.OrderBy(m => m.dPreStaTime); break;
                            case "dPreEndTime": query = query.OrderBy(m => m.dPreEndTime); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sRoomName": query = query.OrderByDescending(m => m.sRoomName); break;
                            case "sLawyerName": query = query.OrderByDescending(m => m.sLawyerName); break;
                            case "sCaseName": query = query.OrderByDescending(m => m.sCaseName); break;
                            case "dAclStaTime": query = query.OrderByDescending(m => m.dAclStaTime); break;
                            case "dAclEndTime": query = query.OrderByDescending(m => m.dAclEndTime); break;
                            case "dPreStaTime": query = query.OrderByDescending(m => m.dPreStaTime); break;
                            case "dPreEndTime": query = query.OrderByDescending(m => m.dPreEndTime); break;
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
        /// 获取所有的律师数据
        /// </summary>
        /// <returns></returns>
        public object GetAllUser(string searchText)
        {
            using (var db=new Entities())
            {
             
                  var  query = from a in db.Lawyer
                                orderby a.ID
                                select new
                                {
                                    a.ID,
                                    a.sLawyerName,
                                    a.sPicture
                                };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.sLawyerName.Contains(searchText));
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
        /// 获取所有的接待室
        /// </summary>
        /// <returns></returns>
        public List<LawyerRoom> GetAllRoom()
        {
            using (var db=new Entities())
            {
                var query = db.LawyerRoom.Where(m=>m.iState==true).OrderByDescending(m => m.ID).ToList();
                return query;
            }
        }

        /// <summary>
        /// 根据ID获取律师案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LawyerCase Get(int ID)
        {
            using (var db = new Entities())
            {
                var lawyercase = db.LawyerCase.Find(ID);
                return lawyercase;
            }
        }

        /// <summary>
        /// 添加律师案件
        /// </summary>
        /// <param name="lawyercase"></param>
        /// <returns></returns>
        public int Add(LawyerCase lawyercase)
        {
            using (var db = new Entities())
            {
                var lawyerroom = db.LawyerRoom.Find(lawyercase.iLawyerRoomId);
                lawyercase.iRoomOrder = lawyerroom.iRoomOrder;
                db.LawyerCase.Add(lawyercase);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑律师案件
        /// </summary>
        /// <param name="lawyercase"></param>
        /// <returns></returns>
        public int Edit(LawyerCase lawyercase)
        {
            using (var db = new Entities())
            {
                var lawyerroom = db.LawyerRoom.Find(lawyercase.iLawyerRoomId);
                lawyercase.iRoomOrder = lawyerroom.iRoomOrder;
                db.Entry<LawyerCase>(lawyercase).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除律师案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var lawyercaseList = db.LawyerCase.Where(m => Ids.Contains(m.ID));
                foreach (var lawyercase in lawyercaseList)
                {
                    db.Entry<LawyerCase>(lawyercase).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 结束律师案件
        /// </summary>
        /// <param name="dAclStaTime"></param>
        /// <param name="dAclEndTime"></param>
        /// <param name="ID"></param>
        public int UpdateOver(DateTime dAclStaTime, DateTime dAclEndTime, int ID)
        {
            using (var db = new Entities())
            {
                var lawyercase = db.LawyerCase.Find(ID);
                lawyercase.dAclStaTime = dAclStaTime;
                lawyercase.dAclEndTime = dAclEndTime;
                return db.SaveChanges();
            }
        }



        /// <summary>
        /// 判断调节案件选择的接待室是否时间存在冲突.
        /// </summary>
        /// <param name="iLawsId">法庭ID</param>
        /// <param name="type">添加 true,编辑 false</param>
        /// <returns></returns>
        public bool DateCommon(LawyerCase lawyercase, bool type)
        {
            using (var db = new Entities())
            {
                var res = false;
                if (type)
                    res = db.LawyerCase.
                       Any(m => m.iLawyerRoomId == lawyercase.iLawyerRoomId && m.dAclEndTime == null && (
                       (lawyercase.dPreStaTime <= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreStaTime) ||    //第一种情况
                       (lawyercase.dPreStaTime >= m.dPreStaTime && lawyercase.dPreEndTime <= m.dPreEndTime) ||         //第二种情况
                       (lawyercase.dPreStaTime >= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreEndTime && lawyercase.dPreStaTime<m.dPreEndTime) ||         //第三种情况
                       (lawyercase.dPreStaTime <= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreEndTime)));         //第四种情况
                else
                    res = db.LawyerCase.
                    Any(m => m.iLawyerRoomId == lawyercase.iLawyerRoomId && m.dAclEndTime== null && m.ID != lawyercase.ID && (
                   (lawyercase.dPreStaTime <= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreStaTime) ||    //第一种情况
                   (lawyercase.dPreStaTime >= m.dPreStaTime && lawyercase.dPreEndTime <= m.dPreEndTime) ||         //第二种情况
                   (lawyercase.dPreStaTime >= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreEndTime&& lawyercase.dPreStaTime < m.dPreEndTime) ||         //第三种情况
                   (lawyercase.dPreStaTime <= m.dPreStaTime && lawyercase.dPreEndTime >= m.dPreEndTime)));         //第四种情况
                return res;
            }
        }


        /// <summary>
        /// 根据律师接待室的ID获取律师案件
        /// </summary>
        /// <param name="iRoomOrder"></param>
        /// <returns></returns>
        public ViewLawyerCase GetLayerCaseByiRoomOrder(int iRoomOrder)
        {
            using (var db=new Entities())
            {
                var result = new ViewLawyerCase();
                var query = (from a in db.LawyerCase
                             join b in db.LawyerRoom
                             on a.iLawyerRoomId equals b.ID
                             join c in db.Lawyer
                             on a.iLawyerId equals c.ID
                             where a.iRoomOrder == iRoomOrder && a.dAclStaTime == null && a.dPreStaTime <= DateTime.Now && a.dPreEndTime >= DateTime.Now
                             select new ViewLawyerCase()
                             {
                                 IsBusy = true,
                                 sCaseIntroduce = a.sCaseIntroduce,
                                 sRoomName = b.sRoomName,
                                 sCaseName = a.sCaseName,
                                 sLawyerName = c.sLawyerName,
                                 sPicture = c.sPicture,
                                 dPreStaTime = a.dPreStaTime.Value,
                                 dPreEndTime = a.dPreEndTime.Value
                             }).SingleOrDefault();
                if (query == null)
                {
                    result.IsBusy = false;
                    return result;
                }
                else
                {
                    return query;
                }
            }

        }
    }
}
