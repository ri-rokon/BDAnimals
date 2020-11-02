using BDAnimalsWeb.Models;
using BDAnimalsWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BDAnimalsWeb.Repository
{
    public class ScientificClassRepository:Repository<ScientificClass>, IScientificClassRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public ScientificClassRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

    }
}
