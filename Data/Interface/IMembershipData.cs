using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IMembershipData
    {
        Membership GetMembershipById(int id);
        List<Membership> GetMemberships();
    }
}
