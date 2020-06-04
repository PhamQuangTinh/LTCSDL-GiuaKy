using LTCSDL.Common.DAL;
using LTCSDL.Common.Req;
using LTCSDL.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
namespace LTCSDL.DAL
{
    public class TransactionRep : GenericRep<MyPhamContext, Transaction>
    {
        #region -- Overrides --
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





        public object findTransactionByCustomerId(int id)
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
        }

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
    }

}
