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
        /// int?�ɿ��ֶ� �ȿ�����int  Ҳ������null
        /// ���ݿ���Ƶ�ʱ��  �ֶ��ǿɿ�null
        /// </summary>
        public int? LastModifierId { get; set; }//Nullable<int>
        public DateTime? LastModifyTime { get; set; }
    }
}