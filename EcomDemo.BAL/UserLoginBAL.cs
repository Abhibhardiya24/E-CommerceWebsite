using EcomDemo.DAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EcomDemo.BAL
{
    public class UserLoginBAL
    {
        public bool UserLogin(UserLogin ul)
        {
            bool result;
            UserLoginDAL uld = new UserLoginDAL();
            result = uld.UserLogin(ul);
            return result;
        }

        public bool CreateAccount(UserLogin ul)
        {
            bool result;
            UserLoginDAL uld = new UserLoginDAL();
            result = uld.CreateAccount(ul);
            return result;
        }

        public bool ChangePassword(ChangePassword cp)
        {
            bool result;
            UserLoginDAL uld = new UserLoginDAL();
            result = uld.ChangePassword(cp);
            return result;
        }

        public bool UserForgotPassword(ForgotPassword fp)
        {
            bool result;
            UserLoginDAL uld= new UserLoginDAL();   
            result = uld.UserForgotPassword(fp);
            return result;
        }
    }
}
