using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtApiDemo.Models
{
    public class DataCache
    {
        #region 缓存数据，将来可以放到缓存数据库中
        /// <summary>
        /// 登录的用户，key:用户ID，value：token,refretoken:刷新token,time：登录时间(单位时间只能登录一个)
        /// </summary>
        public static Dictionary<string, Tuple<string, string,DateTime>> loginUserDict = new Dictionary<string, Tuple<string, string, DateTime>>();
        /// <summary>
        /// 刷新token，key：refretoken，value：用户ID，过期时间
        /// </summary>
        public static Dictionary<string, Tuple<string, DateTime>> refreshTokenDict = new Dictionary<string, Tuple<string, DateTime>>();

        /// <summary>
        /// 只能一个账户登录的时候，删除Refresh
        /// </summary>
        /// <param name="userid"></param>
        public static void AddRefreshToken(string userid,string refreshToken)
        {
            List<string> preLogin = refreshTokenDict.Where(x => x.Value.Item1 == userid).Select(x => x.Key).ToList();
            preLogin.ForEach(x => refreshTokenDict.Remove(x));

            refreshTokenDict.Add(refreshToken, new Tuple<string, DateTime>(userid, DateTime.Now.AddDays(2)));
        }
        #endregion
    }

    /// <summary>
    /// 后一个账户替换前一个账户登录（超过单位时间才可以挤掉，目的：防止后一个不停的挤掉前一个）
    /// </summary>
    public class LoginInFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var name = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var token = context.HttpContext.User.FindFirst("token")?.Value;
            if (name != null && DataCache.loginUserDict.ContainsKey(name) && DataCache.loginUserDict[name].Item1 != token)
            {
                context.Result = new ObjectResult("已经有用户登录此账户，请重新登录并且修改密码");
                return;
            }

        }
    }
}
