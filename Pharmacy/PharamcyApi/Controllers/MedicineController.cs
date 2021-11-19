using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MedicineController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public Medicine Add(Medicine medicine)
        {
            return _uow.GetRepository<IMedicineWriteRepository>().Add(medicine);
        }

        [HttpGet]
        public IEnumerable<Medicine> GetAll()
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetAll();
        }

        [HttpGet("{id}")]
        public Medicine GetById(int id)
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetById(id);
        }

        [HttpGet("{name}")]
        public Medicine GetByName(string name)
        {
            return _uow.GetRepository<IMedicineReadRepository>().GetAll().FirstOrDefault(medicine => medicine.Name.Equals(name));
        }
    }
}
