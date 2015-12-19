using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AspNet.Identity.CustomDatabase;
using course.Models;
using Oracle.ManagedDataAccess.Client;

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

        public Task<IEnumerable<RequestInfo>> GetAllRequestInfos()
        {
            string query = "select * from table(slavko_func())";
            Dictionary<String, object> parameters = new Dictionary<string, object>();
            var result = _database.Query(query, parameters)
                .Select(t => new RequestInfo { TutorId = t["USERID"], TutorName = t["USERNAME"] });
                

            return Task.FromResult(result);
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

        public void ActivateTutor(string tutorid)
        {
            string query = "activate_tutor";
            Dictionary<string,Tuple<object,OracleDbType>> paramaters = new Dictionary<string, Tuple<object,OracleDbType>>();
            paramaters.Add("TUTORID", new Tuple<object, OracleDbType>(tutorid, OracleDbType.Varchar2));
            _database.ExecuteProcedure(query, paramaters);
        }
    }
}