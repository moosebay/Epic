using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static RestClient RestClient = new RestClient("https://heraclitus.herokuapp.com/");
        static string uid = Guid.NewGuid().ToString();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            CallLogin();
        }
      
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CallLogin();
            }
        }

        private void CallLogin()
        {
            RestRequest restRequest = new RestRequest("api/login", Method.POST);
            restRequest.AddJsonBody(new
            {
                username = txtUsername.Text,
                password = txtPassword.Text,
                uid = "123"
            });

            var result = RestClient.ExecuteAsync<object>(restRequest).ConfigureAwait(false).GetAwaiter().GetResult();

            if (result.IsSuccessful)
            {
                this.Hide();
                var form2 = new Dashboard();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
            else
            {
                MessageBox.Show("Invalid");
            }
        }
    }
}
