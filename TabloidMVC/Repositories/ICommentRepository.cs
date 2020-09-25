using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(int id);
        List<Comment> GetAllPostComments(int postId);
        Comment GetCommentById(int id);
    }
}