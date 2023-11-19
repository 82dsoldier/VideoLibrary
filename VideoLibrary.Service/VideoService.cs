using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoLibrary.Data.Entities;
using VideoLibrary.Data.Interfaces;
using VideoLibrary.Service.Interfaces;

namespace VideoLibrary.Service {
    public class VideoService : IVideoService {
        private static Regex _thumbnailNameParser = new Regex(@".*(\d\d\d).png", RegexOptions.Compiled);

        private readonly IVideoRepo _videoRepo;
        private readonly IConfiguration _configuration;

        public VideoService(IVideoRepo videoRepo, IConfiguration configuration) {
            _videoRepo = videoRepo;
            _configuration = configuration;
        }
        public IEnumerable<Video> GetPaged(int start, int count)
            => _videoRepo.GetPaged(start, count);

        public IEnumerable<Video> GetPagedSearch(string keywords, int start, int count)
            => _videoRepo.GetPagedSearch(keywords, start, count);

        public Video Get(int videoId)
            => _videoRepo.Get(videoId);

        public int GetVideoCount()
            => _videoRepo.GetVideoCount();

        public int GetSearchVideoCount(string keywords)
            => _videoRepo.GetSearchVideoCount(keywords);

        public void Update(Video video)
            => _videoRepo.Update(video);

        public int Insert(Video video)
            => _videoRepo.Insert(video);

        public int UploadFile(string uploadedFile, string basePath) {
            basePath = basePath.Replace("api", string.Empty);
            var videoId = 0;
            try {
                var thumbnailName = Path.Combine(basePath, _configuration.GetValue<string>("ThumbnailPath").Trim('/'), $"{uploadedFile.Replace(Path.GetExtension(uploadedFile), string.Empty)}%03d.png");
                var thumbnailPath = Path.GetDirectoryName(thumbnailName);
                if (!Path.Exists(thumbnailPath)) {
                    Directory.CreateDirectory(thumbnailPath);
                }
                var videoFileName = Path.Combine(basePath, _configuration.GetValue<string>("MediaPath").Trim('/'), uploadedFile);
                var videoPath = Path.GetDirectoryName(videoFileName);
                if (!Path.Exists(videoPath)) {
                    Directory.CreateDirectory(videoPath);
                }

                var psi = new ProcessStartInfo(_configuration.GetValue<string>("FFMPEGPath")) {
                    Arguments = $@"-i ""{videoFileName}"" -s 320x240 -vf fps=1/60 ""{thumbnailName}""",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow= true,
                    UseShellExecute = false,                    
                };
                var proc = new Process { StartInfo = psi };
                proc.Start();
                //var output = new StreamWriter(@"C:\temp\output.log");
                //while (!proc.StandardOutput.EndOfStream) {
                //    output.WriteLine(proc.StandardOutput.ReadLine());
                //}
                //while (!proc.StandardError.EndOfStream) {
                //    output.WriteLine(proc.StandardError.ReadLine());
                //}
                //output.Close();
                proc.WaitForExit();
                thumbnailPath = Path.Combine(basePath, _configuration.GetValue<string>("ThumbnailPath").Trim('/'));
                thumbnailName = $"{uploadedFile.Replace(Path.GetExtension(uploadedFile), string.Empty)}???.png";
                var files = Directory.GetFiles(thumbnailPath, thumbnailName);
                if (files.Length > 10) {
                    foreach (var file in files) {
                        var fileNumberMatch = _thumbnailNameParser.Match(file);
                        if (fileNumberMatch.Success) {
                            _ = int.TryParse(fileNumberMatch.Groups[1].Value, out var fileNumber);
                            if (fileNumber > 10) {
                                System.IO.File.Delete(file);
                            }
                        }
                    }
                }
                var video = new Video {
                    ThumbnailPath = $"{_configuration.GetValue<string>("ThumbnailPath")}/{Path.GetFileName(files[0])}",
                    VideoName = uploadedFile.Replace(Path.GetExtension(uploadedFile), string.Empty),
                    VideoPath = $"{_configuration.GetValue<string>("MediaPath")}/{uploadedFile}"
                };

                videoId = Insert(video);
                return videoId;
            } catch(Exception e) {
                File.WriteAllText(@"C:\temp\error.log", $"{e.Message}\r\n\r\n{e.StackTrace}");
            }
            return videoId;
        }
    }
}
