using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories
{
    public class ElectronicObjectRepository : BaseRepository
    {
        public ElectronicObjectRepository(DatabaseContext dbContext) : base(dbContext) { }

        public void Add(ElectronicObject electronicObject)
        {
            DbContext.Add(electronicObject);
            SaveChanges();
        }

        public void AddRange(List<ElectronicObject> electronicObjects)
        {
            DbContext.AddRange(electronicObjects);
            SaveChanges();
        }

        public ElectronicObject GetById(int id)
        {
            var result = DbContext.ElectronicObjects
                  .Where(e => e.Id == id)
                  .FirstOrDefault();

            return result;
        }

        public List<ElectronicObject> GetAll() 
        {
            var result = DbContext.ElectronicObjects.ToList();
            return result;
        }
    }
}
