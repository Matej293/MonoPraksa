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
    public class CityController : ApiController
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }
        
        public async Task<HttpResponseMessage> GetCities()
        {
            List<CityModel> cities = await _service.GetAll();

            if (cities == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            CityModel city = await _service.GetById(id);

            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [Route("api/city/init")]
        public async Task<HttpResponseMessage> PostCity()
        {
            await _service.Init(); // initializing the tables with create if not exists

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PostCity([FromBody]CityModel city)
        {
            if(city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.PostCity(city);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutCity(Guid id, CityModel city)
        {
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await _service.PutCity(id, city);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteCity(Guid id)
        {
            if (_service.GetById(id).Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await _service.DeleteCity(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
