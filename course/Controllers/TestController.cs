using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using course.Models;
using course.Data;
using course.Managers;
using System.Threading.Tasks;
using System.Web;
using course.ControllerCustomizations;
using course.Managers;
using course.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net.Http.Headers;

namespace course.Controllers
{
    [RoutePrefix("api/lectures")]
    [Authorize]
    public class TestController : ApiController
    {

        // GET:
        [Authorize(Roles = "Student,Tutor")]
        [Route("all")]
        [HttpGet]
        public IEnumerable<Lecture> GetByDiscipline()
        {
            var tutorManager = new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));
            IEnumerable<Lecture> AllLectures = tutorManager.GetAllLectures();

            //if (orderby == "bydate")
            //{
            //    result = LectureTests
            //        .Where(l => l.Discipline == discipline)
            //        .OrderBy(l => l.Date)
            //        .Skip((page - 1) * itemsperpage)
            //        .Take(itemsperpage);
            //}
            //else if (orderby == "bydiscipline")
            //{
            //    result = LectureTests
            //        .Where(l => l.Discipline == discipline)
            //        .OrderBy(l => l.Discipline)
            //        .Skip((page - 1) * itemsperpage)
            //        .Take(itemsperpage);
            //}

            return AllLectures;
        }



        [Authorize(Roles = "Student,Tutor")]
        [Route("getfile/{fileid}")]
        [HttpGet]
        public HttpResponseMessage GetFile(string fileid)
        {
            var tutorManager = new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));
            var path = tutorManager.GetFilePath(fileid);
            var namePos = path.LastIndexOf('\\') + 1;
            var fileName = path.Substring(namePos);
            if (!File.Exists(path))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,"Can't load file"));

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName;
            
            
            //result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/odt");
            return result; 
        }

        //TODO get from subject table
        [Route("disciplines")]
        public IEnumerable<string> GetDisciplines()
        {
            var tutorManager = new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));
            IEnumerable<string> result = tutorManager.GetAllDisciplines();
                

            return result;

        }

        [Authorize(Roles ="Admin")]
        [Route("tutorrequests")]
        [HttpGet]
        public async Task<IEnumerable<RequestInfo>> GetTutorRequests()
        {
            var tutorManager = 
                new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));

            var result = await tutorManager.GetRequestInfo();
            return result;
        }

        [Authorize(Roles="Admin")]
        [Route("tutoractivation")]
        [HttpPost]
        public  async Task<IHttpActionResult> ActivateTutor([FromBody]TutorToActivate tutor)
        {
            var tutorManager =
                new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));
            tutorManager.ActivateTutor(tutor.TutorId);
            return Ok();
        }


        [Authorize(Roles="Tutor")]
        [HttpPost]
        [Route("addlecture")]
        public async Task<HttpResponseMessage> Post()
        { 
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new CustomMultipartFormDataStreamProvider(root);


            try {
                await Request.Content.ReadAsMultipartAsync(provider);
                
                string pathToFile = provider.FileData.First().LocalFileName;
                string subject = provider.FormData["subject"];
                string LectureName = provider.FormData["name"];
                var tutorManager = new TutorManager<TutorRequestStore, TutorRequest>(new TutorRequestStore(new ApplicationDbContext()));
                tutorManager.SaveLecture(new Lecture()
                {
                    LectureId = Guid.NewGuid().ToString(),
                    FilePath = pathToFile,
                    Subject = subject,
                    LectureText = LectureName,
                    TutorId = User.Identity.GetUserId()
                });

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }    
}
