using WebRestApi.Service.Models;

namespace WebRestApi.Service.Repository
{
    public abstract class CommentRepositoryBase : RepositoryBase<Comment>
    {
        protected CommentRepositoryBase(AbstractDbContext context) : base(context) { }
    }
}
