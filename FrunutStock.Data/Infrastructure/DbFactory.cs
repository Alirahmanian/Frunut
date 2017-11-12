using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrunutStock.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        FrunutStockEntities dbContext;

        public FrunutStockEntities Init()
        {
            return dbContext ?? (dbContext = new FrunutStockEntities());
            //The ?? operator is called the null-coalescing operator. It returns the left-hand operand if the operand is not null; otherwise it returns the right hand operand.
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
