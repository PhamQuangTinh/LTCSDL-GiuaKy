using System;

using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;

namespace LTCSDL.DAL
{

    using LTCSDL.Common.Rsp;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    public class CommentRep : GenericRep<MyPhamContext, Comment>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Comment Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m); //TODO
            return m.Id;
        }
        #endregion



        public List<object> CreateComment(int userId , int proId, String content)
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
                cmd.CommandText = "AddNewCommentToProdcut";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProId", proId);
                cmd.Parameters.AddWithValue("@Content", content);
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
                            UserName = row["user_name"],
                            ProductId = row["product_id"],
                            CommentContent = row["comment_content"],
                            TimeComment = row["time_comment"]
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

        public object UpdateCommon(Comment cmt)
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
                cmd.CommandText = "UpdateComment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@comID", cmt.Id);
                cmd.Parameters.AddWithValue("@userID", cmt.UserId);
                cmd.Parameters.AddWithValue("@proID", cmt.ProductId);
                cmd.Parameters.AddWithValue("@content", cmt.CommentContent);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    var x = new
                    {
                        Id = row["id"],
                        UserId = row["user_id"],
                        UserName = row["user_name"],
                        ProductId = row["product_id"],
                        CommentContent = row["comment_content"],
                        TimeComment = row["time_comment"]
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

        public SingleRsp RemoveComment(Comment cmt)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                            var t = context.Comment.Remove(cmt);
                            res.Data = cmt;
                            context.SaveChanges();
                            tran.Commit();


                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }

            }

            return res;
        }

        public object GetAllCommentProduct(int productId) {
            var data = All.Join(Context.Product, a => a.ProductId, b => b.Id, (a, b) => new
            {
                a.Id,
                a.UserId,
                a.UserName,
                a.ProductId,
                a.CommentContent,
                a.TimeComment,
            }).Where(x => x.ProductId == productId).ToList();

            var res = data.OrderBy(x => x.TimeComment).
                Select(x => new
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    ProductId = x.ProductId,
                    CommentContent = x.CommentContent,
                    TimeComment = x.TimeComment
                });
            return res;
        }
        private Common common = new Common();
    }

   
}
