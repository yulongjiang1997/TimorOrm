using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework
{
    public static class DBAttributeExtend
    {
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(ElevenBaseMappingAttribute), true))
            {
                ElevenBaseMappingAttribute attribute = (ElevenBaseMappingAttribute)t.GetCustomAttribute(typeof(ElevenBaseMappingAttribute), true);
                return attribute.GetName();
            }
            else
            {
                return t.Name;
            }
        }
        /// <summary>
        /// 排除自增主键，特性标记
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutKey(this Type type)
        {
            return type.GetProperties().Where(p => !p.IsDefined(typeof(ElevenKeyAttribute), true));
        }
        /// <summary>
        /// 只找当前类型声明的，也可以去掉主键
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutInherit(this Type type)
        {
            return type.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
        }



        //public static string GetMappingName(this Type type)
        //{
        //    if (type.IsDefined(typeof(ElevenTableAttribute), true))
        //    {
        //        ElevenTableAttribute attribute = (ElevenTableAttribute)type.GetCustomAttribute(typeof(ElevenTableAttribute), true);
        //        return attribute.GetName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}
        ///// <summary>
        ///// 获取属性的映射
        ///// </summary>
        ///// <param name="prop"></param>
        ///// <returns></returns>
        //public static string GetMappingName(this PropertyInfo prop)
        //{
        //    if (prop.IsDefined(typeof(ElevenColumnAttribute), true))
        //    {
        //        ElevenColumnAttribute attribute = (ElevenColumnAttribute)prop.GetCustomAttribute(typeof(ElevenColumnAttribute), true);
        //        return attribute.GetName();
        //    }
        //    else
        //    {
        //        return prop.Name;
        //    }
        //}
    }
}
