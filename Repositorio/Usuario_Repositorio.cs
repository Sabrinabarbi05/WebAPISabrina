using Microsoft.EntityFrameworkCore;
using WebApiBia.ORM;

namespace ProjetoWebAPI.Repositorio
{
    public class Usuario_Repositorio
    {
        private readonly ProjetoBancoContext _context;

        public Usuario_Repositorio(ProjetoBancoContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}
