using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestApi.Service.Models
{
    public class Message : IEquatable<Message>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }

        public int SenderId { get; set; }
        [NotMapped]
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        [NotMapped]
        public User Receiver { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Message);
        }

        public bool Equals(Message other)
        {
            return other != null &&
                   Id == other.Id &&
                   Text == other.Text &&
                   EqualityComparer<int>.Default.Equals(SenderId, other.SenderId) &&
                   EqualityComparer<User>.Default.Equals(Sender, other.Sender) &&
                   EqualityComparer<int>.Default.Equals(ReceiverId, other.ReceiverId) &&
                   EqualityComparer<User>.Default.Equals(Receiver, other.Receiver) &&
                   EqualityComparer<ICollection<Comment>>.Default.Equals(Comments, other.Comments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text, SenderId, Sender, ReceiverId, Receiver, Comments);
        }

        public static bool operator ==(Message left, Message right)
        {
            return EqualityComparer<Message>.Default.Equals(left, right);
        }

        public static bool operator !=(Message left, Message right)
        {
            return !(left == right);
        }
    }
}
