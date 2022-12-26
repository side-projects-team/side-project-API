using side_project_API.Contracts;

namespace side_project_API.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        // Add more repository contexs here as variable (can be null)

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        // Maybe replace with savechanges
        public void Save() => _repositoryContext.SaveChangesAsync();
    }
}
