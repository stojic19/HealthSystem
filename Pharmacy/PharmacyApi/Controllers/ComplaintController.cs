using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using PharmacyApi.DTO.Base;
using PharmacyApi.HttpRequestSenders;
using PharmacyApi.HttpRequestSenders.Implementation;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintController : BasePharmacyController
    {
        private readonly IHttpRequestSender _httpSender;
        public ComplaintController(IUnitOfWork uow, PharmacyDetails details, IHttpRequestSender sender) : base(uow, details)
        {
            _httpSender = sender;
        }

        [HttpPost]
        public IActionResult GetComplaints(BaseCommunicationDTO hospital)
        {
            if (!IsApiKeyValid(hospital.ApiKey))
            {
                return BadRequest(ModelState);
            }

            var complaints = UoW.GetRepository<IComplaintReadRepository>()
                .GetAll()
                .Where(x => x.Hospital.ApiKey == hospital.ApiKey);

            var complaintResponses = UoW.GetRepository<IComplaintResponseReadRepository>()
                .GetAll()
                .Where(x => x.Complaint.Hospital.ApiKey == hospital.ApiKey);

            var complaintsAndResponses = complaints
                .Join(complaintResponses, x => x.Id, y => y.ComplaintId, (x, y) => new ComplaintsAndResponsesDTO()
                {
                    Complaint = new ComplaintDTO()
                    {
                        ApiKey = x.Hospital.ApiKey,
                        CreatedDateTime = x.CreatedDate,
                        Description = x.Description,
                        Title = x.Title
                    },
                    ApiKey = hospital.ApiKey,
                    ComplaintResponse = (y != null) 
                        ? new ComplaintResponseDTO()
                        {
                            ApiKey = y.Complaint.Hospital.ApiKey,
                            CreatedDateTime = y.CreatedDate,
                            Text = y.Text
                        } 
                        : null,
                });

            return Ok(complaintsAndResponses);
        }

        [HttpPost]
        public IActionResult CreateComplaint(ComplaintDTO newComplaint)
        {
            if (!IsApiKeyValid(newComplaint.ApiKey))
            {
                return BadRequest(ModelState);
            }

            var complaint = new Complaint()
            {
                CreatedDate = newComplaint.CreatedDateTime,
                Description = newComplaint.Description,
                HospitalId = UoW.GetRepository<IHospitalReadRepository>().GetAll()
                    .First(x => x.ApiKey == newComplaint.ApiKey).Id,
                Title = newComplaint.Title
            };

            try
            {
                UoW.GetRepository<IComplaintWriteRepository>()
                    .Add(complaint);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok(Responses.Success);
        }

        [HttpPost]
        public IActionResult CreateComplaintResponse(CreateComplaintResponseDTO newResponse)
        {
            var foundComplaint = UoW.GetRepository<IComplaintReadRepository>().GetAll().Include(c => c.Hospital)
                .FirstOrDefault(c => c.Id == newResponse.ComplaintId);
            if (foundComplaint == null)
            {
                ModelState.AddModelError("ComplaintId", "Complaint not found!");
            }

            var existingComplaintResponse = UoW.GetRepository<IComplaintResponseReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ComplaintId == newResponse.ComplaintId);

            if (existingComplaintResponse != null)
            {
                ModelState.AddModelError("ComplaintId", "That complaint is already answered!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newComplaintResponse = new ComplaintResponse()
            {
                ComplaintId = newResponse.ComplaintId,
                CreatedDate = DateTime.Now,
                Text = newResponse.Description
            };

            try
            {
                UoW.GetRepository<IComplaintResponseWriteRepository>().Add(newComplaintResponse);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            //TODO: notify the hospital

            ComplaintResponseToPharmacyDto dto = new ComplaintResponseToPharmacyDto
            {
                ApiKey = foundComplaint.Hospital.ApiKey,
                ComplaintCreatedDate = fixTimeZone(foundComplaint.CreatedDate),
                CreatedDate = fixTimeZone(newComplaintResponse.CreatedDate),
                Text = newComplaintResponse.Text
            };
            var response = _httpSender.Post(foundComplaint.Hospital.BaseUrl + "/api/ComplaintResponse/ReceiveComplaintResponse", dto);
            if (response.StatusCode != HttpStatusCode.OK) return Problem("Hospital failed to receive response");
            return Ok();
        }

        private DateTime fixTimeZone(DateTime time)
        {
            return time.ToUniversalTime() + (time - time.ToUniversalTime());
        }
    }
}
