using Example.WebApi.Models;
using Example.Repository;
using Microsoft.Ajax.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using Example.Service;
using Example.Service.Common;
using System.Net.NetworkInformation;
using Example.Model;
using Example.Model.Common;

namespace Example.WebApi.Controllers
{
    public class CityController : ApiController
    {
        private CityService service;
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

        public async Task<HttpResponseMessage> GetById(int id)
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
            await service.PostCity(city);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutCity(int id, CityModel city)
        {
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.PutCity(id, city);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteCity(int id)
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
