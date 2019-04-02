using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 替换渲染器
    /// 使用方法：await ReplaceRenderer.ParseAsync("hi @UserName,年龄： @Age，小名：@XiaoName 。", new { UserName = "测试", Age = 19, XiaoName = "小明" })
    /// </summary>
    public class ReplaceRenderer
    {
        public static string Parse<T>(string template, T model)
        {
            foreach (PropertyInfo pi in model.GetType().GetRuntimeProperties())
            {
                template = template.Replace($"@{pi.Name}", pi.GetValue(model)?.ToString());
            }

            return template;
        }

        public static Task<string> ParseAsync<T>(string template, T model)
        {
            return Task.FromResult(Parse(template, model));
        }
    }
}
