using Core;
using Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class MembershipSqlData : IMembershipData
    {
        private readonly ITShopDbContext dbContext;

        public MembershipSqlData(ITShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Membership> GetMemberships()
        {
            return dbContext.Memberships.ToList();
        }

        public Membership GetMembershipById(int id)
        {
            return dbContext.Memberships.SingleOrDefault(x => x.Id == id);
        }
    }
}
