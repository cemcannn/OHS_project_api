using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;
using System.Collections.Generic;

namespace OHS_program_api.Domain.Entities.Trainings
{
    public class Certificate : BaseEntity
    {
        public string Name { get; set; }
        public Guid TypeOfCertificateId { get; set; }
        public ICollection<TypeOfCertificate> TypeOfCertificates { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}
