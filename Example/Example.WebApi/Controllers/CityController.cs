using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Example.Model;
using Example.Model.Common;
using Example.Service.Common;
using Example.WebApi.Models;

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
            List<ICityModel> cities = await _service.GetAll();

            if (cities == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            ICityModel city = await _service.GetById(id);

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

        public async Task<HttpResponseMessage> PostCity([FromBody]CityCreateRest cityRest)
        {
            if(cityRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            CityModel city = CityMapping(cityRest);

            await _service.PostCity(city);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutCity(Guid id, CityModel city)
        {
            if (city == null | _service.GetById(id) == null)
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

        private CityModel CityMapping(CityCreateRest cityRest)
        {
            CityModel city = new CityModel
            {
                Id = Guid.NewGuid(),
                Name = cityRest.Name,
                Country = cityRest.Country,
                Population = cityRest.Population,
                RandomSubclassId = cityRest.RandomSubclassId,
                RandomSubclass = cityRest.RandomSubclass
            };

            return city;
        }


    }
}
