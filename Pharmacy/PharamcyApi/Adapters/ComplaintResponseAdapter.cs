using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharamcyApi.DTO;
using Pharmacy.Model;

namespace PharamcyApi.Adapters
{
    public class ComplaintResponseAdapter
    {
        public static ComplaintResponseDTO ComplaintResponseToComplaintResponseDTO(ComplaintResponse complaintResponse)
        {
            return new ComplaintResponseDTO()
            {
                ApiKey = complaintResponse.Complaint.Hospital.ApiKey.ToString(),
                createdDate = complaintResponse.CreatedDate,
                Text = complaintResponse.Text,
                HospitalComplaintId = complaintResponse.Complaint.HospitalComplaintId
            };
        }
    }
}
