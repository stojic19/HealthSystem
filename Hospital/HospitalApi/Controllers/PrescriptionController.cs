using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using HospitalApi.HttpRequestSenders;
using RestSharp;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpRequestSender _httpRequestSender;
        private readonly string _integrationBaseUrl;
        public PrescriptionController(IUnitOfWork unitOfWork, IHttpRequestSender httpRequestSender)
        {
            _unitOfWork = unitOfWork;
            _httpRequestSender = httpRequestSender;
            _integrationBaseUrl = "https://localhost:44302/";
        }

        [HttpPost]
        public IActionResult CreateNewPrescription(NewPrescriptionDTO newPrescriptionDto)
        {
            Patient patient = _unitOfWork.GetRepository<IPatientReadRepository>().GetById(newPrescriptionDto.PatientId);
            if (patient == null)
            {
                ModelState.AddModelError("Patient Id", "Patient with that id does not exist");
                return BadRequest(ModelState);
            }

            Medication medication = _unitOfWork.GetRepository<IMedicationReadRepository>()
                .GetById(newPrescriptionDto.MedicineId);
            if (medication == null)
            {
                ModelState.AddModelError("Medication Id", "Medication with that id does not exist");
                return BadRequest(ModelState);
            }

            Prescription newPrescription = new Prescription
            {
                Patient = patient,
                MedicationId = medication.Id,
                Medication = medication,
                EndDate = newPrescriptionDto.EndDate,
                StartDate = newPrescriptionDto.StartDate,
                IssuedDate = DateTime.Now
            };
            var prescriptionToIntegrationDTO = new PrescriptionToIntegrationDTO
            {
                PatientFirstName = patient.FirstName,
                PatientLastName = patient.LastName,
                StartDate = newPrescription.StartDate,
                EndDate = newPrescription.EndDate,
                IssuedDate = newPrescription.IssuedDate,
                MedicineName = medication.Name
            };
            var response = _httpRequestSender.Post(_integrationBaseUrl + "api/Prescription/PostPrescription",
                prescriptionToIntegrationDTO);
            var writeRepo = _unitOfWork.GetRepository<IPrescriptionWriteRepository>();
            writeRepo.Add(newPrescription);
            return Ok(response.Content);
        }
    }
}
