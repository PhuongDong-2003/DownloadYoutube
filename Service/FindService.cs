using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DownloadYoutube.Interface;
using YoutubeExplode;
using YoutubeExplode.Search;
using YoutubeExplode.Common;

namespace DownloadYoutube.Service
{
    public class FindService : IFind
    {
        public async Task<List<VideoSearchResult>> Find()
        {
            var youtube = new YoutubeClient();
            Console.Write("Nhập từ khóa tìm kiếm: ");
            string searchQuery = Console.ReadLine();
            return (await youtube.Search.GetVideosAsync(searchQuery)).ToList();

        }

    }


}
