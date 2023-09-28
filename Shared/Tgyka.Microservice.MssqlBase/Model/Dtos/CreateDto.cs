namespace Tgyka.Microservice.MssqlBase.Model.Dtos
{
    public class CreateDto
    {
        public DateTime CreatedDate => DateTime.UtcNow;
        public int CreatedBy { get; set; }
    }
}
