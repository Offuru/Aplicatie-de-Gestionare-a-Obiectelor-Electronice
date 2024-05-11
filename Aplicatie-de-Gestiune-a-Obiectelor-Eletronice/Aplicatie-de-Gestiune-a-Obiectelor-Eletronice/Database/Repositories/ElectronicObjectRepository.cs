﻿using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using Microsoft.EntityFrameworkCore;
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

        public bool UpdateById(ElectronicObject electronicObject)
        {
            var result = DbContext.ElectronicObjects
                .SingleOrDefault(e => e.Id == electronicObject.Id);
            if(result != null)
            {
                result.Type = electronicObject.Type;
                result.Name = electronicObject.Name;
                result.ReceiptNumber = electronicObject.ReceiptNumber;
                result.ActiveObjectType = electronicObject.ActiveObjectType;
                result.Serial = electronicObject.Serial;
                result.Order = electronicObject.Order;
                result.Code = electronicObject.Code;
                result.Date = electronicObject.Date;
                result.Destination = electronicObject.Destination;
                result.ReceiverName = electronicObject.ReceiverName;
                DbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeleteById(int Id)
        {
            var itemToRemove = DbContext.ElectronicObjects
                .SingleOrDefault(x => x.Id == Id);

            if (itemToRemove != null)
            {
                DbContext.ElectronicObjects.Remove(itemToRemove);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
