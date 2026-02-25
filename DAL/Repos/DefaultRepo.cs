using DAL.EF;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DAL.Repos
{
    public sealed class DefaultRepo<T> : BaseRepo<WebStoreDBContext, T> where T : Entity
    {
        public DefaultRepo(WebStoreDBContext context) : base(context)
        {
        }
    }
}
