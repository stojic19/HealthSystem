using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.Attributes;

namespace PharmacyApi.DTO.Base
{
    public class BaseCommunicationDTO
    {
        [GuidValidation(ErrorMessage = "ApiKey is necessary to identify the hospital!")]
        public Guid ApiKey { get; set; }
    }
}
