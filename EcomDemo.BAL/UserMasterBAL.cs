using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomDemo.DAL;
using EcomDemo.Model;

namespace EcomDemo.BAL
{
    public class UserMasterBAL
    {
        public AddEditUserModel UserCreate(AddEditUserModel aeum)
        {
            UserMasterDAL umd = new UserMasterDAL();
            aeum = umd.UserCreate(aeum);
            return aeum;
        }

        public AddEditUserModel UserGet(AddEditUserModel aeum)
        {
            UserMasterDAL umd = new UserMasterDAL();
            aeum = umd.UserGet(aeum);
            return aeum;
        }

        public AddEditUserModel UserEditDone(int ID)
        {
            AddEditUserModel aeum = new AddEditUserModel();
            UserMasterDAL umd = new UserMasterDAL();
            aeum = umd.UserEditDone(ID);
            return aeum;
        }

        public AddEditUserModel UserEditDone(AddEditUserModel aeum)
        {
            UserMasterDAL umd = new UserMasterDAL();
            aeum = umd.UserEditDone(aeum);
            return aeum;
        }

        public AddEditUserModel DeleteUser(int ID)
        {
            AddEditUserModel aeum = new AddEditUserModel();
            UserMasterDAL umd = new UserMasterDAL();
            aeum = umd.DeleteUser(ID);
            return aeum;
        }

        public ContactWithUs GetContactUsLists(ContactWithUs cwu)
        {
            UserMasterDAL umd = new UserMasterDAL();
            cwu = umd.GetContactUsLists(cwu);
            return cwu;
        }

        public ContactWithUs ViewMessage(ContactWithUs cwu)
        {
            UserMasterDAL umd = new UserMasterDAL();
            cwu = umd.ViewMessage(cwu);
            return cwu;
        }

        public ContactWithUs DeleteMessage(ContactWithUs cwu)
        {
            UserMasterDAL umd = new UserMasterDAL();
            cwu = umd.DeleteMessage(cwu);
            return cwu;
        }
    }
}
