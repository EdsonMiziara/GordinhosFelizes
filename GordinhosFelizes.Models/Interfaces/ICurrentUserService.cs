using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GordinhosFelizes.Domain.Interfaces;

public interface ICurrentUserService
{
    int UserId { get; }
}
