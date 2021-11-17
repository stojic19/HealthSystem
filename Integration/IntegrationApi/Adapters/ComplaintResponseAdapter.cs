using Integration.Model;
using IntegrationAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Adapters
{
    public class ComplaintResponseAdapter
    {
        public static ComplaintResponse ComplaintResponseDTOToComplaintResponse(ComplaintResponseDTO dto)
        {
            return new ComplaintResponse 
            { CreatedDate = dto.createdDate, Text = dto.Text, ComplaintId = dto.HospitalComplaintId };
        }
    }
}
