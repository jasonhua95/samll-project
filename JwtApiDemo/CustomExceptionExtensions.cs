using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JwtApiDemo
{

    /// <summary>
    /// 自定义一个异常处理的中间件
    /// 参考UseDeveloperExceptionPage的写法
    /// https://github.com/aspnet/AspNetCore/blob/28157e62597bf0e043bc7e937e44c5ec81946b83/src/Middleware/Diagnostics/src/DeveloperExceptionPage/DeveloperExceptionPageExtensions.cs
    /// </summary>
    public static class CustomExceptionExtensions
    {
        public static IApplicationBuilder UseCustomExceptionExtensions(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

    /// <summary>
    /// 参考地址DeveloperExceptionPageMiddleware
    /// https://github.com/aspnet/AspNetCore/blob/d4a2fa1ceb3b9c10054da8a7c6a4bd61801aea18/src/Middleware/Diagnostics/src/DeveloperExceptionPage/DeveloperExceptionPageMiddleware.cs
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public CustomExceptionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
            _logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");

                var statusCode = context.Response.StatusCode;
                string msg;
                if (statusCode == 401) msg = "请求要求用户的身份认证";
                else if (statusCode == 403) msg = "服务器理解请求客户端的请求，但是拒绝执行此请求";
                else if (statusCode == 404) msg = "未找到服务";
                else if (statusCode == 502) msg = "无效的请求";
                else if (statusCode != 200) msg = "未知错误";
                else msg = "服务器异常，请稍后重试";
                await HandleExceptionAsync(context, statusCode, msg);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var result = JsonConvert.SerializeObject(new { Success = false, Msg = msg, Type = statusCode.ToString() });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }

    }
}
