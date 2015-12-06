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

namespace course.Controllers
{
    [RoutePrefix("api/lectures")]
    [Authorize]
    public class TestController : ApiController
    {
        private IEnumerable<LectureTest> LectureTests = new List<LectureTest>
        {
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Фізика", Date = DateTime.Now , LectureName = "СТВ" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Хімія", Date = DateTime.Now , LectureName = "Білки" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Ресурси", Date = DateTime.Now, LectureName = "Поліномія" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "ФП", Date = DateTime.Now, LectureName = "Техніка безпеки" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Історія", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new LectureTest() {Discipline = "матан", Date =  DateTime.Now, LectureName = "Ще одна безтолкова річ" },
            new LectureTest() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
        };





        // GET: api/Test/5
        [Route("{discipline}/{id:int}/{itemsperpage:int}/{orderby}")]
        public IEnumerable<LectureTest> GetByDiscipline(string discipline, int page, int itemsperpage, string orderby)
        {
            IEnumerable<LectureTest> result = null;
            if (orderby == "bydate")
            {
                result = LectureTests
                    .Where(l => l.Discipline == discipline)
                    .OrderBy(l => l.Date)
                    .Skip((page - 1) * itemsperpage)
                    .Take(itemsperpage);
            }
            else if (orderby == "bydiscipline")
            {
                result = LectureTests
                    .Where(l => l.Discipline == discipline)
                    .OrderBy(l => l.Discipline)
                    .Skip((page - 1) * itemsperpage)
                    .Take(itemsperpage);
            }

            return result;
        }

        [Route("disciplines")]
        public IEnumerable<string> GetDisciplines()
        {
            IEnumerable<string> result = LectureTests
                .Select(l => l.Discipline)
                .Distinct();

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
