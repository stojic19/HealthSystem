using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharamcyApi.DTO;
using Pharmacy.Model;

namespace PharamcyApi.Adapters
{
    public class ComplaintAdapter
    {
        public static Complaint ComplaintDTOToComplaint(ComplaintDTO dto, Hospital hospital)
        {
            Complaint complaint = new Complaint { CreatedDate = dto.CreatedDate, Description = dto.Description, Title = dto.Title,
                Hospital = hospital, HospitalId = hospital.Id, HospitalComplaintId = dto.ComplaintId };
            return complaint;
        }
    }
}
