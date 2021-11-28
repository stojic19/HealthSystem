namespace Integration.Partnership.Model
{
    public class MedicineSpecificationFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Host { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy.Model.Pharmacy Pharmacy { get; set; }
    }
}
