using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 1591
namespace WebRestApi.Models
{
    public class User : IEquatable<User>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Message> SendMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            return other != null &&
                   Id == other.Id &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   EqualityComparer<ICollection<Message>>.Default.Equals(SendMessages, other.SendMessages) &&
                   EqualityComparer<ICollection<Message>>.Default.Equals(ReceivedMessages, other.ReceivedMessages);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, SendMessages, ReceivedMessages);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}