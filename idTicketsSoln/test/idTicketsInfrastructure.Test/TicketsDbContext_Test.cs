using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    public class TicketsDbContext_Test
    {
        [Fact]
        void canSuccessfullyConnectToDb()
        {
            bool success = false;
            using (TicketsDbContext dbCtx = new idTicketsInfrastructure.TicketsDbContext())
            {

                success = dbCtx.Database.CanConnect();
                Assert.True(success);

            }


        }

    }
}
