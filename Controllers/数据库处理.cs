using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using TelerikMvcApp3.Models;

namespace TelerikMvcApp3
{

    public class 数据库处理
    {
        private string connStr = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

        public List<SKViewModel> 获取指定时间统计信息(DateTime sDate, DateTime eDate)
        {
            List<SKViewModel> mylists = new List<SKViewModel>();
            using (SqlConnection mycon = new SqlConnection(connStr))
            {
                try
                {
                    mycon.Open(); //打开
                    string sql = $"select * from 入库个数统计信息 where 时间>='{sDate}' and 时间<='{eDate}'"; //SQL查询语句 (Name,StationID,Date)。按照数据库中的表的字段顺序保存
                    SqlCommand sqlman = new SqlCommand(sql, mycon);
                    SqlDataReader sqlreader = sqlman.ExecuteReader();

                    while (sqlreader.Read())
                    {
                        try
                        {
                            mylists.Add(new SKViewModel
                            {
                                统计个数 = sqlreader.GetInt32(sqlreader.GetOrdinal("个数")),
                                时间 = sqlreader.GetDateTime(sqlreader.GetOrdinal("时间")),
                                名称 = sqlreader.GetString(sqlreader.GetOrdinal("表名"))
                            });
                        }
                        catch
                        {
                        }
                    }
                }

                catch
                {

                }
            }
            return mylists;
        }
        public List<SKViewModel> 获取指定时间统计信息(DateTime sDate, DateTime eDate, string dataName)
        {
            List<SKViewModel> mylists = new List<SKViewModel>();
            using (SqlConnection mycon = new SqlConnection(connStr))
            {
                try
                {
                    mycon.Open(); //打开
                    string sql = $"select * from 入库个数统计信息 where 表名 in ({dataName}) and 时间>='{sDate}' and 时间<='{eDate}'"; //SQL查询语句 (Name,StationID,Date)。按照数据库中的表的字段顺序保存
                    SqlCommand sqlman = new SqlCommand(sql, mycon);
                    SqlDataReader sqlreader = sqlman.ExecuteReader();

                    while (sqlreader.Read())
                    {
                        try
                        {
                            mylists.Add(new SKViewModel
                            {
                                统计个数 = sqlreader.GetInt32(sqlreader.GetOrdinal("个数")),
                                时间 = sqlreader.GetDateTime(sqlreader.GetOrdinal("时间")),
                                名称 = sqlreader.GetString(sqlreader.GetOrdinal("表名"))
                            });
                        }
                        catch
                        {
                        }
                    }
                }

                catch
                {

                }
            }
            return mylists;
        }

        public List<资料种类> 获取数据库表信息()
        {
            List<资料种类> datalist = new List<资料种类>();
            using (SqlConnection mycon = new SqlConnection(connStr))
            {
                try
                {
                    mycon.Open(); //打开
                    string sql = "select * from 数据库表信息"; //SQL查询语句 (Name,StationID,Date)。按照数据库中的表的字段顺序保存
                    SqlCommand sqlman = new SqlCommand(sql, mycon);
                    SqlDataReader sqlreader = sqlman.ExecuteReader();
                    while (sqlreader.Read())
                    {
                        try
                        {
                            datalist.Add(new 资料种类(sqlreader.GetInt32(sqlreader.GetOrdinal("序号")), sqlreader.GetString(sqlreader.GetOrdinal("表名称")).Trim(), sqlreader.GetString(sqlreader.GetOrdinal("数据库表名称")).Trim()));
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }

            return datalist;
        }
    }
}