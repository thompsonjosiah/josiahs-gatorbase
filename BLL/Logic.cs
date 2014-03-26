using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Logic
    {
       
        public HunterInfoVM ConvertHunterInfoDmToVm(HunterInfoDM hunterInfo)
        {
            HunterInfoVM hunterInfoVm = new HunterInfoVM();
            if (hunterInfo != null)
            {
                hunterInfoVm.id = hunterInfo.id;
                hunterInfoVm.lastName = GetHunter(GetHunterAddress(hunterInfo.hunterAddressId).hunterId).lastName;
                hunterInfoVm.firstName = GetHunter(GetHunterAddress(hunterInfo.hunterAddressId).hunterId).firstName;
                hunterInfoVm.email = GetHunter(GetHunterAddress(hunterInfo.hunterAddressId).hunterId).email;
                hunterInfoVm.street = GetAddress(GetHunterAddress(hunterInfo.hunterAddressId).addressId).street;
                hunterInfoVm.city = GetAddress(GetHunterAddress(hunterInfo.hunterAddressId).addressId).city;
                hunterInfoVm.state = GetAddress(GetHunterAddress(hunterInfo.hunterAddressId).addressId).state;
                hunterInfoVm.zip = GetAddress(GetHunterAddress(hunterInfo.hunterAddressId).addressId).zip;
                hunterInfoVm.addressType = GetType(GetHunterAddress(hunterInfo.hunterAddressId).typeId);
                hunterInfoVm.number = GetPhone(GetHunterPhone(hunterInfo.hunterPhoneId).phoneId);
                hunterInfoVm.phoneType = GetType(GetHunterPhone(hunterInfo.hunterPhoneId).typeId);
            }
            return hunterInfoVm;
        }
      
        public HunterInfoDM GetHunterInfo(int id)
        {
            HunterInfoDAO dao = new HunterInfoDAO();
            return dao.GetHunterInfoById(id).Single();
        }
        public string GetType(int id)
        {
            TypeDAO dao = new TypeDAO();
            return dao.GetType(id);
        }
        public string GetPhone(int id)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.GetPhone(id);
        }
        public AddressDM GetAddress(int id)
        {
            AddressDAO dao = new AddressDAO();
            return dao.GetAddress(id);
        }
        public HunterDM GetHunter(int id)
        {
            HunterDAO dao = new HunterDAO();
            return dao.GetHunter(id);
        }
        public HunterPhoneDM GetHunterPhone(int id)
        {
            HunterPhoneDAO dao = new HunterPhoneDAO();
            return dao.GetHunterPhone(id);
        }
        public HunterAddressDM GetHunterAddress(int id)
        {
            HunterAddressDAO dao = new HunterAddressDAO();
            return dao.GetHunterAddress(id);
        }
       
        public HunterInfoDM ConvertHunterInfoVmToDm(HunterInfoVM hunterInfoVm)
        {
            HunterInfoDM hunterInfoDm = new HunterInfoDM();
            if (hunterInfoVm != null)
            {
                SetHunterAddressId(hunterInfoVm, hunterInfoDm);
                SetHunterPhoneId(hunterInfoVm, hunterInfoDm);
            }
            return hunterInfoDm;
        }
       
        private void SetHunterPhoneId(HunterInfoVM hunterInfoVm, HunterInfoDM hunterInfoDm)
        {
            bool w = DoesHunterPhoneExist(GetHunterPhoneId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
            SetPhoneId(hunterInfoVm.number), SetTypeId(hunterInfoVm.phoneType)));
            if (w == false)
            {
                HunterPhoneDM phone = CreateHunterPhone(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email,
                    hunterInfoVm.number, hunterInfoVm.phoneType);
                hunterInfoDm.hunterPhoneId = GetHunterPhoneId(phone.hunterId, phone.phoneId, phone.typeId);
            }
            else
            {
                hunterInfoDm.hunterPhoneId = GetHunterPhoneId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                        SetPhoneId(hunterInfoVm.number), SetTypeId(hunterInfoVm.phoneType));
            }
        }
    
        public int GetHunterInfoId(int hunterAddressId, int hunterPhoneId)
        {
            HunterInfoDAO dao = new HunterInfoDAO();
            HunterInfoDM hunterInfo = new HunterInfoDM();
            hunterInfo = dao.GetHunterInfo(hunterAddressId, hunterPhoneId);
            if (hunterInfo == null)
            {
                CreateHunterInfo(hunterAddressId, hunterPhoneId);
                hunterInfo.id = dao.GetHunterInfo(hunterAddressId, hunterPhoneId).id;
                 hunterInfo.hunterAddressId = dao.GetHunterInfo(hunterAddressId, hunterPhoneId).hunterAddressId;
                 hunterInfo.hunterPhoneId = dao.GetHunterInfo(hunterAddressId, hunterPhoneId).hunterPhoneId;
            }
            return hunterInfo.id;
        }
        public bool DoesHunterInfoExist(int hunterInfoId)
        {
            HunterInfoDAO dao = new HunterInfoDAO();
            return dao.DoesHunterInfoExist(hunterInfoId);
        }
        public int GetPhoneId(string number)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.GetPhoneId(number);
        }
        private int SetPhoneId(string number)
        {
            int y = 0;
            bool x = DoesPhoneExist(GetPhoneId(number));
            if (x == false)
            {
                CreatePhone(number);
                y = GetPhoneId(number);
            }
            else
            {
                y = GetPhoneId(number);
            }
            return y;
        }
        public bool DoesHunterPhoneExist(int id)
        {
            HunterPhoneDAO dao = new HunterPhoneDAO();
            return dao.DoesHunterPhoneExist(id);
        }
        private int SetHunterAddressId(HunterInfoVM hunterInfoVm, HunterInfoDM hunterInfoDm)
        {
            bool w = DoesHunterAddressExist(GetHunterAddressId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                SetAddressId(hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip), SetTypeId(hunterInfoVm.addressType)));
            if (w == false)
            {
                HunterAddressDM address = CreateHunterAddress(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email,
                    hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip, hunterInfoVm.addressType);
                hunterInfoDm.hunterAddressId = GetHunterAddressId(address.hunterId, address.addressId, address.typeId);
            }
            else
            {
                hunterInfoDm.hunterAddressId = GetHunterAddressId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                        SetAddressId(hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip), SetTypeId(hunterInfoVm.addressType));
            }
            return hunterInfoDm.hunterAddressId;
        }
         public int SetHunterInfoDmHunterAddressId(HunterInfoVM hunterInfoVm, HunterInfoDM hunterInfoDm)
         {
             bool w = DoesHunterAddressExist(GetHunterAddressId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                 SetAddressId(hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip), SetTypeId(hunterInfoVm.addressType)));
             if (w == false)
             {
                 HunterAddressDM address = CreateHunterAddress(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email,
                     hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip, hunterInfoVm.addressType);
                 hunterInfoDm.hunterAddressId = GetHunterAddressId(address.hunterId, address.addressId, address.typeId);
             }
             else
             {
                 hunterInfoDm.hunterAddressId = GetHunterAddressId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                         SetAddressId(hunterInfoVm.street, hunterInfoVm.city, hunterInfoVm.state, hunterInfoVm.zip), SetTypeId(hunterInfoVm.addressType));
             }
             return hunterInfoDm.hunterAddressId;
         }
         public int SetHunterInfoDmHunterPhoneId(HunterInfoVM hunterInfoVm, HunterInfoDM hunterInfoDm)
         {
             bool w = DoesHunterPhoneExist(GetHunterPhoneId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                 SetPhoneId(hunterInfoVm.number), SetTypeId(hunterInfoVm.phoneType)));
             if (w == false)
             {
                 HunterPhoneDM phone = CreateHunterPhone(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email,
                     hunterInfoVm.number,  hunterInfoVm.phoneType);
                 hunterInfoDm.hunterPhoneId = GetHunterPhoneId(phone.hunterId, phone.phoneId, phone.typeId);
             }
             else
             {
                 hunterInfoDm.hunterPhoneId = GetHunterPhoneId(SetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email),
                         SetPhoneId(hunterInfoVm.number), SetTypeId(hunterInfoVm.phoneType));
             }
             return hunterInfoDm.hunterAddressId;
         }
        public int GetTypeId(string type)
        {
            TypeDAO dao = new TypeDAO();
            return dao.GetTypeId(type);
        }
        private int SetTypeId(string type)
        {
            int x = 0;
            bool z = DoesTypeExist(GetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                x = GetTypeId(type);
            }
            else
            {
                x = GetTypeId(type);
            }
            return x;
        }
        public int GetAddressId(string street, string city, string state, string zip)
        {
            AddressDAO dao = new AddressDAO();
            return dao.GetAddressId(street, city, state, zip);
        }
        private int SetAddressId(string street, string city, string state, string zip)
        {
            int x = 0;
            bool y = DoesAddressExist(GetAddressId(street, city, state, zip));
            if (y == false)
            {
                CreateAddress(street, city, state, zip);
                x = GetAddressId(street, city, state, zip);
            }
            else
            {
                x = GetAddressId(street, city, state, zip);
            }
            return x;
        }
        public int GetHunterId(string lastName, string firstName, string email)
        {
            HunterDAO dao = new HunterDAO();
          int  x = dao.GetHunterId(lastName, firstName, email);
          int u = x;
          return x;
        }
        private int SetHunterId(string lastName, string firstName, string email)
        {
            int y = 0;
            bool x = DoesHunterExist(GetHunterId(lastName, firstName, email));
            if (x == false)
            {
               CreateHunter(lastName, firstName, email);
               y = GetHunterId(lastName, firstName, email);
            }
            else
            {
                y = GetHunterId(lastName, firstName, email);
            }
            return y;
       }
        public bool DoesHunterAddressExist(int id)
        {
            HunterAddressDAO dao = new HunterAddressDAO();
            return dao.DoesHunterAddressExist(id);
        }
        private static bool DoesHunterExist(HunterInfoVM hunterInfoVm, HunterDAO hunterDao)
        {
            bool a = hunterDao.DoesHunterExist(hunterDao.GetHunterId(hunterInfoVm.lastName, hunterInfoVm.firstName, hunterInfoVm.email));
            return a;
        }
       
        public HunterDM CreateHunter(string lastName, string firstName, string email)
        {
            HunterDM hunter = new HunterDM();
            hunter.lastName = lastName;
            hunter.firstName = firstName;
            hunter.email = email;
            HunterDAO dao = new HunterDAO();
            dao.CreateHunter(lastName, firstName, email);
            return hunter;
        }
        public void CreateHunterInfo(int hunterAddressId, int hunterPhoneId)
        {
            HunterInfoDAO dao = new HunterInfoDAO();
            dao.CreateHunterInfo(hunterAddressId, hunterPhoneId);
        }
      
        public HunterInfoVM CreateHunterInfoVM(string lastName, string firstName, string email,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            HunterInfoVM hunterInfo = new HunterInfoVM();
            hunterInfo.lastName = lastName;
            hunterInfo.firstName = firstName;
            hunterInfo.email = email;
            hunterInfo.street = street;
            hunterInfo.city = city;
            hunterInfo.state = state;
            hunterInfo.zip = zip;
            hunterInfo.addressType = addressType;
            hunterInfo.number = number;
            hunterInfo.phoneType = phoneType;
            HunterInfoDM hunterInfoDm = ConvertHunterInfoVmToDm(hunterInfo);
            CreateHunterInfo(hunterInfoDm.hunterAddressId, hunterInfoDm.hunterPhoneId);
            return hunterInfo;
        }
        public TypeDM CreateType(string type)
        {
            TypeDM typeDm = new TypeDM();
            TypeDAO dao = new TypeDAO();
            typeDm.type = type;
            dao.CreateType(type);
            return typeDm;
        }
        public PhoneDM CreatePhone(string number)
        {
            PhoneDM phone = new PhoneDM();
            PhoneDAO dao = new PhoneDAO();
            phone.number = number;
            dao.CreatePhone(number);
            return phone;
        }
        public AddressDM CreateAddress(string street, string city, string state, string zip)
        {
            AddressDM address = new AddressDM();
            AddressDAO dao = new AddressDAO();
            address.street = street;
            address.city = city;
            address.state = state;
            address.zip = zip;
            dao.CreateAddress(street, city, state, zip);
            return address;
        }
        public HunterPhoneDM CreateHunterPhone(string lastName, string firstName, string email, string number, string type)
        {
            HunterPhoneDAO dao = new HunterPhoneDAO();
            HunterPhoneDM hunterPhone = new HunterPhoneDM();
            SetHunterPhoneDmHunterId(lastName, firstName, email, hunterPhone);
            SetHunterPhoneDmPhoneId(number, hunterPhone);
            SetHunterPhoneDmTypeId(type, hunterPhone);
            dao.CreateHunterPhone(hunterPhone.hunterId, hunterPhone.phoneId, hunterPhone.typeId);
            return hunterPhone;
        }

        private void SetHunterPhoneDmHunterId(string lastName, string firstName, string email, HunterPhoneDM hunterPhone)
        {
            bool x = DoesHunterExist(SetHunterId(lastName, firstName, email));
            if (x == false)
            {
                CreateHunter(lastName, firstName, email);
                hunterPhone.hunterId = SetHunterId(lastName, firstName, email);
            }
            else
            {
                hunterPhone.hunterId = SetHunterId(lastName, firstName, email);
            }
        }

        private void SetHunterPhoneDmPhoneId(string number, HunterPhoneDM hunterPhone)
        {
            bool y = DoesPhoneExist(SetPhoneId(number));
            if (y == false)
            {
                CreatePhone(number);
                hunterPhone.phoneId = SetPhoneId(number);
            }
            else
            {
                hunterPhone.phoneId = SetPhoneId(number);
            }
        }

        private void SetHunterPhoneDmTypeId(string type, HunterPhoneDM hunterPhone)
        {
            bool z = DoesTypeExist(SetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                hunterPhone.typeId = SetTypeId(type);
            }
            else
            {
                hunterPhone.typeId = SetTypeId(type);
            }
        }
        public bool DoesHunterExist(int id)
        {
            HunterDAO dao = new HunterDAO();
            return dao.DoesHunterExist(id);
        }
        public bool DoesTypeExist(int id)
        {
            TypeDAO dao = new TypeDAO();
            return dao.DoesTypeExist(id);
        }
        public bool DoesPhoneExist(int id)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.DoesPhoneExist(id);
        }
       
        public bool DoesAddressExist(int id)
        {
            AddressDAO dao = new AddressDAO();
            return dao.DoesAddressExist(id);
        }
        public int GetHunterAddressId(int hunterId, int addressId, int typeId)
        {
            HunterAddressDAO dao = new HunterAddressDAO();
            return dao.GetHunterAddressId(hunterId, addressId, typeId);
        }
        public int GetHunterPhoneId(int hunterId, int phoneId, int typeId)
        {
            HunterPhoneDAO dao = new HunterPhoneDAO();
            return dao.GetHunterPhoneId(hunterId, phoneId, typeId);
        }
        public HunterAddressDM CreateHunterAddress(string lastName, string firstName, string email,
            string street, string city, string state, string zip, string type)
        {
            HunterAddressDAO dao = new HunterAddressDAO();
            HunterAddressDM hunterAddress = new HunterAddressDM();
            SetHunterAddressDmHunterId(lastName, firstName, email, hunterAddress);
            SetHunterAddressDmAddressId(street, city, state, zip, hunterAddress);
            SetHunterAddressDmTypeId(type, hunterAddress);
            dao.CreateHunterAddress(hunterAddress.hunterId, hunterAddress.addressId, hunterAddress.typeId);
            return hunterAddress;
        }
        public void SetHunterAddressDmHunterId(string lastName, string firstName, string email, HunterAddressDM hunterAddress)
        {
            bool x = DoesHunterExist(SetHunterId(lastName, firstName, email));
            if (x == false)
            {
                CreateHunter(lastName, firstName, email);
                hunterAddress.hunterId = SetHunterId(lastName, firstName, email);
            }
            else
            {
                hunterAddress.hunterId = SetHunterId(lastName, firstName, email);
            }
        }
        public void SetHunterAddressDmAddressId(string street, string city, string state, string zip, HunterAddressDM hunterAddress)
        {
            bool y = DoesAddressExist(SetAddressId(street, city, state, zip));
            if (y == false)
            {
                CreateAddress(street, city, state, zip);
                hunterAddress.addressId = SetAddressId(street, city, state, zip);
            }
            else
            {
                hunterAddress.addressId = SetAddressId(street, city, state, zip);
            }
        }
        public void SetHunterAddressDmTypeId(string type, HunterAddressDM hunterAddress)
        {
            bool z = DoesTypeExist(SetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                hunterAddress.typeId = SetTypeId(type);
            }
            else
            {
                hunterAddress.typeId = SetTypeId(type);
            }
        }
      
        public void DeleteHunterInfo(int id)
        {
            HunterInfoDAO dao = new HunterInfoDAO();
            dao.DeleteHunterInfo(id);
        }
      
        public List<HunterInfoVM> GetAllHunterInfos()
        {
            List<HunterInfoVM> hunterInfoVmList = new List<HunterInfoVM>();
            List<HunterInfoDM> hunterInfoDmList = new List<HunterInfoDM>();
            HunterInfoDAO dao = new HunterInfoDAO();
            hunterInfoDmList = dao.GetAllHunterInfos();
            foreach (HunterInfoDM hunterInfoDm in hunterInfoDmList)
            {
                hunterInfoVmList.Add(ConvertHunterInfoDmToVm(hunterInfoDm));
            }
            return hunterInfoVmList;
        }
        public void UpdateHunterInfo(int id, string lastName, string firstName, string email,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            HunterInfoDM hunterInfo = new HunterInfoDM();
            HunterInfoDAO dao = new HunterInfoDAO();
            HunterAddressDM hunterAddress = new HunterAddressDM();
            HunterAddressDAO padao = new HunterAddressDAO();
            HunterPhoneDM hunterPhone = new HunterPhoneDM();
            HunterPhoneDAO ppdao = new HunterPhoneDAO();
            hunterInfo.hunterAddressId = SetHunterInfoDmHunterAddressId(lastName, firstName, email,
                street, city, state, zip, addressType);
            hunterInfo.hunterPhoneId = SetHunterInfoDmHunterPhoneId(lastName, firstName, email,
                number, phoneType);
            padao.UpdateHunterAddressDB(id, lastName, firstName, email,
                street, city, state, zip, addressType);
            ppdao.UpdateHunterPhoneDB(id, lastName, firstName, email, number, phoneType);
            dao.UpdateHunterInfoDB(id, hunterInfo.hunterAddressId, hunterInfo.hunterPhoneId);
        }
      
        public int SetHunterInfoId(string lastName, string firstName, string email,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            HunterInfoDM hunterInfo = new HunterInfoDM();
            HunterInfoDAO udao = new HunterInfoDAO();
            HunterAddressDAO adao = new HunterAddressDAO();
            HunterPhoneDAO pdao = new HunterPhoneDAO();
            int a = SetHunterId(lastName, firstName, email);
            int b = SetAddressId(street, city, state, zip);
            int c = SetTypeId(addressType);
            int d = SetPhoneId(number);
            int e = SetTypeId(phoneType);
            int f = SetHunterAddressId(a, b, c);
            int g = SetHunterPhoneId(a, d, e);
            bool x = adao.DoesHunterAddressExist(f);
            if (x == false)
            {
                adao.CreateHunterAddress(a, b, c);
                hunterInfo.hunterAddressId = f;
            }
            else
            {
                hunterInfo.hunterAddressId = f;
            }
            bool y = pdao.DoesHunterPhoneExist(g);
            if (y == false)
            {
                pdao.CreateHunterPhone(a, d, e);
                hunterInfo.hunterPhoneId = g;
            }
            else
            {
                hunterInfo.hunterPhoneId = g;
            }
            bool z = udao.DoesHunterInfoExist(GetHunterInfoId(hunterInfo.hunterAddressId, hunterInfo.hunterPhoneId));
            if (z == false)
            {
                CreateHunterInfo(hunterInfo.hunterAddressId, hunterInfo.hunterPhoneId);
                hunterInfo.id = udao.GetHunterInfo(hunterInfo.hunterAddressId, hunterInfo.hunterPhoneId).id;
            }
            else
            {
                udao.CreateHunterInfo(hunterInfo.hunterAddressId, hunterInfo.hunterPhoneId);
            }
            return hunterInfo.id;
        }
        public int SetHunterAddressId(int hunterId, int addressId, int typeId)
        {
            HunterAddressDAO dao = new HunterAddressDAO();
            int y = GetHunterAddressId(hunterId, addressId, typeId);
            bool x = dao.DoesHunterAddressExist(y);
            if (x == false)
            {
                dao.CreateHunterAddress(hunterId, addressId, typeId);
                y = GetHunterAddressId(hunterId, addressId, typeId);
            }
            return y;
        }
        public int SetHunterPhoneId(int hunterId, int phoneId, int typeId)
        {
            HunterPhoneDAO dao = new HunterPhoneDAO();
            int y = GetHunterPhoneId(hunterId, phoneId, typeId);
            bool x = dao.DoesHunterPhoneExist(y);
            if (x == false)
            {
                dao.CreateHunterPhone(hunterId, phoneId, typeId);
                y = GetHunterAddressId(hunterId, phoneId, typeId);
            }
            return y;
        }
        public int SetHunterInfoDmHunterPhoneId(string lastName, string firstName, string email,
            string number, string phoneType)
        {
            HunterInfoDM hunterInfo = new HunterInfoDM();
            HunterPhoneDAO dao = new HunterPhoneDAO();
            int a = SetHunterId(lastName, firstName, email);
            int b = SetPhoneId(number);
            int c = SetTypeId(phoneType);
            bool x = dao.DoesHunterPhoneExist(GetHunterPhoneId(a, b, c));
            if (x == false)
            {
                dao.CreateHunterPhone(a, b, c);
                hunterInfo.hunterPhoneId = GetHunterPhoneId(a, b, c);
            }
            else
            {
                hunterInfo.hunterPhoneId = GetHunterPhoneId(a, b, c);
            }
            return hunterInfo.hunterPhoneId;
        }

        public int SetHunterInfoDmHunterAddressId(string lastName, string firstName, string email,
            string street, string city, string state, string zip, string addressType)
        {
            HunterInfoDM hunterInfo = new HunterInfoDM();
            HunterAddressDAO dao = new HunterAddressDAO();
            int a = SetHunterId(lastName, firstName, email);
            int b = SetAddressId(street, city, state, zip);
            int c = SetTypeId(addressType);
            bool x = dao.DoesHunterAddressExist(GetHunterAddressId(a, b, c));
            if (x == false)
            {
                dao.CreateHunterAddress(a, b, c);
                hunterInfo.hunterAddressId = GetHunterAddressId(a, b, c);
            }
            else
            {
                hunterInfo.hunterAddressId = GetHunterAddressId(a, b, c);
            }
            return hunterInfo.hunterAddressId;
        }
       /*  public int SetHunterInfoDmHunterId(string lastName, string firstName, string email,
            string number, string phoneType)
        {
            HunterInfoDM hunterInfo = new HunterInfoDM();
            HunterAddressDAO dao = new HunterAddressDAO();
            int a = SetHunterId(lastName, firstName, email);
            int b = SetPhoneId(number);
            int c = SetTypeId(phoneType);
            bool x = dao.DoesHunterPhoneExist(GetHunterPhoneId(a, b, c));
            if (x == false)
            {
                dao.CreateHunterPhone(a, b, c);
                hunterInfo.hunterPhoneId = GetHunterPhoneId(a, b, c);
            }
            else
            {
                hunterInfo.hunterAddressId = GetHunterAddressId(a, b, c);
            }
            return hunterInfo.hunterAddressId;
        }*/
    }
}
    
