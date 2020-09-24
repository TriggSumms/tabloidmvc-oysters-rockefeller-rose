using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        List<Comment> GetAllPostComments(int postId);
        Comment GetCommentById(int id);
        Comment GetUserCommentById(int id, int userProfileId);
    }
}