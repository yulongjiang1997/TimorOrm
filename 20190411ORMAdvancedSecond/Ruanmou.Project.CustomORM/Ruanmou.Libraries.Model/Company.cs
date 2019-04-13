using System;

namespace Ruanmou.Libraries.Model
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        /// <summary>
        /// Eleven 
        /// int?可空字段 既可以是int  也可以是null
        /// 数据库设计的时候  字段是可空null
        /// </summary>
        public int? LastModifierId { get; set; }//Nullable<int>
        public DateTime? LastModifyTime { get; set; }
    }
}