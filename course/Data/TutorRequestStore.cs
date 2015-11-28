﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AspNet.Identity.CustomDatabase;

namespace course.Data
{
    public class TutorRequestStore : IStore<TutorRequest>
    {
        private CustomDatabase _database;

        public TutorRequestStore(CustomDatabase database)
        {
            _database = database;
        }



        public Task<int> AddNewRequestAsync(TutorRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request is null");


            string query = "INSERT INTO TUTORREQUEST(REQUESTID,REQUESTERID) VALUES(:requestId, :requesterId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("requestId", request.RequestId);
            parameters.Add("requesterId",request.RequesterId);
            int result = _database.Execute(query, parameters);
            return Task.FromResult(result);
        }

        public Task<IQueryable<TutorRequest>> GetAllRequests()
        {
            throw new NotImplementedException();
        }

        public Task RemoveRequest(string requestId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if(_database != null)
            {
                _database.Dispose();
                _database = null;
            }
        }
    }
}