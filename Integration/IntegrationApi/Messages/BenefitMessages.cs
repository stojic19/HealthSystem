using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public static class BenefitMessages
    {
        public static readonly string WrongId = "Benefit with that ID not found";
        public static readonly string AlreadyPublished = "Benefit is already published";
        public static readonly string CannotPublish = "Error, could not publish benefit";
        public static readonly string ConfirmPublish = "Benefit published";
        public static readonly string AlreadyHidden = "Benefit is already hidden";
        public static readonly string CannotHide = "Error, could not hide benefit";
        public static readonly string ConfirmHide = "Benefit hidden";
    }
}
