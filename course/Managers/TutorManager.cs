﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using course.Data;
using System.Threading.Tasks;
using course.Models;

namespace course.Managers
{
    public class TutorManager<Store,T> where Store : class, Data.IStore<T> where T : class
    {
        private Store _store;
        private LectureTable _lectureTable;
        
        public TutorManager(Store store)
        {
            _store = store;
            _lectureTable = new LectureTable(new AspNet.Identity.CustomDatabase.CustomDatabase());
        }


        public async Task<int> AddTutorRequest(T req)
        {
            var res = await _store.AddNewRequestAsync(req);
            return res;
        }

        public async Task<IEnumerable<RequestInfo>> GetRequestInfo()
        {
            var res = await _store.GetAllRequestInfos();
            return res;
        }

        public void ActivateTutor(string tId)
        {
            _store.ActivateTutor(tId);
        }

        public void SaveLecture(Lecture lecture)
        {
            _lectureTable.Insert(lecture);
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            var res = _lectureTable.GetAll();
            return res;
        }

        public IEnumerable<string> GetAllDisciplines()
        {
            var res = _lectureTable.GetAllSubjects();
            return res;
        }

        public string GetFilePath(string id)
        {
            var res = _lectureTable.GetFilePath(id);
            return res;
        }
    }
}