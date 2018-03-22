using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityServices
    {
        
        public interface IIdentityAction
        {
            SortedList<string, string> GetClaim(IIdentity identity);
        }
        public class IdentityService : IIdentityAction
        {
            //public IdentityService(Tkey _Tk, Tvalue _Tv)
            //{
            //}
            public SortedList<string, string> GetClaim(IIdentity identity)
            {
                SortedList<string, string> _list = new SortedList<string, string>();
                ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
                foreach (var item in claimsIdentity.Claims)
                {
                    _list.Add(item.Type,item.Value);
                }
                return _list;
            }
        }
    }
}
