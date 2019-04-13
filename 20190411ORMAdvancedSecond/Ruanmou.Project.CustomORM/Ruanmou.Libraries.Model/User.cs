using Ruanmou.Framework;
using System;

namespace Ruanmou.Libraries.Model
{
    /// <summary>
    /// LM_User
    /// UserModel
    /// 
    /// 数据库叫User 程序中叫UserModel
    /// </summary>
    [ElevenTable("User")]
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        /// <summary>
        /// 数据库叫State  程序中叫Status
        /// </summary>
        [ElevenColumnAttribute("State")]
        public int Status { get; set; }
        public int UserType { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }

    }
}
