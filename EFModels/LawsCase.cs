//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class LawsCase
    {
        public int ID { get; set; }
        public int iLawsId { get; set; }
        public string sCaseNum { get; set; }
        public string sCaseType { get; set; }
        public string sCaseIntroduce { get; set; }
        public Nullable<System.DateTime> dPreStaTime { get; set; }
        public Nullable<System.DateTime> dPreEndTime { get; set; }
        public Nullable<System.DateTime> dAclStaTime { get; set; }
        public Nullable<System.DateTime> dAclEndTime { get; set; }
        public string sCaseName { get; set; }
        public Nullable<int> iUserId { get; set; }
        public int iLawOrder { get; set; }
    }
}
