namespace Tgyka.Microservice.MssqlBase.Model.Dtos
{
    public class UpdateDto
    {
        public int Id { get; set; }
        public DateTime? ModifiedDate => DateTime.UtcNow;
    }
}
