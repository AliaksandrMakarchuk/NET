using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Models;

namespace WebRestApi.Repository
{
    public class CommentRepository : CommentRepositoryBase
    {
        public CommentRepository(WebRestApiContext context) : base(context) { }

        public override Task<Comment> AddAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public override Task<Comment> DeleteAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Comment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Comment> UpdateAsync(Comment model)
        {
            throw new NotImplementedException();
        }
    }
}
