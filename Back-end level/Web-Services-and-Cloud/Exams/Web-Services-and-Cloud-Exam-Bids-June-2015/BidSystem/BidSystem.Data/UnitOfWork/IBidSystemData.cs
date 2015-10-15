namespace BidSystem.Data.UnitOfWork
{
    using Models;
    using Repositories;
    using Microsoft.AspNet.Identity;

    public interface IBidSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Offer> Offers { get; }

        IRepository<Bid> Bids { get; }

        IUserStore<User> UserStore { get; }

        void SaveChanges();
    }
}
