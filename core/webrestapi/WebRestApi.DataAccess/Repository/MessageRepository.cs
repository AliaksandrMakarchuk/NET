using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.DataAccess.Repository
{
    public class MessageRepository : MessageRepositoryBase
    {
        public MessageRepository(WebRestApiContext context) : base(context) { }

        public override Task<Message> AddAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public override Task<Message> DeleteAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Message>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Message> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Message> UpdateAsync(Message model)
        {
            throw new NotImplementedException();
        }
    }
}
