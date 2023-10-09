using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Example.Service;
using Example.Model;

namespace Example.WebApi.Controllers
{
    public class CityController : ApiController
    {
        private readonly CityService service;

        public CityController()
        {
            service = new CityService();
        }

        public async Task<HttpResponseMessage> GetCities()
        {
            List<CityModel> cities = await service.GetAll();
            if (cities == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            CityModel city = await service.GetById(id);
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        public async Task<HttpResponseMessage> PostCity([FromBody]CityModel city)
        {
            if(city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await service.Init(); // initializing the tables with create if not exists

            await service.PostCity(city);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutCity(Guid id, CityModel city)
        {
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.PutCity(id, city);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteCity(Guid id)
        {
            if (service.GetById(id).Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.DeleteCity(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
