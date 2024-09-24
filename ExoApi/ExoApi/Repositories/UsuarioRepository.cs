using ExoApi.Domains;
using ExoApi.Properties;

namespace ExoApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ExoApiContext _context;

        public UsuarioRepository(ExoApiContext context)
        {            
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public void Criar(Usuario usuario) 
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(int id, Usuario usuario) 
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            if (usuario.Email != null) 
            {
                usuarioBuscado.Email = usuario.Email;
            }
            if (usuario.Senha != null)
            {
                usuarioBuscado.Senha = usuario.Senha;
            }

            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }

        public Usuario BuscaPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void Deletar(int id)
        {
            Usuario usuario = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public Usuario BusacarPorEmailESenha(Usuario usuario)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);
        }

    }
}
