using System;
using System.Collections.Generic;
using System.Text;

namespace DataForStock.Models
{

    /// <summary>
    /// 股票研报数据
    /// http://reportapi.eastmoney.com/report/list?cb=datatable5806245&industryCode=*&pageSize=50&industry=*&rating=&ratingChange=&beginTime=2017-09-17&endTime=2019-09-17&pageNo=2&fields=&qType=0&orgCode=&code=*&rcode=&_=1568687455425
    /// </summary>
    public class StockAnalysisData
    {
        public int hits { get; set; }
        public int size { get; set; }
        public Datum[] data { get; set; }
        public int TotalPage { get; set; }
        public int pageNo { get; set; }
        public int currentYear { get; set; }
    }

    /// <summary>
    /// 数据
    /// </summary>
    public class Datum
    {
        public string title { get; set; }
        public string stockName { get; set; }
        public string stockCode { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public string orgSName { get; set; }
        public string publishDate { get; set; }
        public string infoCode { get; set; }
        public string column { get; set; }
        public string predictNextTwoYearEps { get; set; }
        public string predictNextTwoYearPe { get; set; }
        public string predictNextYearEps { get; set; }
        public string predictNextYearPe { get; set; }
        public string predictThisYearEps { get; set; }
        public string predictThisYearPe { get; set; }
        public string predictLastYearEps { get; set; }
        public string predictLastYearPe { get; set; }
        public string actualLastTwoYearEps { get; set; }
        public string actualLastYearEps { get; set; }
        public string industryCode { get; set; }
        public string industryName { get; set; }
        public string emIndustryCode { get; set; }
        public string indvInduCode { get; set; }
        public string indvInduName { get; set; }
        public string emRatingCode { get; set; }
        public string emRatingValue { get; set; }
        public string emRatingName { get; set; }
        public string lastEmRatingCode { get; set; }
        public string lastEmRatingValue { get; set; }
        public string lastEmRatingName { get; set; }
        public object ratingChange { get; set; }
        public int reportType { get; set; }
        public object author { get; set; }
        public string indvIsNew { get; set; }
        public string researcher { get; set; }
        public string newListingDate { get; set; }
        public string newPurchaseDate { get; set; }
        public object newIssuePrice { get; set; }
        public object newPeIssueA { get; set; }
        public string indvAimPriceT { get; set; }
        public string indvAimPriceL { get; set; }
        public string attachType { get; set; }
        public int attachSize { get; set; }
        public int attachPages { get; set; }
        public string encodeUrl { get; set; }
        public string sRatingName { get; set; }
        public string sRatingCode { get; set; }
        public string market { get; set; }
        public object authorID { get; set; }
        public int count { get; set; }
        public string orgType { get; set; }
    }
}
