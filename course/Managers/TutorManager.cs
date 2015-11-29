using System;
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
        public TutorManager(Store store)
        {
            _store = store;
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
    }
}