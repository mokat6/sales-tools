namespace big_data.Models
{
    public class OtherContact
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public string Identifier { get; set; }


        // Foreign key for the related company
        public int CompanyId { get; set; }
        public Company Company { get; set; }  // Navigation property to the parent Company
        public bool Checked { get; set; }
        public DateTime Date { get; set; }

    }
}
