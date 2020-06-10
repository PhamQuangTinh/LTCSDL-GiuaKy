using LTCSDL.Common.DAL;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LTCSDL.DAL
{
    public class BasketRep : GenericRep<MyPhamContext, Basket>
    {

        private Common common = new Common();
        public List<object> AddNewProductToBasket(int proId, int userId, String proname, decimal price, int proInvent, String proImg)
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
                cmd.CommandText = "AddNewProductToBasket";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proId",proId );
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@proname", proname);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@proInvent", proInvent);
                cmd.Parameters.AddWithValue("@proImg", proImg);

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Proid = row["proid"],
                            Userid = row["userid"],
                            Productname = row["productname"],
                            Price = row["price"],
                            ProductInventory = row["product_inventory"],
                            ProductImgLink = row["product_img_link"],
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

        public List<object> DeleteBasket(int userId)
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
                cmd.CommandText = "DeleteBasket";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
              

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            Username = row["username"],
                            Ten = row["ten"],
                            Email = row["email"],

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

        public List<object> DeleteProductsInBasket(int userId, int proId)
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
                cmd.CommandText = "DeleteProductsInBasket";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@proId", proId);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            Username = row["username"],
                            Ten = row["ten"],
                            Email = row["email"],

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

        public object GetBasket(int UserId)
        {
            var res = All.Where(x => x.Userid == UserId);
            return res;
        }

        public object UpdateProdcutBasket(int userId, int proId, int proInvent)
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
                cmd.CommandText = "UpdateProductBasket";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@proid", proId);
                cmd.Parameters.AddWithValue("@proinvent", proInvent);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    var x = new
                    {
                        Proid = row["proid"],
                        Userid = row["userid"],
                        Productname = row["productname"],
                        Price = row["price"],
                        ProductInventory = row["product_inventory"],
                        ProductImgLink = row["product_img_link"],
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
    }
}
