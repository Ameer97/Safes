namespace Safes.Models.Dto
{
    public class OwnerCreateDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public bool IsMale { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}
