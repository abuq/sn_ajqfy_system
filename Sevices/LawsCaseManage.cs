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
    /// 法庭案件管理相关业务
    /// </summary>
    public class LawsCaseManage
    {
        /// <summary>
        /// 分页获取法庭案件相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = from a in db.LawsCase
                            join b in db.Laws
                            on a.iLawsId equals b.ID
                            join c in db.User on a.iUserId equals c.ID
                            orderby a.ID descending
                            select new
                            {
                                a.ID,
                                a.sCaseName,
                                a.sCaseNum,
                                a.sCaseType,
                                a.sCaseIntroduce,
                                a.dPreStaTime,
                                a.dPreEndTime,
                                a.dAclStaTime,
                                a.dAclEndTime,
                                b.sLawName,
                                c.RealName
                            };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.
                        Where(m => m.sCaseNum.Contains(searchText) || m.sLawName.Contains(searchText));
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sCaseNum": query = query.OrderBy(m => m.sCaseNum); break;
                            case "sLawName": query = query.OrderBy(m => m.sLawName); break;
                            case "sCaseName": query = query.OrderBy(m => m.sCaseName); break;
                            case "dAclEndTime": query = query.OrderBy(m => m.dAclEndTime); break;
                            case "dAclStaTime": query = query.OrderBy(m => m.dAclStaTime); break;
                            case "sCaseIntroduce": query = query.OrderBy(m => m.sCaseIntroduce); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sCaseNum": query = query.OrderByDescending(m => m.sCaseNum); break;
                            case "sLawName": query = query.OrderByDescending(m => m.sLawName); break;
                            case "sCaseName": query = query.OrderByDescending(m => m.sCaseName); break;
                            case "dAclEndTime": query = query.OrderByDescending(m => m.dAclEndTime); break;
                            case "dAclStaTime": query = query.OrderByDescending(m => m.dAclStaTime); break;
                            case "sCaseIntroduce": query = query.OrderByDescending(m => m.sCaseIntroduce); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
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
        /// 获取所有的法庭室
        /// </summary>
        /// <returns></returns>
        public List<Laws> GetAllRoom()
        {
            using (var db=new Entities())
            {
                var query = db.Laws.Where(m=>m.bLawState==false).OrderByDescending(m => m.ID).ToList();
                return query;
            }
        }

        /// <summary>
        /// 根据ID获取法庭案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LawsCase Get(int ID)
        {
            using (var db = new Entities())
            {
                var lawsCase = db.LawsCase.Find(ID);
                return lawsCase;
            }
        }

        /// <summary>
        /// 添加法庭案件
        /// </summary>
        /// <param name="lawsCase"></param>
        /// <returns></returns>
        public int Add(LawsCase lawsCase)
        {
            using (var db = new Entities())
            {
                var laws = db.Laws.Find(lawsCase.iLawsId);
                lawsCase.sCaseName = string.Empty;
                lawsCase.iLawOrder = laws.iLawOrder;
                db.LawsCase.Add(lawsCase);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑法庭案件
        /// </summary>
        /// <param name="lawsCase"></param>
        /// <returns></returns>
        public int Edit(LawsCase lawsCase)
        {
            using (var db = new Entities())
            {
                lawsCase.sCaseName = string.Empty;
                db.Entry<LawsCase>(lawsCase).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除法庭案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var lawsCaseList = db.LawsCase.Where(m => Ids.Contains(m.ID));
                foreach (var lawsCase in lawsCaseList)
                {
                    db.Entry<LawsCase>(lawsCase).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 结束开庭案件
        /// </summary>
        /// <param name="dAclStaTime"></param>
        /// <param name="dAclEndTime"></param>
        /// <param name="ID"></param>
        public int UpdateOver(DateTime dAclStaTime, DateTime dAclEndTime, int ID)
        {
            using (var db=new Entities())
            {
                var lawsCase = db.LawsCase.Find(ID);
                lawsCase.dAclStaTime = dAclStaTime;
                lawsCase.dAclEndTime = dAclEndTime;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 判断法庭案件选择的调解室是否时间存在冲突.
        /// </summary>
        /// <param name="iLawsId">法庭ID</param>
        /// <param name="type">添加 true,编辑 false</param>
        /// <returns></returns>
        public bool DateCommon(LawsCase lawsCase,bool type)
        {
            using (var db=new Entities())
            {
                var res = false;
                if (type)
                 res = db.LawsCase.
                    Any(m => m.iLawsId == lawsCase.iLawsId && m.dAclEndTime==null &&(
                    (lawsCase.dPreStaTime <= m.dPreStaTime && lawsCase.dPreEndTime >= m.dPreStaTime)||    //第一种情况
                    (lawsCase.dPreStaTime>=m.dPreStaTime&&lawsCase.dPreEndTime<=m.dPreEndTime) ||         //第二种情况
                    (lawsCase.dPreStaTime>=m.dPreStaTime&&lawsCase.dPreEndTime>=m.dPreEndTime&&lawsCase.dPreStaTime<m.dPreEndTime) ||         //第三种情况
                    (lawsCase.dPreStaTime<=m.dPreStaTime&&lawsCase.dPreEndTime>=m.dPreEndTime)));         //第四种情况
                else
                    res = db.LawsCase.
                    Any(m => m.iLawsId == lawsCase.iLawsId && m.dAclEndTime ==null &&m.ID!=lawsCase.ID && (
                   (lawsCase.dPreStaTime <= m.dPreStaTime && lawsCase.dPreEndTime >= m.dPreStaTime) ||    //第一种情况
                   (lawsCase.dPreStaTime >= m.dPreStaTime && lawsCase.dPreEndTime <= m.dPreEndTime) ||         //第二种情况
                   (lawsCase.dPreStaTime >= m.dPreStaTime && lawsCase.dPreEndTime >= m.dPreEndTime&& lawsCase.dPreStaTime < m.dPreEndTime) ||         //第三种情况
                   (lawsCase.dPreStaTime <= m.dPreStaTime && lawsCase.dPreEndTime >= m.dPreEndTime)));         //第四种情况
                return res;
            }
        }


    }
}
