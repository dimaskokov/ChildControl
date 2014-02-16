using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWebApi.Data
{
    public interface ICachedObj
    {
        Guid Id { get; }
    }
}
