using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void Add(Tag tag);
        void Delete(int tagId);
        List<Tag> GetAll();
        Tag GetTagById(int id);
    }
}