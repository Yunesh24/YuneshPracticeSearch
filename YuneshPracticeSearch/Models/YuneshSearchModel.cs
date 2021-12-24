using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using YuneshPracticeSearch.Data;
namespace YuneshPracticeSearch.Models
{
    public class YuneshSearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string SaveCustomer(YuneshSearchModel model)
        {
            string msg = "";
            chocolateEntities Db = new chocolateEntities();

            var CustomerSave = new tbl_choco()
            {
                //CustId = model.CustId,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                
                
            };
            Db.tbl_choco.Add(CustomerSave);
            Db.SaveChanges();
            msg = "Customer Added Successfully";
            return msg;


        }
        public List<YuneshSearchModel> SearchCustomer(string Prefix)
        {
            try
            {
                List<YuneshSearchModel> model = new List<YuneshSearchModel>();
                chocolateEntities db = new chocolateEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCustomerSearch";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter LID = cmd.CreateParameter();
                        LID.ParameterName = "SearchString";
                        LID.Value = Prefix;
                        cmd.Parameters.Add(LID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["NotesDate"].ToString());
                            }
                            catch
                            {

                            }
                            model.Add(new YuneshSearchModel()
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                Name = dr["Name"].ToString(),
                                Email = dr["Email"].ToString(),
                                Address = dr["Address"].ToString(),
                                // SalePrice = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                
                                
                            });
                        }


                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}