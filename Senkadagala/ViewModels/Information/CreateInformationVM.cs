namespace Senkadagala.ViewModels
{
    public class CreateInformationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
