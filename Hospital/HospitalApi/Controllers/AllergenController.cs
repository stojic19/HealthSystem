using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AllergenController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult GetAllergens()
        {
            try
            {
                var allergensReadRepo = _uow.GetRepository<IMedicationIngredientReadRepository>();
                var allergens = allergensReadRepo.GetAll();
                return Ok(allergens);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error! Failed loading allergens!");
            }
        }
    }
}
