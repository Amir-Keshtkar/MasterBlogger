using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.Domain.ArticleAgg;

namespace MB.Domain.CommentAgg {
    public class Comment {
        public enum Statuses {
            New = 0,
            Confirmed = 1,
            Canceled = 2,
        }
        public long Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public Statuses Status { get; private set; }
        public DateTime CreationDate { get; private set; }
        public long ArticleId { get; private set; }
        public Article Article { get; private set; }

        protected Comment () {

        }

        public Comment (string name, string email, string message, long articleId) {
            Name = name;
            Email = email;
            Message = message;
            ArticleId = articleId;
            CreationDate = DateTime.Now;
            Status = Statuses.New;
        }
    }
}
