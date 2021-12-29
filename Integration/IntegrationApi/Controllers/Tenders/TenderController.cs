using Integration.Shared.Model;
using Integration.Tendering.Repository;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;
using IntegrationAPI.DTO.Tender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers.Tenders
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public TenderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost, Produces("application/json")]
        public IActionResult CreateTender(CreateTenderDto createTenderDto)
        {
            if (createTenderDto.Name.Equals(""))
            {
                return BadRequest("Invalid tender name.");
            }
            if(DateTime.Compare(createTenderDto.EndDate, createTenderDto.StartDate)<0)
            {
                return BadRequest("Start date must be before end date.");
            }
            if (createTenderDto.MedicineRequests.Count == 0)
            {
                return BadRequest("No medicine requests in list.");
            }
            foreach (MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
            {
                if (medicineRequestDto.MedicineName.Equals(""))
                {
                    return BadRequest("Medicine name required.");
                }
                if (medicineRequestDto.Quantity <= 0)
                {
                    return BadRequest("Invalid quantity for " + medicineRequestDto.MedicineName + ".");
                }
            }
            Tender tender = new Tender(createTenderDto.Name, new TimeRange(createTenderDto.StartDate, createTenderDto.EndDate));
            foreach(MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
            {
                tender.AddMedicationRequest(new MedicationRequest(medicineRequestDto.MedicineName, medicineRequestDto.Quantity));
            }
            _uow.GetRepository<ITenderWriteRepository>().Add(tender);
            return Ok("Tender created");
        }
    }
}
