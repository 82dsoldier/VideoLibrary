using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary.Data.Entities;
using VideoLibrary.Data.Interfaces;

namespace VideoLibrary.Data {
    public class VideoRepo : IVideoRepo {
        private readonly IDbConnection _dbConnection;
        public VideoRepo(IDbConnection dbConnection) {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Video> GetAll()
            => _dbConnection.Query<Video>("[dbo].[GetVideo]", commandType: CommandType.StoredProcedure);

        public IEnumerable<Video> GetPaged(int start, int count)
            => _dbConnection.Query<Video>("[dbo].[GetPagedVideos]", new { @Start = start, @Count = count }, commandType: CommandType.StoredProcedure);

        public IEnumerable<Video> GetPagedSearch(string keywords, int start, int count)
            => _dbConnection.Query<Video>("[dbo].[GetPagedVideosSearch]", new { @Keywords = keywords, @Start = start, @Count = count }, commandType: CommandType.StoredProcedure);

        public Video Get(int videoId)
            => _dbConnection.QuerySingle<Video>("[dbo].[GetVideoById]", new { @VideoId = videoId }, commandType: CommandType.StoredProcedure);

        public int Insert(Video video) {
            var args = new DynamicParameters();
            var idParam = new SqlParameter("VideoId", SqlDbType.Int);
            args.Add("@VideoId", dbType: idParam.DbType, direction: ParameterDirection.Output);
            args.Add("@VideoPath", video.VideoPath);
            args.Add("@VideoName", video.VideoName);
            args.Add("@ThumbnailPath", video.ThumbnailPath);
            args.Add("@Keywords", video.Keywords);
            args.Add("@Rating", video.Rating);
            _dbConnection.Execute("[dbo].[InsertVideo]", args, commandType: CommandType.StoredProcedure);
            return args.Get<int>("@VideoId");
        }

        public bool DoesVideoExist(string videoPath)
            => _dbConnection.QuerySingle<bool>("[dbo].[DoesVideoExist]", new { @VideoPath = videoPath }, commandType: CommandType.StoredProcedure);

        public int GetVideoCount()
            => _dbConnection.QuerySingle<int>("[dbo].[GetVideoCount]", commandType: CommandType.StoredProcedure);

        public int GetSearchVideoCount(string keywords)
            => _dbConnection.QuerySingle<int>("[dbo].[GetSearchVideoCount]", new { @Keywords = keywords }, commandType: CommandType.StoredProcedure);

        public void Update(Video video)
            => _dbConnection.Execute("[dbo].[UpdateVideo]", new { @VideoId = video.VideoId, @VideoPath = video.VideoPath, @VideoName = video.VideoName, @ThumbnailPath = video.ThumbnailPath, @Keywords = video.Keywords, @Rating = video.Rating }, commandType: CommandType.StoredProcedure);
    }
}
