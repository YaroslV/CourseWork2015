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

namespace course.Controllers
{
    [RoutePrefix("api/lectures")]
    [Authorize]
    public class TestController : ApiController
    {
        private IEnumerable<Lecture> lectures = new List<Lecture>
        {
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Фізика", Date = DateTime.Now , LectureName = "СТВ" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Хімія", Date = DateTime.Now , LectureName = "Білки" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Ресурси", Date = DateTime.Now, LectureName = "Поліномія" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "ФП", Date = DateTime.Now, LectureName = "Техніка безпеки" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Історія", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
            new Lecture() {Discipline = "матан", Date =  DateTime.Now, LectureName = "Ще одна безтолкова річ" },
            new Lecture() {Discipline = "Правознавство", Date = DateTime.Now, LectureName = "Кримінальний кодекс" },
        };





        // GET: api/Test/5
        [Route("{discipline}/{id:int}/{itemsperpage:int}/{orderby}")]
        public IEnumerable<Lecture> GetByDiscipline(string discipline, int page, int itemsperpage, string orderby)
        {
            IEnumerable<Lecture> result = null;
            if (orderby == "bydate")
            {
                result = lectures
                    .Where(l => l.Discipline == discipline)
                    .OrderBy(l => l.Date)
                    .Skip((page - 1) * itemsperpage)
                    .Take(itemsperpage);
            }
            else if (orderby == "bydiscipline")
            {
                result = lectures
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
            IEnumerable<string> result = lectures
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
                      

            return Ok();
        }


        // POST: api/Test
        public void Post([FromBody]string value)
        { 

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
