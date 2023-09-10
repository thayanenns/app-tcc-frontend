using AppTccFrontend.Enums;


namespace AppTccFrontend.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string Telefone { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }

        public UsuarioModel() { }

    }
}
