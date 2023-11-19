using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary.Data.Entities;

namespace VideoLibrary.Service.Interfaces {
    public interface IVideoService {
        IEnumerable<Video> GetPaged(int start, int count);
        IEnumerable<Video> GetPagedSearch(string keywords, int start, int count);
        Video Get(int videoId);
        int GetVideoCount();
        int GetSearchVideoCount(string keywords);
        void Update(Video video);
        int Insert(Video video);
        int UploadFile(string uploadedFile, string basePath);
    }
}
