using DynamicNews.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DynamicNews.Services
{
    /// <summary>
    /// 新闻服务
    /// </summary>
    public class NewsService
    {
        /// <summary>
        /// 匹配模板中的占位符（${字母、数字、下划线、空格}）
        /// </summary>
        private const string PATTERN = @"[\$][\{][a-zA-Z0-9_ ]{1,}[\}]";

        /// <summary>
        /// 模板文件的路径
        /// </summary>
        private readonly static string READ_PATH = ".";

        /// <summary>
        /// 生成文件的路径
        /// </summary>
        private readonly static string WRITE_PATH = "./wwwroot";

        /// <summary>
        /// 自动生成文件并且返回文件的全路径
        /// </summary>
        /// <param name="dictValue"></param>
        /// <param name="newTemplate"></param>
        /// <returns></returns>
        public static string AutoDynamicNews(Dictionary<string, string> dictValue, string newTemplate)
        {
            string readPath = $"{READ_PATH}/Template/{newTemplate}.html";
            string name = $"/news/{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.html";
            //string writePath = $"{WRITE_PATH}//news//{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.html";
            string writePath = $"{WRITE_PATH}{name}";

            string data;
            using (FileStream fs = new FileStream(readPath, FileMode.Open, FileAccess.Read))
            using (FileStream fsw = new FileStream(writePath, FileMode.Create))
            {
                using (StreamReader r = new StreamReader(fs))
                using (StreamWriter w = new StreamWriter(fsw, Encoding.UTF8))
                {
                    while (r.Peek() > 0)
                    {
                        data = r.ReadLine();

                        foreach (Match match in Regex.Matches(data, PATTERN))
                        {
                            if (dictValue.ContainsKey(match.Value))
                            {
                                data = data.Replace(match.Value, dictValue[match.Value]);
                            }
                        }

                        w.WriteLine(data);
                    }
                }
            }

            return $"{AppSettings.GetValueByKey("www")}{name}";
        }

        /// <summary>
        /// 获取模板中的标签
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <returns></returns>
        public static List<string> FindNewsTag(string templateName)
        {
            List<string> template = new List<string>();
            string readPath = $"{READ_PATH}/Template/{templateName}.html";

            string data;
            using (FileStream fs = new FileStream(readPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    while (r.Peek() > 0)
                    {
                        data = r.ReadLine();

                        foreach (Match match in Regex.Matches(data, PATTERN))
                        {
                            if (!template.Contains(match.Value)) template.Add(match.Value);
                        }
                    }
                }
            }

            return template;
        }

        /// <summary>
        /// 获取模板名称列表
        /// </summary>
        /// <returns></returns>
        public static List<string> FindNewsName()
        {
            List<string> template = new List<string>();
            var filePath = Directory.GetFiles($@"{READ_PATH}/Template");
            foreach (var path in filePath)
            {
                var last = path.LastIndexOf("\\") + 1;
                template.Add(path.Substring(last, path.Length - last - 5));//去掉后缀名
            }

            return template;
        }

        /// <summary>
        /// check文件，不存在的时候创建
        /// </summary>
        /// <param name="path"></param>
        public static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf(@"\\")));
                    var file = File.Create(path);
                    file.Close();
                }
            }
        }


    }
}
