using AspNet.Identity.CustomDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace course.Data
{
    public class LectureTable
    {
        private CustomDatabase _database;

        public LectureTable(CustomDatabase database)
        {
            _database = database;
        }

        public void Insert(Lecture lecture)
        {
            Dictionary<string,Tuple<object, string>> parameters = new Dictionary<string, Tuple<object,string>>();
            parameters.Add("LECTUREID_IN",new Tuple<object, string>(lecture.LectureId, "VARCHAR2"));
            parameters.Add("TUTORID_IN", new Tuple<object, string>(lecture.TutorId, "VARCHAR2"));
            parameters.Add("LECTURETXT_IN",new Tuple<object, string>( lecture.LectureText, "CLOB"));            
            parameters.Add("FILEPATH_IN",new Tuple<object, string> (lecture.FilePath, "VARCHAR2"));
            parameters.Add("SUBJECTID_IN" ,new Tuple<object, string>(Guid.NewGuid().ToString(), "VARCHAR2"));
            parameters.Add("SUBJECT_IN",new Tuple<object, string>(lecture.Subject, "VARCHAR2") );
            string command = "ADD_LECTURE";
            var res =_database.ExecuteProcedure(command, parameters);
        }

        public void Update(Lecture lecture)
        {
            //TODO
        }

        public void Delete(string lectureId)
        {
            Dictionary<string, Tuple<object, string>> paramaters = new Dictionary<string, Tuple<object, string>>();
            paramaters.Add("LECTUREID_IN", new Tuple<object, string>(lectureId,"VARCHAR2"));
            string command = "DELETE_LECTURE";
            var res = _database.ExecuteProcedure(command, paramaters);
        }

        public IEnumerable<Lecture> GetAll()
        {
            string query = "SELECT * FROM TABLE(GET_MANY_LECTURES)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            var result = _database.Query(query, param)
                .Select(l => new Lecture()
                {
                    LectureId = l["LECTUREID"],
                    LectureText = l["LECTURENAME"],
                    Subject = l["LECTURESUBJECT"],
                    Date = l["LECTUREDATE"],
                    TutorId = l["LECTURETUTOR"]
                });

            return result;                            
        }

        public Lecture GetLectureById(string id)
        {
            string query = @"SELECT * FROM TABLE(GET_LECTUREINFO_BY_ID(:Id))";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {":Id", id }
            };
            var result = _database.Query(query, parameters)
                .Select(l => new Lecture()
                {                    
                    LectureText = l["LECTURENAME"],
                    Subject = l["LECTURESUBJECT"],
                    FilePath = l["LECTUREFILE"]                    
                })
                .FirstOrDefault();

            return result;
        }

        public IEnumerable<Lecture> GetAllByTutorId(string tutorId)
        {
            string query = @"SELECT * FROM TABLE(GET_LECTURES_BY_TUTORID(:tutorId))";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {":tutorId", tutorId }
            };
            var result = _database.Query(query, parameters)
                .Select(l => new Lecture()
                {
                    LectureId = l["LECTUREID"],
                    LectureText = l["LECTURENAME"],
                    Subject = l["LECTURESUBJECT"],
                    Date = l["LECTUREDATE"],
                    TutorId = l["LECTURETUTOR"]
                });

            return result;
        }


        public IEnumerable<string> GetAllSubjects()
        {
            string query = "SELECT SUBJECTNAME FROM SUBJECTS";
            Dictionary<string, object> param = new Dictionary<string, object>();
            var result = _database.Query(query, param)
                            .Select(l => l["SUBJECTNAME"]);

            return result;
        }

        public  string GetFilePath(string id)
        {
            var query = "SELECT FILEPATH FROM LECTURES WHERE LECTUREID = :id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(":id", id);
            var result = _database.QueryValue(query,param);

            return (string)result;
        }

        public string GetFileName(string id)
        {
            var query = "SELECT SUBJECTNAME FROM LECTURES WHERE SUBJECTID = :id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(":id", id);
            var result = _database.QueryValue(query, param);

            return (string)result;
        }
    }
}