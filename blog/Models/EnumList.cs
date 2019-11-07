using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 枚举的处理
    /// </summary>
    public class EnumList
    {
        static EnumList()
        {
            PrivilegeList = Enum.GetNames(typeof(Privileges)).ToList();
        }

        public static IList<string> PrivilegeList;
    }
}
