using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class HunterPhoneDAO
    {
        public List<HunterPhoneDM> ReadHunterPhone(string statement,
            SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(
                @"Data Source=.\SQLEXPRESS;Initial Catalog=GatorBase;
                Integrated Security=SSPI;"))
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
                    List<HunterPhoneDM> hunterPhones = new List<HunterPhoneDM>();
                    while (data.Read())
                    {
                        HunterPhoneDM hunterPhone = new HunterPhoneDM();
                        hunterPhone.id = Convert.ToInt32(data["id"]);
                        hunterPhone.hunterId = Convert.ToInt32(data["hunterId"]);
                        hunterPhone.phoneId = Convert.ToInt32(data["phoneId"]);
                        hunterPhone.typeId = Convert.ToInt32(data["typeId"]);
                        hunterPhones.Add(hunterPhone);
                    }
                    return hunterPhones;
                }
            }
        }
        public HunterPhoneDM GetHunterPhone(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadHunterPhone("GetHunterPhoneById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetHunterPhoneId(int hunterId, int phoneId, int typeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@PhoneId",phoneId),
                    new SqlParameter("@TypeId",typeId)
                };
                return ReadHunterPhone("GetHunterPhoneId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateHunterPhone(int hunterId, int phoneId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@PhoneId",phoneId),
                    new SqlParameter("@TypeId",typeId)
                };
            dao.Write("CreateHunterPhone", parameters);
        }
        public bool DoesHunterPhoneExist(int id)
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
        public void UpdateHunterPhoneDB(int id, string lastName, string firstName, string email,
            string number, string phoneType)
        {
            DAO dao = new DAO();
            HunterDAO pdao = new HunterDAO();
            PhoneDAO phdao = new PhoneDAO();
            TypeDAO tdao = new TypeDAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@HunterId",pdao.GetHunterId(lastName, firstName, email)),
                new SqlParameter("@PhoneId",phdao.GetPhoneId(number)),
                new SqlParameter("@TypeId",tdao.GetTypeId(phoneType))
            };
            dao.Write("UpdateHunterPhone", parameters);
        }
    }
}