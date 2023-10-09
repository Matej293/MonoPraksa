using Example.Model;
using Example.Service;
using Example.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Example.WebApi.Controllers
{
    public class RandomSubclassController : ApiController
    {
        private RandomSubclassService service;
        public RandomSubclassController()
        {
            service = new RandomSubclassService();
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            List<RandomSubclassModel> list = await service.GetAll();
            if (list == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            RandomSubclassModel RandomSubclass = await service.GetById(id);
            if (RandomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, RandomSubclass);
        }

        public async Task<HttpResponseMessage> PostRandomSubclass([FromBody] RandomSubclassModel RandomSubclass)
        {
            if (RandomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.PostRandomSubclass(RandomSubclass);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutRandomSubclass(int id, RandomSubclassModel RandomSubclass)
        {
            if (RandomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.PutRandomSubclass(id, RandomSubclass);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteRandomSubclass(int id)
        {
            if (service.GetById(id).Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.DeleteRandomSubclass(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
