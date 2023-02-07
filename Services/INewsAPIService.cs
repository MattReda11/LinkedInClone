using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Services
{
    public interface INewsAPIService
    {
        Task<List<NewsModel>> GetHeadlines();
    }
}