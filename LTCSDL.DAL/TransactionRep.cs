using LTCSDL.Common.DAL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace LTCSDL.DAL
{
    public class TransactionRep : GenericRep<MyPhamContext, Transaction>
    {
        #region -- Overrides --
        private Common common = new Common();
        public override Transaction Read(int id)
        {
            var res = All.FirstOrDefault(x => x.Id == id);
            return res;
        }
        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m); //TODO
            return m.Id;
        }
        #endregion

        public List<object> CreateNewTransaction(int userId, int proId, decimal amount)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)Context.Database.GetDbConnection();

            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "OrderProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@proId", proId);
                cmd.Parameters.AddWithValue("@amount", amount);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            UserId = row["user_id"],
                            Amount = row["amount"],
                            TimeTransaction = row["time_transaction"],
                        };

                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }


        static DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("proId", typeof(Int32));
            dt.Columns.Add("pronum", typeof(Int32));
            return dt;
        }





        public List<object> CreateNewTransactionWithManyProducts(int userId, List<ProIDvsProNumReq> array, decimal amount)
        {
            List<object> res = new List<object>();

            DataTable myTable = CreateTable();
            array.ForEach(a => myTable.Rows.Add(a.ProId, a.ProNum));
            var cmn = (SqlConnection)Context.Database.GetDbConnection();

            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "OrderProducts";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@Products", myTable);
                cmd.Parameters.AddWithValue("@amount", amount);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            UserId = row["user_id"],
                            Amount = row["amount"],
                            TimeTransaction = row["time_transaction"],
                        };

                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }





        /*public object findTransactionByCustomerId(int id)
        {
            var data = Context.User.Join(Context.Transaction, a => a.Id, b => b.UserId, (a, b) => new
            {
                a.Id,
                a.Username,
                a.Ho,
                a.Ten,
                a.Sdt,
                a.Email,
                b.UserId,
                b.Amount,
                b.TimeTransaction,


            });


            return null;
        }*/

        public List<object> findTransactionByTransactionId(int transactionId)
        {
            List<object> res = new List<object>();

            var cmn = (SqlConnection)Context.Database.GetDbConnection();

            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "FindTranSactionByTransactionId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionId", transactionId);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            UserId = row["user_id"],
                            Amount = row["amount"],
                            TimeTransaction = row["time_transaction"],
                            ProductId = row["product_id"],
                            Productname = row["productname"],
                            Price = row["price"],
                            Quantity = row["quantity"]
                        };

                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }


        public List<object> removeOrderProductsTransction(int tranId, List<ProIDvsProNumReq> array)
        {
            List<object> res = new List<object>();
            DataTable myTable = CreateTable();
            array.ForEach(a => myTable.Rows.Add(a.ProId, a.ProNum));
            var cmn = (SqlConnection)Context.Database.GetDbConnection();

            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "DeleteProductsTransaction";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TranId", tranId);
                cmd.Parameters.AddWithValue("@Products", myTable);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            TransactionId = row["transaction_id"],
                        };

                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }


        public object DeleteTransaction(int userId,int tranId )
        {
            object res = null;
            var cmn = (SqlConnection)Context.Database.GetDbConnection();
            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "DeleteTransactionByUservsTran";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@tranid", tranId);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    var x = new
                    {
                        Id = row["id"],
                        Ten = row["ten"],
                        
                    };

                    res = x;
                
                }
            }
            catch (Exception)
            {
                res = null;
            }

            return res;
        }


        public List<object> findTransactionByUserIdvsTranId(int userId,int tranID)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)Context.Database.GetDbConnection();
            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "FindTransactionByUserId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@tranid", tranID);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Ho = row["ho"],
                            Ten = row["ten"],
                            TimeTransaction = row["time_transaction"],
                            Amount = row["amount"],
                            ProductId = row["product_id"],
                            Quantity = row["quantity"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }

            return res;


        }

        public object findTransactionByUser(int userId)
        {
            var res = All.Join(Context.User, a => a.UserId, b => b.Id, (a, b) => new
            {
                a.UserId,
                a.Id,
                a.TimeTransaction,
                a.Amount,
            }).Where(x => x.UserId == userId);

            var data = res.OrderByDescending(x => x.TimeTransaction)
                .Select(x => new
                {
                    Id = x.Id,
                    TimeTransaction = x.TimeTransaction,
                    Amount = x.Amount,
                    UserId = x.UserId,

                });
            return data;

        }

        public object findTransactionPagination(int page, int size)
        {
            var pro = All.Select(x=>x);


            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.TimeTransaction).Skip(offset).Take(size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = page,
                Size = size,
            };

            return res;

        }

        public object findByDateTransaction(int page,int size,DateTime date)
        {
            var tran = All.Where(x => x.TimeTransaction == date);
            
            var offset = (page - 1) * size;
            var total = tran.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = tran.OrderBy(x => x.TimeTransaction).Skip(offset).Take(size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = page,
                Size = size,
            };

            return res;
        }

        public object StatisticalByDate(int page,int size)
        {
            var tran = All.GroupBy(x => x.TimeTransaction)
                .Select(x => new
                {
                    TimeTraction = x.Key,
                    Total = x.Sum(x => x.Amount)
                });
                
            var offset = (page - 1) * size;
            var total = tran.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = tran.OrderByDescending(x => x.TimeTraction).Skip(offset).Take(size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = page,
                Size = size,
            };

            return res;


        }




    }




}
