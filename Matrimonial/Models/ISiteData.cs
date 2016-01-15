using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimonial.Models
{
    interface ISiteData
    {
        List<Occupation> GetAllOccupation();
        bool AddOccupation(Occupation occupation);
        bool DeleteOccupation(Occupation occupation);
        List<Height> GetAllHeights();
        bool AddAge(Height age);
        bool DeleteAge(Height age);
    }
}
