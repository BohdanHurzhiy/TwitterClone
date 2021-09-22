using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.ASP.Services.ServiceInterface;
using TwitterClone.ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace TwitterClone.ASP.Services
{
    public class TagService : ITagService
    {
        private DbTwitterCloneContex _dbTwitterContex;
        public TagService(DbTwitterCloneContex dbTwitterContex)
        {
            _dbTwitterContex = dbTwitterContex;
        }
        public ICollection<Post> GetPostsByTag(string tagText)
        {
            var tag = _dbTwitterContex.Tags
                .Where(t => t.TagsText == tagText)
                .Include(t => t.Posts)
                .ThenInclude(p => p.User)
                .FirstOrDefault();
            if (tag == null)
            {
                throw new NullReferenceException();
            }
            return tag.Posts;            
        }
    }
}
