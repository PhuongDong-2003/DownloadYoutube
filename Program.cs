using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using DownloadYoutube.Service;

class Program
{

    static async Task Main(string[] args)
    {
        FindService findService = new FindService();
        UserChoosesService userChoosesService = new UserChoosesService();
        DownLoadYoutubeService downLoadYoutubeService = new DownLoadYoutubeService();

        var youtube = new YoutubeClient();

        while (true)
        {
            var searchResults = await findService.Find();

            if (searchResults is not null)
            {
                var selectedVideo = await userChoosesService.LoadChooses(searchResults);

                if (selectedVideo == null)
                {
                    break;
                }
                await downLoadYoutubeService.DownloadVideoAsync(youtube, selectedVideo);
            }
            else
            {
                Console.WriteLine("Không tìm thấy video nào. Vui lòng thử lại.");
            }
        }


    }

    // Console.WriteLine("Chương trình đã kết thúc.");
    //     var youtube = new YoutubeClient();

    //     while (true)
    //     {
    //         Console.Write("Nhập từ khóa tìm kiếm: ");
    //         string searchQuery = Console.ReadLine();

    //         // Thực hiện tìm kiếm
    //         var searchResults = await youtube.Search.GetVideosAsync(searchQuery);

    //         if (searchResults.Any())
    //         {
    //             Console.WriteLine("Danh sách video có liên quan:");


    //             for (int i = 0; i < searchResults.Count(); i++)
    //             {
    //                 Console.WriteLine($"{i + 1}. {searchResults[i].Title}");
    //             }

    //             Console.Write("Nhập số thứ tự video để tải về (hoặc 0 để kết thúc): ");
    //             int selectedVideoIndex;

    //             while (true)
    //             {
    //                 if (int.TryParse(Console.ReadLine(), out selectedVideoIndex) && selectedVideoIndex >= 0 && selectedVideoIndex <= searchResults.Count)
    //                 {
    //                     break;
    //                 }
    //                 else
    //                 {
    //                     Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
    //                 }
    //             }


    //             if (selectedVideoIndex == 0)
    //             {
    //                 break;
    //             }
    //              var selectedVideo = searchResults[selectedVideoIndex - 1];

    //             await DownloadVideoAsync(youtube, selectedVideo);
    //         }
    //         else
    //         {
    //             Console.WriteLine("Không tìm thấy video nào. Vui lòng thử lại.");
    //         }
    //     }

    //     Console.WriteLine("Chương trình đã kết thúc.");
    // static async Task DownloadVideoAsync(string videoUrl)
    // {
    //     var youtube = new YoutubeClient();
    //     var video = await youtube.Videos.GetAsync(videoUrl);
    //     var videoId = video.Id;

    //     var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(videoId);
    //     var audioStreamInfo = streamInfoSet.GetAudioOnlyStreams().GetWithHighestBitrate();

    //     var videoStreamInfo = streamInfoSet
    //         .GetVideoOnlyStreams()
    //         .Where(s => s.Container == Container.Mp4)
    //         .GetWithHighestVideoQuality();

    //     var videoExt = videoStreamInfo.Container.Name;
    //     var audioStream = await youtube.Videos.Streams.GetAsync(audioStreamInfo);
    //     var videoStream = await youtube.Videos.Streams.GetAsync(videoStreamInfo);
    //     var muxedStream = streamInfoSet.GetMuxedStreams().GetWithHighestVideoQuality();

    //     await youtube.Videos.Streams.DownloadAsync(muxedStream, $"video.{muxedStream.Container}");
    //     var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{video.Title}.{videoExt}");

    //     //  Console.WriteLine($"Đang tải video và âm thanh...");

    //     //    using (var fileStream = File.OpenWrite(outputFilePath))
    //     // {
    //     //     await audioStream.CopyToAsync(fileStream);
    //     //     await videoStream.CopyToAsync(fileStream);
    //     // }

    //     // Console.WriteLine($"Video và âm thanh đã được tải về: {outputFilePath}");

    //     // Ghi thông tin về video vào một file text
    //     // var infoFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{video.Title}_info.txt");
    //     // WriteVideoInfoToFile(video, infoFilePath);
    //     // Console.WriteLine($"Thông tin về video đã được lưu vào: {infoFilePath}");

    // }


}