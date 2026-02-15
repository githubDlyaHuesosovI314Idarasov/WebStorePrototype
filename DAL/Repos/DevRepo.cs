using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.Models;

namespace DAL.Repos
{
    public sealed class DevRepo<T> : BaseRepo<WebStoreDBContext, T> where T : Entity
    {
        public DevRepo(WebStoreDBContext context) : base(context)
        {
        }
    }
}
