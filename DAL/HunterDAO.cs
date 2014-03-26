using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class HunterDAO
    {
        public List<HunterDM> ReadHunter(string statement, SqlParameter[] parameters)
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
                    List<HunterDM> hunters = new List<HunterDM>();
                    while (data.Read())
                    {
                        HunterDM hunter = new HunterDM();
                        hunter.id = Convert.ToInt32(data["id"]);
                        hunter.lastName = data["lastName"].ToString();
                        hunter.firstName = data["firstName"].ToString();
                        hunter.email = data["email"].ToString();
                        hunters.Add(hunter);
                    }
                    return hunters;
                }
            }
        }
        public HunterDM GetHunter(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadHunter("GetHunterById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetHunterId(string lastName, string firstName, string email)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastName",lastName),
                    new SqlParameter("@FirstName",firstName),
                    new SqlParameter("@Email",email)
                };
                return ReadHunter("GetHunterId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetHunterById(string lastName, string firstName, string email)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastName",lastName),
                    new SqlParameter("@FirstName",firstName),
                    new SqlParameter("@Email",email)
                };
                return ReadHunter("GetHunterById",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateHunter(string lastName, string firstName, string email)
        {
            DAO dao = new DAO();

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastName",lastName),
                    new SqlParameter("@FirstName",firstName),
                    new SqlParameter("@Email",email)
                };
            dao.Write("CreateHunter", parameters);

        }
        public bool DoesHunterExist(int id)
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
}