// See https://aka.ms/new-console-template for more information
using BulkLoad;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using VideoLibrary.Data;
using VideoLibrary.Data.Entities;

var thumbnailNameParser = new Regex(@".*(\d\d\d).png", RegexOptions.Compiled);

var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("appsettings.json"));

if(settings == null) {
    throw new ArgumentNullException(nameof(settings));
}

var repo = new VideoRepo(new SqlConnection(settings.ConnectionString));

var videoFiles = Directory.GetFiles(settings.InputPath);

foreach(var videoFile in videoFiles) {
    var videoFileName = Path.GetFileName(videoFile);
    var thumbnailFileName = Path.Combine(settings.ThumbnailPath, $"{videoFileName.Replace(Path.GetExtension(videoFileName), string.Empty)}%03d.png");
    var videoDestination = Path.Combine(settings.OutputPath, videoFileName);

    Console.WriteLine($"Copying {videoFile} to {videoDestination}");

    File.Copy(videoFile, videoDestination, true);

    Console.WriteLine("Generating thumbnails");

    var psi = new ProcessStartInfo(settings.FFMPEGPath) {
        Arguments = $@"-i ""{videoFile}"" -s 320x240 -vf fps=1/60 ""{thumbnailFileName}""",
        CreateNoWindow = false,
    };
    var proc = Process.Start(psi);
    if(proc == null) {
        throw new Exception("An error occured while starting FFMPEG");
    }
    proc.WaitForExit();

    thumbnailFileName = $"{videoFileName.Replace(Path.GetExtension(videoFileName), string.Empty)}???.png";
    var thumbnailFiles = Directory.GetFiles(settings.ThumbnailPath, thumbnailFileName);

    if(thumbnailFiles.Length > 10) {
        foreach (var thumbnailFile in thumbnailFiles) {
            var match = thumbnailNameParser.Match(thumbnailFile);
            if (match.Success) {
                _ = int.TryParse(match.Groups[1].Value, out var thumbnailFileNameVal);
                if (thumbnailFileNameVal > 10) {
                    File.Delete(thumbnailFile);
                }
            }
        }
    }

    Console.WriteLine("Saving Video");

    var video = new Video {
        VideoName = videoFileName.Replace(Path.GetExtension(videoFileName), string.Empty),
        Rating = 3,
        ThumbnailPath = $"/images/{Path.GetFileName(thumbnailFiles[0])}",
        VideoPath = $"/media/{videoFileName}",
    };

    repo.Insert(video);
}