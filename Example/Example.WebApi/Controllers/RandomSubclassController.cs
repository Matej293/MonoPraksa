using System;
using Example.Model;
using Example.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Example.WebApi.Controllers
{
    public class RandomSubclassController : ApiController
    {
        private readonly RandomSubclassService service;

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

        public async Task<HttpResponseMessage> GetById(Guid id)
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

            await service.Init(); // initializing the tables with create if not exists

            await service.PostRandomSubclass(RandomSubclass);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutRandomSubclass(Guid id, RandomSubclassModel RandomSubclass)
        {
            if (RandomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.PutRandomSubclass(id, RandomSubclass);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteRandomSubclass(Guid id)
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
