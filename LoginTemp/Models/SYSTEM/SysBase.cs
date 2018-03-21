using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mSystem
{
    public static class SysBase
    {
        
        //cookieName
        private static string _cookieName = @"goodarc";
        //Home
        private static string _testConn1 = @"Data Source=DESKTOP-I8D8RT8\SQLEXPRESS;Initial Catalog=Core;Integrated Security=True";
        //Company 
        private static string _testConn2 = @"Data Source=DESKTOP-RNAENQ8\SQLEXPRESS;Initial Catalog=Core;Integrated Security=True";


        public static string cookieName
        {
            get { return _cookieName; }
            set { value = _cookieName; }
        }

        public static string testConn1
        {
            get { return _testConn1; }
            set { value = _testConn1; }
        }

        public static string testConn2
        {
            get { return _testConn2; }
            set { value = _testConn2; }
        }
    }
}
