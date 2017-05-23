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
    /// 法官案件管理相关业务
    /// </summary>
    public class JudgeStudioCaseManage
    {
        /// <summary>
        /// 分页获取法官案件相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = from a in db.JudgeStudioCase
                            join b in db.JudgeStudio
                            on a.iJudgeStudioId equals b.ID
                            join c in db.User on b.iUserId equals c.ID
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
                                b.sJobName,
                                c.RealName
                            };
                if (!string.IsNullOrEmpty(searchText))
                    query = query.
                        Where(m => m.sJobName.Contains(searchText) || m.RealName.Contains(searchText));
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sCaseName": query = query.OrderBy(m => m.sCaseName); break;
                            case "dAclEndTime": query = query.OrderBy(m => m.dAclEndTime); break;
                            case "dAclStaTime": query = query.OrderBy(m => m.dAclStaTime); break;
                            case "dPreStaTime": query = query.OrderBy(m => m.dPreStaTime); break;
                            case "dPreEndTime": query = query.OrderBy(m => m.dPreEndTime); break;
                            case "sCaseIntroduce": query = query.OrderBy(m => m.sCaseIntroduce); break;
                            case "RealName": query = query.OrderBy(m => m.RealName); break;
                            case "sJobName": query = query.OrderBy(m => m.sJobName); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sCaseName": query = query.OrderByDescending(m => m.sCaseName); break;
                            case "dAclEndTime": query = query.OrderByDescending(m => m.dAclEndTime); break;
                            case "dAclStaTime": query = query.OrderByDescending(m => m.dAclStaTime); break;
                            case "dPreStaTime": query = query.OrderByDescending(m => m.dPreStaTime); break;
                            case "dPreEndTime": query = query.OrderByDescending(m => m.dPreEndTime); break;
                            case "sCaseIntroduce": query = query.OrderByDescending(m => m.sCaseIntroduce); break;
                            case "RealName": query = query.OrderByDescending(m => m.RealName); break;
                            case "sJobName": query = query.OrderByDescending(m => m.sJobName); break;
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
        /// 获取所有的法官室
        /// </summary>
        /// <returns></returns>
        public List<JudgeStudio> GetAllRoom()
        {
            using (var db=new Entities())
            {
                var query = db.JudgeStudio.Where(m=>m.iState==true).OrderByDescending(m => m.ID).ToList();
                return query;
            }
        }

        /// <summary>
        /// 根据ID获取法官案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JudgeStudioCase Get(int ID)
        {
            using (var db = new Entities())
            {
                var judsCase = db.JudgeStudioCase.Find(ID);
                return judsCase;
            }
        }

        /// <summary>
        /// 添加法官案件
        /// </summary>
        /// <param name="lawsCase"></param>
        /// <returns></returns>
        public int Add(JudgeStudioCase judsCase)
        {
            using (var db = new Entities())
            {
                var juds = db.JudgeStudio.Find(judsCase.iJudgeStudioId);
                judsCase.iRoomOrder = juds.iRoomOrder;
                db.JudgeStudioCase.Add(judsCase);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑法官案件
        /// </summary>
        /// <param name="judsCase"></param>
        /// <returns></returns>
        public int Edit(JudgeStudioCase judsCase)
        {
            using (var db = new Entities())
            {
                var juds = db.JudgeStudio.Find(judsCase.iJudgeStudioId);
                judsCase.iRoomOrder = juds.iRoomOrder;
                db.Entry<JudgeStudioCase>(judsCase).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除法官案件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var judsCaseList = db.JudgeStudioCase.Where(m => Ids.Contains(m.ID));
                foreach (var judsCase in judsCaseList)
                {
                    db.Entry<JudgeStudioCase>(judsCase).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 结束法官案件
        /// </summary>
        /// <param name="dAclStaTime"></param>
        /// <param name="dAclEndTime"></param>
        /// <param name="ID"></param>
        public int UpdateOver(DateTime dAclStaTime, DateTime dAclEndTime, int ID)
        {
            using (var db=new Entities())
            {
                var judsCase = db.JudgeStudioCase.Find(ID);
                judsCase.dAclStaTime = dAclStaTime;
                judsCase.dAclEndTime = dAclEndTime;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 判断法官案件选择的调解室是否时间存在冲突.
        /// </summary>
        /// <param name="iLawsId">法官ID</param>
        /// <param name="type">添加 true,编辑 false</param>
        /// <returns></returns>
        public bool DateCommon(JudgeStudioCase judsCase, bool type)
        {
            using (var db=new Entities())
            {
                var res = false;
                if (type)
                 res = db.JudgeStudioCase.
                    Any(m => m.iJudgeStudioId == judsCase.iJudgeStudioId && m.dAclEndTime==null &&(
                    (judsCase.dPreStaTime <= m.dPreStaTime && judsCase.dPreEndTime >= m.dPreStaTime)||    //第一种情况
                    (judsCase.dPreStaTime>=m.dPreStaTime&& judsCase.dPreEndTime<=m.dPreEndTime) ||         //第二种情况
                    (judsCase.dPreStaTime>=m.dPreStaTime&& judsCase.dPreEndTime>=m.dPreEndTime&& judsCase.dPreStaTime<m.dPreEndTime) ||         //第三种情况
                    (judsCase.dPreStaTime<=m.dPreStaTime&& judsCase.dPreEndTime>=m.dPreEndTime)));         //第四种情况
                else
                    res = db.JudgeStudioCase.
                    Any(m => m.iJudgeStudioId == judsCase.iJudgeStudioId && m.dAclEndTime ==null &&m.ID!= judsCase.ID && (
                   (judsCase.dPreStaTime <= m.dPreStaTime && judsCase.dPreEndTime >= m.dPreStaTime) ||    //第一种情况
                   (judsCase.dPreStaTime >= m.dPreStaTime && judsCase.dPreEndTime <= m.dPreEndTime) ||         //第二种情况
                   (judsCase.dPreStaTime >= m.dPreStaTime && judsCase.dPreEndTime >= m.dPreEndTime&& judsCase.dPreStaTime < m.dPreEndTime) ||         //第三种情况
                   (judsCase.dPreStaTime <= m.dPreStaTime && judsCase.dPreEndTime >= m.dPreEndTime)));         //第四种情况
                return res;
            }
        }


    }
}
