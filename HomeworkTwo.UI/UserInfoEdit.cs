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
using HomeworkTwo.Comm.Model;

namespace HomeworkTwo.UI
{
    public partial class UserInfoEdit : Form
    {
        ISqlHerper sqlHerper = FactoryInfo.CreateSqlHerperExample();
        private Action _Action = null;
        public UserInfoEdit(UserModel model = null,Action action=null)
        {
            _Action = action;
            InitializeComponent();
            if (model != null)
            {
                //lblName
                this.lblName.Text = ExtentMethond.MyLabelFor<UserModel, string>(m => m.Name);
                this.lblAccount.Text = ExtentMethond.MyLabelFor<UserModel, string>(m => m.Account);
                this.lblEmail.Text = ExtentMethond.MyLabelFor<UserModel, string>(m => m.Email);
                this.lblPassword.Text = ExtentMethond.MyLabelFor<UserModel, string>(m => m.Password);
                this.lblState.Text = ExtentMethond.MyLabelFor<UserModel, int>(m => m.Status);
                this.lblUserType.Text = ExtentMethond.MyLabelFor<UserModel, int>(m => m.UserType);
                this.lblMobile.Text = ExtentMethond.MyLabelFor<UserModel, string>(m => m.Mobile);
                this.lblCreateTime.Text = ExtentMethond.MyLabelFor<UserModel, DateTime>(m => m.CreateTime);
                //txtValue

                this.txtName.Text = model.Name;
                this.txtAccount.Text = model.Account;
                this.txtEmail.Text = model.Email;
                this.txtPassword.Text = model.Password;
                this.txtState.Text = model.Status.ToString();
                this.txtUserType.Text = model.UserType.ToString();
                this.txtId.Text = model.Id.ToString();
                this.txtMobile.Text = model.Mobile;
                this.dtpCreateTime.Value = model.CreateTime;
                this.txtId.Hide();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserModel user = new UserModel()
            {
                Id = int.TryParse(txtId.Text, out int p) ? p : p,
                Account = this.txtAccount.Text,
                Name = this.txtName.Text,
                Email = this.txtEmail.Text,
                Mobile = this.txtMobile.Text,
                Password = this.txtPassword.Text,
                Status = 0,//txtState.Text
                UserType = 1,// txtUserType.Text
                CreateTime = this.dtpCreateTime.Value
            };

            Console.WriteLine("我就是想测试一下数据");
            Save(user);

        }

        private void Save(UserModel user)
        {
            if (user.Id > 0)//为修改
            {
                //修改时间
                user.LastModifyTime = DateTime.Now;

                IEnumerable<ValidataErrorModel> list = user.Validata();
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        MessageBox.Show(item.ErrorMsg);
                    }
                    return;
                }


                bool flag = sqlHerper.Update(user);
                if (flag)
                {
                    MessageBox.Show("修改成功");
                    this.Close();
                    Console.WriteLine("Update 11111");
                    _Action.Invoke();
                    Console.WriteLine("Update 2222");
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
