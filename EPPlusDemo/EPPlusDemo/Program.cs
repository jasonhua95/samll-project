using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EPPlusDemo
{
    /// <summary>
    /// EPPlus文档地址 https://github.com/JanKallman/EPPlus/wiki/Getting-Started
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //WriteExcel();
            //ReadExcel();
            Merge();
            Console.WriteLine("Hello World!");
            Console.Read();
        }

        static void Merge()
        {
            var file = new FileInfo(@"E:\GitPublic\samll-project\EPPlusDemo\EPPlusDemo\test.xlsx");

            //创建工作表
            using (ExcelPackage ep = new ExcelPackage(file)) {
                ExcelWorkbook wb = ep.Workbook;
                //配置文件属性
                wb.Properties.Category = "类别";
                wb.Properties.Author = "作者";
                wb.Properties.Comments = "备注";
                wb.Properties.Company = "公司";
                wb.Properties.Keywords = "关键字";
                wb.Properties.Manager = "管理者";
                wb.Properties.Status = "内容状态";
                wb.Properties.Subject = "主题";
                wb.Properties.Title = "标题";
                wb.Properties.LastModifiedBy = "最后一次保存者";
                //写数据
                ExcelWorksheet ws = wb.Worksheets.Add("我的工作表");
                ws.Cells[1, 1].Value = "Hello";
                ws.Cells["B1"].Value = "World";
                ws.Cells[3, 3, 3, 5].Merge = true;
                ws.Cells[3, 3].Value = "Cells[3, 3, 3, 5]合并";
                ws.Cells["A4:D5"].Merge = true;
                ws.Cells["A4"].Value = "Cells[\"A4:D5\"]合并";
                ep.Save();
            }
        }

        static void WriteExcel()
        {
            var file = new FileInfo(@"E:\GitPublic\samll-project\EPPlusDemo\EPPlusDemo\test.xlsx");
            using (var p = new ExcelPackage(file))
            {
                string sheetName = "MySheet";
                ExcelWorksheet ws = null;
                foreach (var s in p.Workbook.Worksheets)
                {
                    if (s.Name == sheetName)
                    {
                        ws = s;
                        break;
                    }
                }
                if (ws == null)
                {
                    ws = p.Workbook.Worksheets.Add(sheetName);
                }


                ws.Cells["A1"].Value = "This is cell A1";
                ws.Cells[2, 1].Value = "Test";
                p.Save();
            }
        }

        static void ReadExcel()
        {
            var file = new FileInfo(@"E:\GitPublic\samll-project\EPPlusDemo\EPPlusDemo\test.xlsx");
            using (var p = new ExcelPackage(file))
            {
                StringBuilder sb = new StringBuilder();
                ExcelWorksheet worksheet = p.Workbook.Worksheets[0];
                int row = worksheet.Dimension.Rows;
                int col = worksheet.Dimension.Columns;
                for (int r = 1; r <= row; r++)
                {
                    for (int c = 1; c <= col; c++)
                    {
                        sb.Append(worksheet.Cells[r, c].Value.ToString() + "\t");
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }
}
