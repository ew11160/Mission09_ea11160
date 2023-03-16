using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_ea11160.Models
{
    public interface ICheckOutRepository
    {
        // interface, place to build for rest of repo
        IQueryable<Checkout> Checkouts { get; }

        void SaveCheckout(Checkout checkout);
    }
}
