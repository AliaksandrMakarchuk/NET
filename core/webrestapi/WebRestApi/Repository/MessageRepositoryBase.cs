
#pragma warning disable 1591
using WebRestApi.Models;

namespace WebRestApi.Repository
{
    public abstract class MessageRepositoryBase : RepositoryBase<Message, WebRestApiContext>
    {
        protected MessageRepositoryBase(WebRestApiContext context) : base(context) { }
    }
}
