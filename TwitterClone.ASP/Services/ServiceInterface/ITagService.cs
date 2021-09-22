using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.ASP.Models;

namespace TwitterClone.ASP.Services.ServiceInterface
{
    public interface ITagService
    {
        ICollection<Post> GetPostsByTag(string tagText);
    }
}
