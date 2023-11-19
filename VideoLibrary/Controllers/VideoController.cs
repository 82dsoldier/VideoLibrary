using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using VideoLibrary.Data.Entities;
using VideoLibrary.Service.Interfaces;

namespace VideoLibrary.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class VideoController : ControllerBase {
        private readonly IVideoService _videoService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VideoController(IVideoService videoService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {
            _videoService = videoService;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetPaged")]
        public IActionResult GetPaged(int start, int count)
            => Ok(_videoService.GetPaged(start, count));

        [HttpGet]
        [Route("GetPagedSearch")]
        public IActionResult GetPagedSearch(string keywords, int start, int count)
            => Ok(_videoService.GetPagedSearch(keywords, start, count));

        [HttpGet]
        [Route("GetVideo")]
        public IActionResult GetVideo(int videoId)
            => Ok(_videoService.Get(videoId));

        [HttpGet]
        [Route("GetVideoCount")]
        public IActionResult GetVideoCount()
            => Ok(_videoService.GetVideoCount());

        [HttpGet]
        [Route("GetSearchVideoCount")]
        public IActionResult GetSearchVideoCount(string keywords)
            => Ok(_videoService.GetSearchVideoCount(keywords));

        [HttpPut]
        [Route("UpdateVideo")]
        public IActionResult UpdateVideo(Video video) {
            _videoService.Update(video);
            return Ok();
        }

        [HttpGet]
        [Route("UploadVideo")]
        public IActionResult UploadVideo(string fileName)
            => Ok(_videoService.UploadFile(fileName, _webHostEnvironment.ContentRootPath));
    }
}
