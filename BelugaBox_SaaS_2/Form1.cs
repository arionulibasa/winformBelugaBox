using Newtonsoft.Json;
using RestWrapper;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace BelugaBox_SaaS_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class LoginBody
        {
            public string userName { get; set; }
            public string password { get; set; }
        }


        public class SubmissionObject
        { 
            public string accessToken { get; set; }
            public string refreshToken { get; set; }
            public int accessTokenExpiryInSecond { get; set; }
            public string createdUtcDate { get; set; }

        }


        public class LoginResponse
        {
            public string submissionStatus { get; set; }
            public string reason { get; set; }

            public string message { get; set; }

            public SubmissionObject submissionObject { get; set; }

            public bool isSubmissionSuccess { get; set; }
        }

        public class InvalidAccess
        {
            public string submissionStatus { get; set; } = "failed";
            public string reason { get; set; } = "ユーザ名もしくはパスワードが不適切です";
            public string message { get; set; }
            public SubmissionObject submissionObject { get; set; } = null;

            public bool isSubmissionSuccess { get; set; } = false;
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            string usernameVal = textBox1.Text;
            string passwordVal = textBox2.Text;

            LoginBody loginBody = new LoginBody();
            loginBody.userName = usernameVal;
            loginBody.password = passwordVal;

            string url = "https://employeeauthwebapijptest01.azurewebsites.net/api/auth/signin";
            string jsonBody = JsonConvert.SerializeObject(loginBody);
            //MessageBox.Show(jsonBody);


            RestRequest req = new RestRequest(url, HttpMethod.POST, "application/json");
            RestResponse resp = req.Send(jsonBody);
            //MessageBox.Show(resp.StatusCode.ToString());
            LoginResponse result = resp.DataFromJson<LoginResponse>();
           
            //MessageBox.Show(result.submissionStatus);
            //string auth_token = result.submissionobject.accesstoken;


            if (result.submissionStatus == "success")
            {
                //MessageBox.Show(result.submissionObject.accessToken);
                this.Hide();
                Form2 form2 = new Form2(result.submissionObject.accessToken);
                form2.Show();

            }
            
            else
            {
                InvalidAccess resultt = resp.DataFromJson<InvalidAccess>();
                MessageBox.Show(resultt.reason);
            }

        }

    }
}
