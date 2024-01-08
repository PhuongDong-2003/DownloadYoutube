using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadYoutube.Interface;
using YoutubeExplode.Search;

namespace DownloadYoutube.Service
{
    public class UserChoosesService : IUserChooses
    {
        public async Task<VideoSearchResult> LoadChooses(List<VideoSearchResult> searchResults)
        {
            Console.WriteLine("Danh sách video có liên quan:");

            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {searchResults[i].Title}");
            }

            Console.Write("Nhập số thứ tự video để tải về (hoặc 0 để kết thúc): ");
            int selectedVideoIndex;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out selectedVideoIndex) && selectedVideoIndex >= 0 && selectedVideoIndex <= searchResults.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                }
            }

            if (selectedVideoIndex == 0)
            {
                Console.WriteLine("Chương trình đã kết thúc.");
                return null;
            }

            return searchResults[selectedVideoIndex - 1];
        }
    }
}