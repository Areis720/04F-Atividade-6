using ExoApi.Domains;
using ExoApi.Properties;

namespace ExoApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoApiContext _context;

        public ProjetoRepository(ExoApiContext context)
        {
            _context = context;
        }

        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }

        public void Criar(Projeto projeto)
        {
            _context.Projetos.Add(projeto);

            _context.SaveChanges();
        }

        public void Atualizar(int Id, Projeto projeto)
        {
            Projeto projeto1 = _context.Projetos.Find(Id);

            if (projeto.NomeDoProjeto != null)
            {
                projeto1.NomeDoProjeto = projeto.NomeDoProjeto;
            }
            if (projeto.Area != null)
            {
                projeto1.Area = projeto.Area;
            }
            if (projeto.Status != null)
            {
                projeto1.Status = projeto.Status;
            }

            _context.Projetos.Update(projeto1);

            _context.SaveChanges();
        }

        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }

        public void Deletar (int id)
        {
            Projeto projeto = _context.Projetos.Find(id);

            _context.Projetos.Remove(projeto);

            _context.SaveChanges();
        }
    }
    
}