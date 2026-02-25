using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.Models;

namespace DAL.Repos
{
    public sealed class DevRepo<T> : BaseRepo<ExternalWebStoreDBContext, T> where T : Entity
    {
        public DevRepo(ExternalWebStoreDBContext context) : base(context)
        {
        }
    }
}
