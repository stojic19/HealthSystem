using Integration;
using Integration.DTO;
using Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Adapters
{
    public class ComplaintAdapter
    {
        public static ComplaintDTO ComplaintToComplaintDTO(Complaint complaint)
        {
            ComplaintDTO dto = new ComplaintDTO();
            dto.ApiKey = complaint.Pharmacy.ApiKey.ToString();
            dto.CreatedDate = complaint.CreatedDate;
            dto.Description = complaint.Description;
            dto.Title = complaint.Title;
            dto.ComplaintId = complaint.Id;
            return dto;
        }
        public static Complaint CreateComplaintDTOToComplaint(CreateComplaintDTO createComplaintDTO, Pharmacy pharmacy)
        {
            Complaint complaint = new Complaint();
            complaint.Description = createComplaintDTO.Description;
            complaint.Title = createComplaintDTO.Title;
            complaint.Pharmacy = pharmacy;
            complaint.CreatedDate = DateTime.Now;
            complaint.ManagerId = Program.ManagerId;
            return complaint;
        }
    }
};
