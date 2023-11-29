using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomDemo.DAL;

namespace EcomDemo.BAL
{
    public class AdminLoginBAL
    {
        public bool AdminLogin(AdminViewModel avm)
        {
            AdminLoginDAL ald = new AdminLoginDAL();
            bool result = ald.AdminLogin(avm);
            return result;
        }
       
        public bool ChangePassword(ChangePassword cp)
        {
            bool result;
            AdminLoginDAL ald = new AdminLoginDAL();
            result = ald.ChangePassword(cp);
            return result;
        }

        public bool GetUserNameFP(ForgotPassword fp)
        {
            AdminLoginDAL ald = new AdminLoginDAL();
            bool result = ald.GetUserNameFP(fp);
            return result;
        }
    }
}
