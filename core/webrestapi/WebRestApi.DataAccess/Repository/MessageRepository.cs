using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.DataAccess.Repository
{
    public class MessageRepository : MessageRepositoryBase
    {
        public MessageRepository(AbstractDbContext context) : base(context) { }

        public override async Task<Message> AddAsync(Message message)
        {
            var newMessage = await Context.Messages.AddAsync(message);
            await Context.SaveChangesAsync();

            return newMessage.Entity;
        }

        public override Task<Message> DeleteAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await Context.Messages.ToListAsync();
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
