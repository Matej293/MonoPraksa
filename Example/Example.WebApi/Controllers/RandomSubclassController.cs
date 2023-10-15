using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Example.Model;
using Example.Model.Common;
using Example.Service.Common;
using Example.WebApi.Models;

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
            List<IRandomSubclassModel> list = await _service.GetAll();
            if (list == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            IRandomSubclassModel randomSubclass = await _service.GetById(id);

            if (randomSubclass == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, randomSubclass);
        }


        [Route("api/randomsubclass/init")]
        public async Task<HttpResponseMessage> PostRandomSubclass()
        {
            await _service.Init(); // initializing the tables with create if not exists

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PostRandomSubclass([FromBody] RandomSubclassCreateRest rsRest)
        {
            if (rsRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            RandomSubclassModel rand = RSMapping(rsRest);

            await _service.PostRandomSubclass(rand);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        public async Task<HttpResponseMessage> PutRandomSubclass(Guid id, IRandomSubclassModel randomSubclass)
        {
            if (randomSubclass == null | _service.GetById(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.PutRandomSubclass(id, randomSubclass);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteRandomSubclass(Guid id)
        {
            if (_service.GetById(id).Id == 0 | _service.GetById(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.DeleteRandomSubclass(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private RandomSubclassModel RSMapping(RandomSubclassCreateRest rsRest)
        {
            RandomSubclassModel rand = new RandomSubclassModel
            {
                Id = Guid.NewGuid(),
                RandomArg1 = rsRest.RandomArg1,
                RandomArg2 = rsRest.RandomArg2
            };

            return rand;
        }
    }
}
