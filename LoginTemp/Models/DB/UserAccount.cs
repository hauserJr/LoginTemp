
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;

namespace DB
{
    public partial class UserAccount : DBRepo
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
    }
}
