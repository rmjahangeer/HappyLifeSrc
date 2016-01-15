using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matrimonial.Models
{
    public class SiteDataRepository:ISiteData
    {
        MatrimonialEntities entities = new MatrimonialEntities();
        public List<Occupation> GetAllOccupation()
        {
            return entities.Occupations.ToList();
        }

        public bool AddOccupation(Occupation occupation)
        {
            entities.Occupations.Add(occupation);
            return true;
        }

        public bool DeleteOccupation(Occupation occupation)
        {
            throw new NotImplementedException();
        }

        public List<Height> GetAllHeights()
        {
            return entities.Heights.ToList();
        }

        public bool AddAge(Height height)
        {
            entities.Heights.Add(height);
            return true;
        }

        public bool DeleteAge(Height height)
        {
            throw new NotImplementedException();
        }
    }
}