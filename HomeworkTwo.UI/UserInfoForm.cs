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
                this.dgvUserData.DataSource = sqlHerper.QueryAll<UserModer>();
            else
            {
                UserModer userModer = sqlHerper.QueryOne<UserModer>(int.TryParse(this.txtNo.Text, out int p) ? p : 0);
                DataGridViewRow row = new DataGridViewRow();

                this.dgvUserData.DataSource = new List<UserModer>() { userModer };
                this.dgvUserData.Refresh();
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
            int id= Convert.ToInt32(row.Cells["Id"].Value);
            UserModer user = sqlHerper.QueryOne<UserModer>(id);
            UserInfoEdit userInfoEdit = new UserInfoEdit(user);
            userInfoEdit.Text = "用户信息编辑"; 
            userInfoEdit.Show();
        }
    }
}
