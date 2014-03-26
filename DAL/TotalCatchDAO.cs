/*using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class TotalCatchDAO
    {
        public List<TotalCatchDM> ReadTotalCatch(string statement, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(
                @"Data Source=.\SQLEXPRESS;Initial Catalog=GatorBase;Integrated Security=SSPI;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqlDataReader data = command.ExecuteReader();
                    List<TotalCatchDM> totalCatches = new List<TotalCatchDM>();
                    while (data.Read())
                    {
                        TotalCatchDM totalCatch = new TotalCatchDM();
                        totalCatch.id = Convert.ToInt32(data["id"]);
                        totalCatch.hunterId = Convert.ToInt32(data["hunterId"]);
                        totalCatch.totalLength = Convert.ToDecimal(data["totalLength"]);
                        totalCatch.totalMass = Convert.ToDecimal(data["totalMass"]);
                        totalCatches.Add(totalCatch);
                    }
                    return totalCatches;
                }
            }
        }
        public TotalCatchDM GetTotalCatch(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadTotalCatch("GetTotalCatchById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetTotalCatchId(int hunterId, decimal length, decimal mass)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@hunterId", hunterId),
                    new SqlParameter("@length", length),
                    new SqlParameter("@mass", mass) 
                };
                return ReadTotalCatch("GetTotalCatchId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateTotalCatch(int hunterId, decimal length, decimal mass)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@Length",length),
                    new SqlParameter("@Mass",mass)
                };
            dao.Write("CreateTotalCatch", parameters);
        }
        public bool DoesTotalCatchExist(int id)
        {
            if (id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}*/