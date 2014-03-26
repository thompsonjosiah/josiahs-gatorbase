using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class HunterInfoDAO
    {
        public List<HunterInfoDM> ReadHunterInfo(string statement,
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
                    List<HunterInfoDM> HunterInfos = new List<HunterInfoDM>();
                    while (data.Read())
                    {
                        HunterInfoDM HunterInfo = new HunterInfoDM();
                        HunterInfo.id = Convert.ToInt32(data["id"]);
                        HunterInfo.hunterAddressId = Convert.ToInt32(data["hunterAddressId"]);
                        HunterInfo.hunterPhoneId = Convert.ToInt32(data["hunterPhoneId"]);
                        HunterInfos.Add(HunterInfo);
                    }
                    return HunterInfos;
                }
            }
        }
        public void CreateHunterInfo(int hunterAddressId, int hunterPhoneId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HunterAddressId", hunterAddressId),
                new SqlParameter("@HunterPhoneId", hunterPhoneId)
            };
            dao.Write("CreateHunterInfo", parameters);
        }
        public void DeleteHunterInfo(int id)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            dao.Write("DeleteHunterInfoById", parameters);
        }
        public List<HunterInfoDM> GetAllHunterInfos()
        {
            List<HunterInfoDM> allHunterInfos = ReadHunterInfo("GetAllHunterInfos", null);
            if (allHunterInfos != null)
            {
                return allHunterInfos;
            }
            else
            {
                allHunterInfos.Take(0).ToList();
                return allHunterInfos;
            }
        }
        public List<HunterInfoDM> GetHunterInfoById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            return ReadHunterInfo("GetHunterInfoById", parameters);
        }
        public void UpdateHunterInfoDB(int id, int hunterAddressId, int hunterPhoneId)
        {
            DAO dao = new DAO();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@HunterAddressId", hunterAddressId),
                new SqlParameter("@HunterPhoneId", hunterPhoneId)
            };
            dao.Write("UpdateHunterInfo", parameters);
        }
        public HunterInfoDM GetHunterInfo(int hunterAddressId, int hunterPhoneId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HunterAddressId",hunterAddressId),
                new SqlParameter("@HunterPhoneId",hunterPhoneId)
            };
            HunterInfoDM hu = new HunterInfoDM();
            List<HunterInfoDM> hunterInfo = new List<HunterInfoDM>();
            hunterInfo = ReadHunterInfo("GetHunterInfo", parameters);
            foreach (HunterInfoDM hunterInfoDm in hunterInfo)
            {
                hu = hunterInfoDm;
            }
            return hu;
        }
        public bool DoesHunterInfoExist(int id)
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