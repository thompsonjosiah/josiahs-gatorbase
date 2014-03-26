using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class HunterAddressDAO
    {
        public List<HunterAddressDM> ReadHunterAddress(string statement,
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
                    List<HunterAddressDM> hunterAddresses = new List<HunterAddressDM>();
                    while (data.Read())
                    {
                        HunterAddressDM hunterAddress = new HunterAddressDM();
                        hunterAddress.id = Convert.ToInt32(data["id"]);
                        hunterAddress.hunterId = Convert.ToInt32(data["hunterId"]);
                        hunterAddress.addressId = Convert.ToInt32(data["addressId"]);
                        hunterAddress.typeId = Convert.ToInt32(data["typeId"]);
                        hunterAddresses.Add(hunterAddress);
                    }
                    return hunterAddresses;
                }
            }
        }
        public HunterAddressDM GetHunterAddress(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",id)
                };
                return ReadHunterAddress("GetHunterAddressById", parameters).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetHunterAddressId(int hunterId, int addressId, int typeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@AddressId",addressId),
                    new SqlParameter("@TypeId",typeId)
                };
                return ReadHunterAddress("GetHunterAddressId",
                    parameters).SingleOrDefault().id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void CreateHunterAddress(int hunterId, int addressId, int typeId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@HunterId",hunterId),
                    new SqlParameter("@AddressId",addressId),
                    new SqlParameter("@TypeId",typeId)
                };
            dao.Write("CreateHunterAddress", parameters);
        }
        public bool DoesHunterAddressExist(int id)
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
        public void UpdateHunterAddressDB(int id, string lastName, string firstName, string email,
            string street, string city, string state, string zip, string addressType)
        {
            DAO dao = new DAO();
            HunterDAO pdao = new HunterDAO();
            AddressDAO adao = new AddressDAO();
            TypeDAO tdao = new TypeDAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id),
                new SqlParameter("@HunterId", pdao.GetHunterId(lastName, firstName, email)),
                new SqlParameter("@AddressId", adao.GetAddressId(street, city, state, zip)),
                new SqlParameter("@TypeId", tdao.GetTypeId(addressType))
            };
            dao.Write("UpdateHunterAddress", parameters);
        }
    }
}