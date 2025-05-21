namespace Villa1.ViewModels
{
    public class BestUpdateVM
    {
        public int Id { get; set; } 
        public string? ImgUrl { get; set; }
        public IFormFile? ImgFile { get; set; }

        public string CAtegory { get; set; }

        public double Price { get; set; }
        public string Address { get; set; }
    }
}
    