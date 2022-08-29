using System;
using System.IO;
using AutoMapper;
using chatApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers
{

    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {


        private IWebHostEnvironment _hostingEnvironment;

        public ImageController(IWebHostEnvironment hostingEnvironment)
        {

            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("upload/car")]
        public ActionResult uploadImage([FromForm] IFormFile file,[FromForm]int status)
        {
            string path = _hostingEnvironment.WebRootPath + "/images/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

           string endPointFile="";
           if(status==0){
            endPointFile=".jpeg";
           }else if(status==1){
              endPointFile=".pdf";
           }else {
              endPointFile="audio.3gpp";
           }

            String fileName = DateTime.Now.ToString("yyyyMMddTHHmmss") + endPointFile;
            using (var fileStream = System.IO.File.Create(path + fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                return Ok(fileName);
            }
        }

    }
}