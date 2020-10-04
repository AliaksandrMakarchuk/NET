using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRestApi.Service.Models
{
    public class Comment : IEquatable<Comment>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Comment);
        }

        public bool Equals(Comment other)
        {
            return other != null &&
                   Id == other.Id &&
                   Text == other.Text &&
                   MessageId == other.MessageId &&
                   EqualityComparer<Message>.Default.Equals(Message, other.Message);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text, MessageId, Message);
        }

        public static bool operator ==(Comment left, Comment right)
        {
            return EqualityComparer<Comment>.Default.Equals(left, right);
        }

        public static bool operator !=(Comment left, Comment right)
        {
            return !(left == right);
        }
    }
}
