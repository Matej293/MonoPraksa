using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Example.Model;
using Example.Model.Common;
using Example.Service.Common;

namespace Example.WebApi.Controllers
{
    public class RandomSubclassController : ApiController
    {
        private readonly IRandomSubclassService _service;

        public RandomSubclassController(IRandomSubclassService service)
        {
            _service = service;
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            List<Model.Common.RandomSubclassModel> list = await _service.GetAll();
            if (list == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            Model.Common.RandomSubclassModel randomSubclass = await _service.GetById(id);
            if (randomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, randomSubclass);
        }

        public async Task<HttpResponseMessage> PostRandomSubclass([FromBody] Model.RandomSubclassModel randomSubclass)
        {
            if (randomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.PostRandomSubclass(randomSubclass);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/randomsubclass/init")]
        public async Task<HttpResponseMessage> PostRandomSubclass()
        {
            await _service.Init(); // initializing the tables with create if not exists

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutRandomSubclass(Guid id, Model.RandomSubclassModel randomSubclass)
        {
            if (randomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.PutRandomSubclass(id, randomSubclass);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteRandomSubclass(Guid id)
        {
            if (_service.GetById(id).Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await _service.DeleteRandomSubclass(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
