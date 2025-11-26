using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    public enum ParkResult
    {
        Success,
        GarageFull,
        AlreadyExists
    }

    public enum UnparkResult
    {
        Success,
        RegNoNotFound,
        GarageEmpty
    }
}
