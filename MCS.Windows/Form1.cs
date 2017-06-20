using MCS.DataAccess;
using MCS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace MCS.Windows {
    public partial class Form1 : Form {
        int mark = 1;

        public Form1() {
            InitializeComponent();
        }

        private void BindGridViewData(object obj) {
            dataGridView1.DataSource = obj;
            dataGridView1.Refresh();
        }

        private void btnInsert_Click(object sender, EventArgs e) {
            var roleId = 1;
            var roleData = new RolesData();
            if (roleData.GetAllCount() <= 0){
              roleId = roleData.Insert(
                    new RoleInfo(){
                        Name = "Admin",
                        Description = "Administrator",
                        IsActive = true
                });
            }

            var info = new UserInfo() {
                FirstName = string.Format("FN{0}", mark),
                LastName = string.Format("LN{0}", mark),
                DateOfBirth = DateTime.Now.AddYears(-mark),
                RoleId = roleId,
                IsActive = true
            };
            var returnKey = new UsersData().Insert(info);
            MessageBox.Show("Insert successful. Data key:" + returnKey);
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtId.Text)) { txtId.Focus(); return; }

            var objUsersData = new UsersData();
            var info = objUsersData.GetById(Convert.ToInt64(txtId.Text));
            info.LastName = info.LastName + " - Updated";
            objUsersData.Update(info);
            MessageBox.Show("Update successful");
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtId.Text)) { txtId.Focus(); return; }

            var userId = Convert.ToInt64(txtId.Text);
            var deleteReturn = new UsersData().Delete(userId);
            MessageBox.Show("Delete successful. Delete count:" + deleteReturn);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e) {
            var deleteReturn = new UsersData().DeleteAll();
            MessageBox.Show("Delete successful. Delete count:" + deleteReturn);
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            var data = new UsersData().GetAll();
            BindGridViewData(data);
        }

        private void btnLoadPerPage_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtPageNo.Text)) { txtPageNo.Focus(); return; }

            var data = new UsersData().GetByPage(Convert.ToInt16(txtPageNo.Text), 3);
            BindGridViewData(data);
        }

        private void btnLoadUserByRoleId_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtRoleId.Text)) { txtRoleId.Focus(); return; }

            var roleId = Convert.ToInt32(txtRoleId.Text);
            var data = new UsersData().GetByRoleByQuery(roleId);         
            BindGridViewData(data);
        }
    }
}
