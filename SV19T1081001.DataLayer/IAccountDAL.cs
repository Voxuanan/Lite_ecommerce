using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer
{
    public interface IAccountDAL
    {
        Account LogIn(string email, string passwor);
        bool ChangePassword(string email, string oldPassword, string newPassword);
    }
}
