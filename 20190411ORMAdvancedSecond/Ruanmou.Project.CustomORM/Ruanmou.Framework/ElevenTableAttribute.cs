using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    /// <summary>
    /// 数据库映射的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ElevenTableAttribute : ElevenBaseMappingAttribute
    {
        public ElevenTableAttribute(string name) : base(name)
        {
        }
    }
}
