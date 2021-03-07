using System.Collections.Generic;
using System.Linq;
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

        public override async Task<Message> DeleteAsync(Message message)
        {
            if (message != null)
            {
                Context.Messages.Remove(message);
                await Context.SaveChangesAsync();
            }

            return message;
        }

        public override async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await Context.Messages.ToListAsync();
        }

        public override async Task<Message> GetByIdAsync(int id)
        {
            return await Context.Messages.FindAsync(id);
        }

        public override async Task<Message> UpdateAsync(Message message)
        {
            if (message == null)
            {
                return null;
            }

            Context.Messages.Update(message);
            await Context.SaveChangesAsync();

            return message;
        }
    }
}