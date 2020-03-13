namespace Safes.Models.Dto
{
    public class BoxCountDto
    {
        public int Total { get; set; }
        public int Delivered { get; set; }
        public int Received { get; set; }
        public int Late { get; set; }
        public int TooLate { get; set; }
    }
}
