/*using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
public class GatorDAO
    {
        public List<GatorDM> ReadGator(string statement, SqlParameter[] parameters)
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
                    List<GatorDM> gators = new List<GatorDM>();
                    while (data.Read())
                    {
                        GatorDM gator = new GatorDM();
                        gator.id = Convert.ToInt32(data["id"]);
                        gator.hunterId = Convert.ToInt32(data["hunterId"]);
                        gator.length = Convert.ToDecimal(data["length"]);
                        gator.mass = Convert.ToDecimal(data["mass"]);
                        gators.Add(gator);
                    }
                    return gators;
                }
            }
        }
        public GatorDM GetGator(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadGator("GetGatorById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetGatorId(int hunterId, decimal length, decimal mass)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@hunterId", hunterId),
                    new SqlParameter("@length", length),
                    new SqlParameter("@mass", mass) 
                };
                return ReadGator("GetGatorId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateGator(int hunterId, decimal length, decimal mass)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@Length",length),
                    new SqlParameter("@Mass",mass)
                };
            dao.Write("CreateGator", parameters);
        }
        public bool DoesGatorExist(int id)
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