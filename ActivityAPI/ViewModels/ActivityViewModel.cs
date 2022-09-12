namespace ActivityAPI.ViewModels
{
    public class ActivityViewModel
    {
       
        public int ActivityId { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }
        public string? ActivityName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateDeadline { get; set; }
        public int CityId { get; set; }
        public string? Description { get; set; }
        public string? Adress { get; set; }
        public int TickedTypeId { get; set; }
        
    }
}
