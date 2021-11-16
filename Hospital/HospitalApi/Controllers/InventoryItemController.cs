using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public InventoryItemController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /*[HttpPost]
        public IActionResult AddInventoryItems(IEnumerable<InventoryItem> inventoryItems)
        {
            var inventoryItemRepo = _uow.GetRepository<IInventoryItemWriteRepository>();
            return Ok(inventoryItemRepo.AddRange(inventoryItems));
        }*/

    }
}
