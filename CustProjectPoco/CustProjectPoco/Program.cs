using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CustProjectPoco
{
    public class Customers
    {
        public string CId { get; set; }
        public string CName { get; set; }
        public string DOB { get; set; }
        public string City { get; set; }
    }
    class Program
    {
        public static void GetDataByGetMethodsOfDr()
        {
            List<Customers> Custlist = new List<Customers>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TblCustomer", cn))
                {
                    using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (sdr.Read())
                        {
                            Custlist.Add(new Customers
                            {
                                CId=sdr.GetString(sdr.GetOrdinal("customerid")),
                                CName = sdr.GetString(sdr.GetOrdinal("CName")),
                                DOB=sdr.GetDateTime(sdr.GetOrdinal("DOB")).ToString(),
                                City = sdr.GetString(sdr.GetOrdinal("City"))
                            });
                        }
                    }
                }
                Console.WriteLine("------------------------------");
                foreach (var r in Custlist)
                {
                    Console.WriteLine($"Customer Id:{r.CId}\nCustomerName : {r.CName}\nDOB:{r.DOB}\nCity : {r.City}\n");
                }
            }
        }
        public static void GetDataBySqlDataReader()
        {
            StringBuilder sb = new StringBuilder(1024);
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from TblCustomer", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            sb.AppendLine($"Customer ID:{dr[0].ToString()}");
                            sb.AppendLine($"CustomerName:{dr[1].ToString()}");
                            sb.AppendLine($"DOB:{dr[2].ToString()}");
                            sb.AppendLine($"City:{dr[3].ToString()}");
                            sb.AppendLine();
                        }
                    }
                }
            }
            Console.WriteLine("--------------------------------------------");
            var res = sb.ToString();
            Console.WriteLine(res);
        }
        public static void GetDataByGetFieldValue()
        {
            List<Customers> Custlist = new List<Customers>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ToString()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TblCustomer", cn))
                {
                    using (SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (sd.Read())
                        {
                            Custlist.Add(new Customers
                            {
                                CId=sd.GetFieldValue<string>(sd.GetOrdinal("customerid")),
                                CName = sd.GetFieldValue<string>(sd.GetOrdinal("CName")),
                                DOB=sd.GetFieldValue<DateTime>(sd.GetOrdinal("dob")).ToString(),
                                City = sd.GetFieldValue<string>(sd.GetOrdinal("City"))
                            });
                        }
                    }
                }
                Console.WriteLine("----------------------------------------------");
                foreach (var r in Custlist)
                {
                    Console.WriteLine($"Customer Id:{r.CId}\nCustomerName : {r.CName}\nDOB:{r.DOB}\nCity : {r.City}\n");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Getting data using Datareader");
            GetDataBySqlDataReader();
            Console.WriteLine("getting data using datareadermethods");
            GetDataByGetMethodsOfDr();
            Console.WriteLine("getting data using getfieldvalues");
            GetDataByGetFieldValue();

        }
    }
}
