using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApiDemo.AuthExtension
{
    public interface IPermissionAuthorizeData:IAuthorizeData
    {
        string Groups { get; set; }
        string Permissions { get; set; }
    }
}
