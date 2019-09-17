using System;
using System.Collections.Generic;
using System.Text;

namespace DataForStock.Models
{
    /// <summary>
    /// http://datainterface.eastmoney.com//EM_DataCenter/js.aspx?type=SR&sty=GGSR&js=var%20sNGkmMSG={%22data%22:[(x)],%22pages%22:%22(pc)%22,%22update%22:%22(ud)%22,%22count%22:%22(count)%22}&ps=50&p=1&mkt=0&stat=0&cmd=2&code=&rt=52289725
    /// </summary>
    public class EM_DataCenter
    {
        public DataCenterDatum[] data { get; set; }
        public string pages { get; set; }
        public string update { get; set; }
        public string count { get; set; }
    }

    public class DataCenterDatum
    {
        public string author { get; set; }
        public string change { get; set; }
        public string companyCode { get; set; }
        public DateTime datetime { get; set; }
        public string infoCode { get; set; }
        public string insCode { get; set; }
        public string insName { get; set; }
        public string insStar { get; set; }
        public string[] jlrs { get; set; }
        public string rate { get; set; }
        public string secuFullCode { get; set; }
        public string secuName { get; set; }
        public string sratingName { get; set; }
        public string sy { get; set; }
        public string[] syls { get; set; }
        public string[] sys { get; set; }
        public string title { get; set; }
        public string profitYear { get; set; }
        public string type { get; set; }
        public string newPrice { get; set; }
    }

}
