
using DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountServices
    {     
        public interface IAccountAction
        {           
            bool LoginCheck(string act, string pwd);
            string GetHashPwd(string pwd);

        }

        public class AccountService: IAccountAction
        {
            private readonly CoreContext _db;

            public AccountService(CoreContext _db)
            {
                this._db = _db;
            }

            public bool LoginCheck(string account, string pwd)
            {

                var SaltPwd = GetHashPwd(pwd);
                var query = _db.UserAccount.Where(o => o.Account.Equals(account) && o.Pwd.Equals(SaltPwd));
                return query.Any();
            }

            public string GetHashPwd(string pwd)
            {
                byte[] BytePwa = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(pwd)));
                var SaltPwd = AddSaltToPwd(BytePwa);
                return SaltPwd;
            }

            private string AddSaltToPwd(Byte[] Pwd)
            {
                //位元組的隨機金鑰
                int Byte_cb = 8;

                //定義兩組SaltKey,必須大於8Byte
                byte[] SaltKey_1 = Encoding.UTF8.GetBytes("ABCDAAAA");
                byte[] SaltKey_2 = Encoding.UTF8.GetBytes("@ABCDABCD");

                //First Add Salt
                Rfc2898DeriveBytes Rfc_1 = new Rfc2898DeriveBytes(Pwd, SaltKey_1, 2);
                byte[] GetRfc_1 = Rfc_1.GetBytes(Byte_cb);
                var Salt_1Pwd = Convert.ToBase64String(GetRfc_1);

                //Second Add Salt
                Rfc2898DeriveBytes Rfc_2 = new Rfc2898DeriveBytes(Salt_1Pwd, SaltKey_2);
                byte[] GetRfc_2 = Rfc_2.GetBytes(Byte_cb);

                var Salt_2Pwd = Convert.ToBase64String(GetRfc_2);
                return Salt_2Pwd;
            }
        }
    }
}
