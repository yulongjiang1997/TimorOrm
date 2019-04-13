using Ruanmou.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DAL
{
    /// <summary>
    /// SQL生成+缓存
    /// 
    /// 完全不知道的刷2   听我讲 看过的 了解的 刷个1
    /// </summary>
    public class SqlBuilder<T>
    {
        private static string FindOneSql = null;
        private static string InsertSql = null;
        private static string UpdateSql = null;
        static SqlBuilder()
        {
            Type type = typeof(T);
            string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            string valuesStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            InsertSql = $@"INSERT INTO [{type.GetMappingName()}]  ({columnStrings})                         VALUES( {valuesStrings});";//SELECT @@Identity;

            string columnStringsUpdate = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]=@{p.GetMappingName()}"));
            UpdateSql = $"UPDATE [{type.GetMappingName()}] SET {columnStringsUpdate} WHERE Id=@Id;";

            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            FindOneSql = $"SELECT {columnString} FROM [{type.GetMappingName()}]  WHERE Id =@Id; ";
        }

        public static string GetSql(SqlType sqlType)
        {
            switch (sqlType)
            {
                case SqlType.FindOne:
                    return FindOneSql;
                case SqlType.Insert:
                    return InsertSql;
                case SqlType.Update:
                    return UpdateSql;
                default:
                    throw new Exception("wrong SqlType");
            }
        }
    }

    public enum SqlType
    {
        FindOne,
        Insert,
        Update
    }
}
