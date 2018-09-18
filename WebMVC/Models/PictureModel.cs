namespace WebMVC.Models
{
    public class PictureModel
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string PictureDescription { get; set; }

        public byte[] Content { get; set; }

        public bool IsSnail { get; set; }

        public string Name { get; set; }
    }
}