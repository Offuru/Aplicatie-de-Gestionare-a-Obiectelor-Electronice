using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories
{
    public class BaseRepository
    {
        protected DatabaseContext DbContext { get; set; }

        public BaseRepository(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
