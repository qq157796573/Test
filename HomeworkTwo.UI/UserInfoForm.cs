﻿using HomeworkTwo.Comm.ExtentsMethonds;
using HomeworkTwo.Factory;
using HomeworkTwo.IDal;
using HomeworkTwo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HomeworkTwo.Comm.EnumComm;

namespace HomeworkTwo.UI
{
    public partial class UserInfoForm : Form
    {
        public UserInfoForm()
        {
            InitializeComponent();
        }
        ISqlHerper sqlHerper = FactoryInfo.CreateSqlHerperExample();
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNo.Text))
                this.dgvUserData.DataSource = sqlHerper.QueryAll<UserModel>();
            else
            {
                UserModel userModer = sqlHerper.QueryOne<UserModel>(int.TryParse(this.txtNo.Text, out int p) ? p : 0);
                if (userModer != null)
                {
                    this.dgvUserData.DataSource = new List<UserModel>() { userModer };
                }
                else
                {
                    this.dgvUserData.DataSource = null;
                }
            }
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dgvUserData.CurrentRow;
            int id = Convert.ToInt32(row.Cells["Id"].Value);
            UserModel user = sqlHerper.QueryOne<UserModel>(id);
            UserInfoEdit userInfoEdit = new UserInfoEdit(user, () => 
            {
                this.btnSelect.Click += BtnSelect_Click;
                this.btnSelect.PerformClick();
                this.btnSelect.Click -= BtnSelect_Click;
            });
            userInfoEdit.Text = "用户信息编辑";
            userInfoEdit.Show();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            this.btnSelect_Click(sender, e);
        }
    }
}
