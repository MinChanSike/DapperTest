using MCS.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;

/*
    MCS.DataAccess
    Test for Dapper and Dapper.SCRUD
    [ Insert, Update, Delete, Select, SelectBy, ByLinq, ByLambda ]
    
    Reference: Dapper - a simple object mapper for .Net
    https://github.com/StackExchange/Dapper
    https://github.com/ericdc1/Dapper.SimpleCRUD
*/
namespace MCS.DataAccess {
    public class UsersData {

        /// <summary>
        /// Insert by Dapper.SCRUD
        /// Return new item's primary key
        /// </summary>        
        public long Insert(UserInfo info) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.Insert<long>(info);
            }
        }

        /// <summary>
        /// Update by Dapper.SCRUD
        /// No return
        /// </summary> 
        public void Update(UserInfo info) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                conn.Update(info);
            }
        }

        /// <summary>
        /// Delete by Dapper.SCRUD
        /// Return delete item
        /// </summary> 
        public int Delete(long id) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.Delete<UserInfo>(id);
            }
        }

        /// <summary>
        /// Delete all by condiction
        /// Return deleted count
        /// </summary> 
        public int DeleteAll() {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.DeleteList<UserInfo>("where Id%2=0");
            }
        }

        /// <summary>
        /// Get by Id by Dapper.SCRUD
        /// Id is must be primary key
        /// </summary> 
        public UserInfo GetById(long id) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.Get<UserInfo>(id);
            }
        }

        /// <summary>
        /// Get Info list by Dapper.SCRUD linq query parameter        
        /// </summary>
        public IEnumerable<UserInfo> GetAll() {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.GetList<UserInfo>("where IsActive=1");
            }
        }

        /// <summary>
        /// Get Info list by pagingion by Dapper.SCRUD linq query parameter        
        /// </summary>
        public IEnumerable<UserInfo> GetByPage(int pageNo, int rowPerPage) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.GetListPaged<UserInfo>(pageNo, rowPerPage, "where IsActive=1", "Id asc");
            }
        }

        /// <summary>
        /// GetList Info by Dapper.SCRUD linq query parameter        
        /// </summary>
        public IEnumerable<UserInfo> GetByRoleByLambda(int roleId) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                return conn.GetList<UserInfo>(new { RoleId = roleId });
            }
        }

        /// <summary>
        /// Get data from join query, return with DataTable.
        /// </summary>        
        public DataTable GetByRoleByQuery(int roleId) {
            using (var conn = ConnectionFactory.GetDBConnection()) {
                var param = new DynamicParameters();
                param.Add("@RoleId", roleId);
                var data = conn.Query<dynamic>("sp_GetUsersByRole", param, commandType: System.Data.CommandType.StoredProcedure);
                return ExtensionMethods.ToDataTable(data);
            }
        }

    }
}
