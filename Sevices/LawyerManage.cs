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
    /// 律师相关业务
    /// </summary>
    public class LawyerManage
    {
        /// <summary>
        /// 分页获取律师相关数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public object List(PageInfo info, string searchText = null)
        {
            using (var db = new Entities())
            {
                int total = 0;
                var query = db.Lawyer.OrderByDescending(m=>m.ID);
                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(m => m.sLawyerName.Contains(searchText)||m.sPhone.Contains(searchText)).OrderByDescending(m => m.ID);
                if (query.Count() > 0)
                {
                    if (info.order == OrderType.ASC)
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderBy(m => m.ID); break;
                            case "sLawyerName": query = query.OrderBy(m => m.sLawyerName); break;
                            case "sPhone": query = query.OrderBy(m => m.sPhone); break;
                            case "sPicture": query = query.OrderBy(m => m.sPicture); break;
                            case "iAges": query = query.OrderBy(m => m.iAges); break;
                            case "sJobAddress": query = query.OrderBy(m => m.sJobAddress); break;
                            case "sExecutionNum": query = query.OrderBy(m => m.sExecutionNum); break;
                            case "bSex": query = query.OrderBy(m => m.bSex); break;
                        }
                    }
                    else
                    {
                        switch (info.sort)
                        {
                            case "ID": query = query.OrderByDescending(m => m.ID); break;
                            case "sLawyerName": query = query.OrderByDescending(m => m.sLawyerName); break;
                            case "sPhone": query = query.OrderByDescending(m => m.sPhone); break;
                            case "sPicture": query = query.OrderByDescending(m => m.sPicture); break;
                            case "iAges": query = query.OrderByDescending(m => m.iAges); break;
                            case "sJobAddress": query = query.OrderByDescending(m => m.sJobAddress); break;
                            case "sExecutionNum": query = query.OrderByDescending(m => m.sExecutionNum); break;
                            case "bSex": query = query.OrderByDescending(m => m.bSex); break;
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
        /// 根据ID获取律师
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Lawyer Get(int ID)
        {
            using (var db = new Entities())
            {
                var law = db.Lawyer.Find(ID);
                return law;
            }
        }

        /// <summary>
        /// 添加律师
        /// </summary>
        /// <param name="lawyer"></param>
        /// <returns></returns>
        public int Add(Lawyer lawyer)
        {
            using (var db = new Entities())
            { 
                db.Lawyer.Add(lawyer);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑律师
        /// </summary>
        /// <param name="lawyer"></param>
        /// <returns></returns>
        public int Edit(Lawyer lawyer)
        {
            using (var db = new Entities())
            {
                db.Entry<Lawyer>(lawyer).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 删除律师
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Cancel(List<int> Ids)
        {
            using (var db = new Entities())
            {
                var lawyerList = db.Lawyer.Where(m => Ids.Contains(m.ID));
                foreach (var lawyer in lawyerList)
                {
                    db.Entry<Lawyer>(lawyer).State = System.Data.Entity.EntityState.Deleted;
                }
                return db.SaveChanges();
            }
        }
    }
}
