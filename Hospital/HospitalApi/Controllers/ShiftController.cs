using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ShiftController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetHospitalShifts()
        {
            var shiftRepo = _uow.GetRepository<IShiftReadRepository>();
            return Ok(
            shiftRepo.GetAll());
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult InsertShift(ShiftDTO shiftDTO)
        {
            try
            {
                if (shiftDTO == null)
                {
                    return BadRequest("Incorrect shift format sent! Please try again.");
                }

                var shiftRepo = _uow.GetRepository<IShiftWriteRepository>();
                Shift addedShift = shiftRepo.Add(_mapper.Map<Shift>(shiftDTO));

                if (addedShift == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not insert shift in the database.");
                }

                return Ok("Your shift has been submitted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting shift in the database.");
            }

        }
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public IActionResult UpdateShift([FromQuery(Name = "id")] int id, [FromQuery(Name = "from")] int from, [FromQuery(Name = "to")] int to)
        {
            try
            {
                if (id <= 0 || from > to || from <= 0 || to <= 0)
                {
                    return BadRequest();
                }

                var shiftReadRepo = _uow.GetRepository<IShiftReadRepository>();
                var shiftWriteRepo = _uow.GetRepository<IShiftWriteRepository>();
                var shift = shiftReadRepo.GetById(id);
                shift.From = from;
                shift.To = to;
                Shift updatedShift = shiftWriteRepo.Update(shift);

                if (updatedShift == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update shift!");
                }

                return Ok(updatedShift);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete]
        public IActionResult DeleteShift([FromQuery(Name = "id")] int id) {

            if (id <= 0) {
                return BadRequest();
            }
            var shiftReadRepo = _uow.GetRepository<IShiftReadRepository>();
            var shiftWriteRepo = _uow.GetRepository<IShiftWriteRepository>();

            var shift = shiftReadRepo.GetById(id);
            shiftWriteRepo.Delete(shift);
            return Ok();

        }

    }
}
