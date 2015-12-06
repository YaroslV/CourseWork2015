using AspNet.Identity.CustomDatabase;
using System;
using System.Collections.Generic;

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

        //public IEnumerable<Lecture> GetAll()
        //{

        //}
    }
}