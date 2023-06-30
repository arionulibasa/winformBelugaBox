using System;
using System.Collections.Generic;
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
        private string auth_key;
        public Form3()
        {
            InitializeComponent();
        }


        public Form3(string content1, string content2, string content3, string content4)
        {
            InitializeComponent();
            username = content1;
            password = content2;
            number = content3;
            auth_key = content4;


        }



        public class SubmissionObject
        {

        }

        public class LoginResponse
        {
            public int id { get; set; }
            public string name { get; set; }
            public string fileName { get; set; }
            public string fileUrl { get; set; }
            public int retryCount { get; set; }
            public int status { get; set; }
            public string date { get; set; }
            public string[] queueLogDetails { get; set; }
            public List<DataAnalysis> acoustics { get; set; }



        }

        public class DataAnalysis
        {
            public int callId { get; set; }
            public string @operator { get; set; } 
            public string fileName { set; get; }
            public string score { get; set; }
            public int channel { get; set; }
            public int startTime { get; set; }
            public int endTime { get; set; }
            public int length { get; set; }
            public int calm { get; set; }
            public int anger { get; set; }
            public int joy { get; set; }
            public int sorrow { get; set; }
            public int vigor { get; set; }
            public string primalEmotion { get; set; }
            public string utcCreatedDate { get; set; }

        }

        public class LoginBody
        {
            public string userName { get; set; }
            public string passWord { get; set; }

            public int id { get; set; }

        }

                
        private void button1_Click(object sender, EventArgs e)
        {


            Dictionary<string, string> HEADER = new Dictionary<string, string>()
            {
                {"authorization", "Bearer " + auth_key }
            };


            LoginBody loginBody = new LoginBody();
            loginBody.userName = username;
            loginBody.passWord = password;
            LoginBody data = JsonConvert.DeserializeObject<LoginBody>(number);
            loginBody.id = data.id;

            string url = "https://belugaboxapiappjptest01.azurewebsites.net/api/queuelog/acoustic/" + loginBody.id;
            string jsonBody = JsonConvert.SerializeObject(loginBody);
            RestRequest req = new RestRequest(url, HttpMethod.GET, "application/json");
            req.Headers = HEADER;
            RestResponse resp = req.Send(jsonBody);
            //MessageBox.Show(resp.StatusCode.ToString());
            //MessageBox.Show(auth_key);

            label1.Text = resp.DataAsString;
            //LoginResponse result = resp.DataFromJson<LoginResponse>();
            //label1.Text = $"id: {result.id}, name: {result.name}, filename: {result.fileName}, fileUrl: {result.fileUrl}, " +
            //    $"retryCount {result.retryCount}, status: {result.status}, date: {result.date}, queueLogDetails: {result.queueLogDetails} " +
            //$"acoustics: [callId: {result.acoustics[0].callId.ToString()}, operator: {result.acoustics[0].@operator}";
            //$"fileName: {result.acoustics[0].fileName.ToString()}, score: {result.acoustics[0].score}, channel:"; 
            //$"{result.acoustics[0].channel.ToString()}, startTime: {result.acoustics[0].startTime.ToString()}, endTime: {result.acoustics[0].endTime.ToString()}, " +
            //$"length: {result.acoustics[0].length.ToString()}, calm: {result.acoustics[0].calm.ToString()}, anger: {result.acoustics[0].anger.ToString()}, " +
            //$"joy: {result.acoustics[0].joy.ToString()}, sorrow: {result.acoustics[0].sorrow.ToString()}, vigor: {result.acoustics[0].vigor.ToString()}," +
            //$"primalEmotion: {result.acoustics[0].primalEmotion.ToString()}, utcCreatedDate: {result.acoustics[0].utcCreatedDate.ToString()}";



        }
    }
}
