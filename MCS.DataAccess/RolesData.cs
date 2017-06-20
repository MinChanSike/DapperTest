using MCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace MCS.DataAccess
{
    public class RolesData
    {
        public int Insert(RoleInfo info)
        {
            using (var conn = ConnectionFactory.GetDBConnection()){
                return conn.Insert<int>(info);
            }
        }

        public int GetAllCount()
        {
            using (var conn = ConnectionFactory.GetDBConnection())
            {
                return conn.RecordCount<RoleInfo>();
            }
        }

    }
}
