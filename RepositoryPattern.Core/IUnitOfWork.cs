using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<Author> Authors { get; }

        IBooksRepository Books { get; }

        int Complete();
    }
}
