using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FrunutStock.Model.Models;
using FrunutStock.Data;

namespace FrunutStock.BL.General
{
    public class Relations
    {
        
        public bool CheckRelatedRecords(FrunutStockEntities db, string tableName, string foreignKeyName, Int64 id)
        {
            var tables = db.Database.SqlQuery<string>("Select OBJECT_NAME(parent_object_Id) from sys.foreign_keys where object_name(referenced_object_id) = '" + tableName + "'").ToList();
            foreach (var table in tables)
            {
                var related = db.Database.SqlQuery<Int64>("select " + foreignKeyName + " from " + table + " where " + foreignKeyName + " = " + id + "").Count();//.FirstOrDefault();
                if (related > 0)
                    return true;

            }
            return false;
          
        }
    }
}
