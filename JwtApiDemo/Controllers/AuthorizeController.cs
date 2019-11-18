using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtApiDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly ILogger<AuthorizeController> _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthorizeController(ILogger<AuthorizeController> logger, IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Token(LoginModel login)
        {
            _logger.LogInformation($"获取Token：User：{login.User}");
            if (string.IsNullOrEmpty(login.User) || string.IsNullOrEmpty(login.Password))//判断账号密码是否正确
            {
                return BadRequest();
            }


            var claim = new List<Claim>{
                    new Claim(ClaimTypes.Name,login.User),
                    new Claim(ClaimTypes.Role,"Test")
                };

            //登录的账户缓存
            if (DataCache.loginUserDict.ContainsKey(login.User))
            {
                //这个时间可以调长点
                if (DataCache.loginUserDict[login.User].Item3.AddSeconds(10) > DateTime.Now)
                {
                    return Ok(new { message = "已经有用户登录，请10S之后在登录，并且修改密码。" });
                }
                DataCache.loginUserDict.Remove(login.User);
            } 
            string tempToken = Guid.NewGuid().ToString();
            string tempRefshToken = Guid.NewGuid().ToString();
            DataCache.loginUserDict.Add(login.User, new Tuple<string, string, DateTime>(tempToken, tempRefshToken, DateTime.Now));            
            DataCache.AddRefreshToken(login.User, tempRefshToken);
            claim.Add(new Claim("token", tempToken));

            //建立增加策略的授权
            if (login.User == "Test") claim.Add(new Claim("Test", "Test"));
            if (login.User == "Test1") claim.Add(new Claim("Test", "Test1"));
            if (login.User == "Test2") claim.Add(new Claim("Test", "Test2"));
            if (login.User == "Test3") claim.Add(new Claim("Test", "Test3"));

            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);


            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), refreshtoken = tempRefshToken });//refreshtoken：jwt过期，刷新token
        }

        /// <summary>
        /// 
        /// JWT过期401Unauthorized
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> TokenForAuthExpire(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return "请重新登录";
            }

            return $"基于策略的登录";
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> NoAuthValue()
        {
            _logger.LogInformation($"没有权限的API");
            return "没有权限的访问";
        }

        /// <summary>
        /// 角色的授权
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Test")]
        public ActionResult<string> AuthValue()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            _logger.LogInformation($"权限登录,用户名：{name},角色：{role}");

            return $"权限登录,用户名：{name},角色：{role}";
        }

        /// <summary>
        /// 策略的授权
        /// jwt过期和错误返回401Unauthorized，禁止访问返回403Forbidden
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "OnlyTestAccess")]
        public ActionResult<string> AuthExtensionValue()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            _logger.LogInformation($"基于策略的登录,用户名：{name},角色：{role}");

            return $"基于策略的登录,用户名：{name},角色：{role}";
        }

    }
}