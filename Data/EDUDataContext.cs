using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EduDataAPI.Models
{
    public class EDUDataContext : DbContext
    {
        public EDUDataContext(DbContextOptions<EDUDataContext> options)
            : base(options)
            {
                
            }
            public DbSet<DataElement> DataElement { get; set; }
            public DbSet<Datatype> Datatype { get; set; }
            public DbSet<Facets> Facets { get; set; }
            public DbSet<Domain> Domain { get; set; }
            public DbSet<Change> Change { get; set; }
            public DbSet<ChangeSet> ChangeSet { get; set; }
            
    }
}