using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary.Data.Entities;

namespace VideoLibrary.Data.Interfaces {
    public interface IVideoRepo {
        IEnumerable<Video> GetAll();
        IEnumerable<Video> GetPaged(int start, int count);
        IEnumerable<Video> GetPagedSearch(string keywords, int start, int count);
        Video Get(int videoId);
        int Insert(Video video);
        bool DoesVideoExist(string videoPath);
        int GetVideoCount();
        int GetSearchVideoCount(string keywords);
        void Update(Video video);
    }
}
