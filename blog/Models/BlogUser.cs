using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 权限
    /// </summary>
    public enum Privileges
    {
        Normal,
        Admin
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class BlogUser
    {
        public int Id { get; set; }
        [DisplayName("用户名")]
        public string Name { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }

        [DisplayName("权限")]
        public Privileges Privileges { get; set; }
    }
}
