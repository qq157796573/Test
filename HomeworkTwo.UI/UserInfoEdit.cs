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
using HomeworkTwo.Comm.ExtentsMethonds;

namespace HomeworkTwo.UI
{
    public partial class UserInfoEdit : Form
    {
        ISqlHerper sqlHerper = FactoryInfo.CreateSqlHerperExample();
        public UserInfoEdit(UserModer user = null)
        {
            InitializeComponent();
            if (user != null)
            {
                //lblName
                this.lblName.Text = user.Name.GetDisplayName();
                this.lblAccount.Text = user.Account.GetDisplayName();
                this.lblEmail.Text = user.Email.GetDisplayName();
                this.lblPassword.Text = user.Password.GetDisplayName();
                this.lblState.Text = user.State.GetDisplayName();
                this.lblUserType.Text = user.UserType.GetDisplayName();
                this.lblMobile.Text = user.Mobile.GetDisplayName();
                //txtValue
                this.txtName.Text = user.Name;
                this.txtAccount.Text = user.Account;
                this.txtEmail.Text = user.Email;
                this.txtPassword.Text = user.Password;
                this.txtState.Text = user.State.ToString();
                this.txtUserType.Text = user.UserType.ToString();
                this.txtId.Text = user.Id.ToString();
                this.txtMobile.Text = user.Mobile;
                this.dtpCreateTime.Value = user.CreateTime;
                this.txtId.Hide();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserModer user = new UserModer()
            {
                Id = int.TryParse(txtId.Text, out int p) ? p : p,
                Account = this.txtAccount.Text,
                Name = this.txtName.Text,
                Email = this.txtEmail.Text,
                Mobile = this.txtMobile.Text,
                Password = this.txtPassword.Text,
                State = 0,//txtState.Text
                UserType = 1,// txtUserType.Text
                CreateTime = this.dtpCreateTime.Value
            };

            Console.WriteLine("我就是想测试一下数据");            
            Save(user);

        }

        private void Save(UserModer user)
        {
            if (user.Id > 0)//为修改
            {
                //修改时间
                user.LastModifyTime = DateTime.Now;

                bool flag = sqlHerper.Update(user);
                if (flag)
                {
                    MessageBox.Show("修改成功");
                    this.Close();
                }
                else
                    MessageBox.Show("修改失败");

            }
            else
            {
                //创建时间
                user.CreateTime = DateTime.Now;
                bool flag = sqlHerper.Insert(user);
                if (flag)
                {
                    MessageBox.Show("添加成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
        }
    }
}
