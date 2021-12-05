using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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

            return CityReadRepo.GetAll(); 
        }


    }
}
