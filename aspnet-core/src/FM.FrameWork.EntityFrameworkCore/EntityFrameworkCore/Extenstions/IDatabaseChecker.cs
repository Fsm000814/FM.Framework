using FM.FrameWork.Database;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.EntityFrameworkCore.Extenstions
{
    public interface IDatabaseChecker<TDbContext> : IDatabaseChecker where TDbContext : DbContext
    {
    }
}
