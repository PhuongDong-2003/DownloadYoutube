using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Nhập URL video từ YouTube: ");
        string videoUrl = Console.ReadLine();
        await DownloadVideoAsync(videoUrl);
    }
    static async Task DownloadVideoAsync(string videoUrl)
    {
        var youtube = new YoutubeClient();

        var video = await youtube.Videos.GetAsync(videoUrl);
        var videoId = video.Id;

        var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(videoId);
        

        var audioStreamInfo = streamInfoSet.GetAudioOnlyStreams().GetWithHighestBitrate();

        
        var videoStreamInfo = streamInfoSet
            .GetVideoOnlyStreams()
            .Where(s => s.Container == Container.Mp4)
            .GetWithHighestVideoQuality();

        var videoExt = videoStreamInfo.Container.Name;

        var audioStream = await youtube.Videos.Streams.GetAsync(audioStreamInfo);
        var videoStream = await youtube.Videos.Streams.GetAsync(videoStreamInfo);

        var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{video.Title}.{videoExt}");

        Console.WriteLine($"Đang tải video và âm thanh...");

    //    using (var fileStream = File.OpenWrite(outputFilePath))
    // {
    //     await audioStream.CopyToAsync(fileStream);
    //     await videoStream.CopyToAsync(fileStream);
    // }
        await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, $"video.{videoStreamInfo.Container}");
        await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, $"audio.{audioStreamInfo.Container}");

        // Console.WriteLine($"Video và âm thanh đã được tải về: {outputFilePath}");

        // Ghi thông tin về video vào một file text
        // var infoFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{video.Title}_info.txt");
        // WriteVideoInfoToFile(video, infoFilePath);
        // Console.WriteLine($"Thông tin về video đã được lưu vào: {infoFilePath}");
    }

    // static void WriteVideoInfoToFile(Video video, string filePath)
    // {
    //     // Ghi thông tin chi tiết về video vào một file text
    //     using (var writer = new StreamWriter(filePath))
    //     {
    //         writer.WriteLine($"Tiêu đề: {video.Title}");
    //         writer.WriteLine($"Mô tả: {video.Description}");
    //         writer.WriteLine($"Thời lượng: {video.Duration}");
    //         writer.WriteLine($"Ngày đăng: {video.UploadDate}");

    //         // Bạn có thể thêm các thông tin khác tùy theo nhu cầu
    //     }
   // }







}