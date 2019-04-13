using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    /// <summary>
    /// 数据库映射的特性基类
    /// </summary>
    public class ElevenBaseMappingAttribute : Attribute
    {
        private string _Name = null;
        public ElevenBaseMappingAttribute(string name)
        {
            this._Name = name;
        }

        public virtual string GetName()
        {
            return this._Name;
        }
    }
}
