using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Conventions
{
    internal class TurncateConverter : ValueConverter<string, string>
    {
        public TurncateConverter(int limit) : base (x => x.Substring(0, limit), x => x)
        {

        }
    }
}
