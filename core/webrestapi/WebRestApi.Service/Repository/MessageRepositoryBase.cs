using WebRestApi.Service.Models;

namespace WebRestApi.Service.Repository
{
    public abstract class MessageRepositoryBase : RepositoryBase<Message>
    {
        protected MessageRepositoryBase(AbstractDbContext context) : base(context) { }
    }
}
