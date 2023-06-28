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
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;

namespace BelugaBox_SaaS_2
{
    public partial class Form2 : Form
    {
        private string auth_key;
        private static HttpClient _client = null;


        public Form2()
        {
            InitializeComponent();
            
        }

        public Form2(string content)
        {
            InitializeComponent();
            auth_key = content;
            
        }



        public class UploadBody
        { 

            public string userName { get; set; }
            public Form1.SubmissionObject submissionObject { get; set; }


        
        }


        public class UploadResponse
        {
            public int id { get; set; }

            public string userName { get; set; }


            public string submissionStatus { get; set; }
            public string reason { get; set; }

            public string message2 { get; set; }

            public bool submissionObject { get; set; }

            public bool isSubmissionSuccess { get; set; }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string usernameVal = textBox1.Text;
            string customerID = textBox2.Text;
            string keyValue = textBox3.Text;
            string customerName = textBox4.Text;

            DateTime currentTime = DateTime.Now;


            Dictionary<string, string> HEADER = new Dictionary<string, string>()
            {
                {"authorization", "Bearer " + auth_key },
                { "UserName", usernameVal},
                { "CustomerId", customerID },
                { "CustomerName", customerName },
                { "KeyValue", keyValue },
                { "UploadDate", currentTime.ToString()}
            };


            string url = "https://belugaboxuploadapiappjptest01.azurewebsites.net/api/blob/upload/";
            string filePath = "C:/Users/Intern-FOS/Documents/wav_files/demo1_stereo.wav";

            Dictionary<string, string> wavFile = new Dictionary<string, string>();



            var fileData = File.ReadAllBytes(filePath);
            String base64Encoded = Convert.ToBase64String(fileData);

            var requestContent = new MultipartFormDataContent();
            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(fileData);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("audio/wav");

            requestContent.Add(imageContent, "File", "demo1_stereo.wav");

            var client = new HttpClient
            {
                BaseAddress = new Uri(url),
            };

            using (var requestMessage =
            new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth_key);
                requestMessage.Headers.Add("UserName" ,usernameVal);
                requestMessage.Headers.Add( "CustomerId", customerID );
                requestMessage.Headers.Add( "CustomerName", customerName );
                requestMessage.Headers.Add( "KeyValue", keyValue );
                requestMessage.Headers.Add( "UploadDate", currentTime.ToString());
                requestMessage.Content = requestContent;
                using (var response = client.SendAsync(requestMessage).Result)
                {
                    var result = response.Content.ReadAsStringAsync();
                    MessageBox.Show(result.Result);
                }
                
                

               
            }

           

            //wavFile.Add("File", base64Encoded);


            ////UploadBody uploadBody = new UploadBody();
            //RestRequest req = new RestRequest(url, HttpMethod.POST);
            //req.Headers = HEADER;
            ////req.Headers.Add("Content-Type", "multipart/form-data");
            //RestResponse resp = req.Send(wavFile);



            //MessageBox.Show(resp.StatusCode.ToString());


        }
    }
}
