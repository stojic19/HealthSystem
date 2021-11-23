using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hospital.Repositories.Base;
using Hospital.Model;
using Hospital.Repositories;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [HttpGet]
        public IEnumerable<City> GetAll() {

            var CityReadRepo = uow.GetRepository<ICityReadRepository>();

            return CityReadRepo.GetAll(); //
        }


    }
}
