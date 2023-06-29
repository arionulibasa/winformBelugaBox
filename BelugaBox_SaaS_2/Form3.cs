using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestWrapper;

namespace BelugaBox_SaaS_2
{
    public partial class Form3 : Form
    {

        private string username;
        private string password;
        private string number;
        public Form3()
        {
            InitializeComponent();
        }


        public Form3(string content1, string content2, string content3)
        {
            InitializeComponent();
            username = content1;
            password = content2;
            number = content3;


        }



        public class SubmissionObject
        {

        }

        public class LoginResponse
        {
            public string submissionStatus { get; set; }
            public string reason { get; set; }

            public string message2 { get; set; }

            public string token { get; set; }

            public SubmissionObject submissionObject { get; set; }

            public bool isSubmissionSuccess { get; set; }
        }



        public class LoginBody
        {
            public string userName { get; set; }
            public string passWord { get; set; }

            public int id { get; set; }

        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            LoginBody loginBody = new LoginBody();
            loginBody.userName = username;
            loginBody.passWord = password;
            LoginBody data = JsonConvert.DeserializeObject<LoginBody>(number);
            loginBody.id = data.id;

            string url = "https://belugaboxapiappjpdevl01.azurewebsites.net/api/queuelog/acoustic/" ;
            string jsonBody = JsonConvert.SerializeObject(loginBody);
            RestRequest req = new RestRequest(url, HttpMethod.GET, "application/json");
            RestResponse resp = req.Send(jsonBody);
            LoginResponse result = resp.DataFromJson<LoginResponse>();

        }
    }
}
