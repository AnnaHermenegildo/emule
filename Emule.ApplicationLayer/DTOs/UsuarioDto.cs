namespace Emule.ApplicationLayer.DTOs
{
    public class UsuarioDto
    {
        public UsuarioDto()
        {
                
        }

        public UsuarioDto(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
