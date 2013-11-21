using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileUpload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytearray = null;

            string name = "xyz.jpeg";

            //    name = FileUpload1.FileName;
            //    Stream stream = FileUpload1.FileContent;
            //    stream.Seek(0, SeekOrigin.Begin);
            //    bytearray = new byte[stream.Length];
            //    int count = 0;
            //    while (count < stream.Length)
            //    {
            //        bytearray[count++] = Convert.ToByte(stream.ReadByte());
            //    }

            //}

            Image im = Image.FromFile("C:\\Users\\trilok.rangan\\Desktop\\RPi\\Images\\Source\\001.jpg");
            bytearray = imageToByteArray(im);


            Uri baseAddress = new Uri("http://localhost:7635/ImageUploadService/FileUpload");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseAddress);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.ContentLength = bytearray.Length;
           
            Stream serverStream = request.GetRequestStream();
            serverStream.Write(bytearray, 0, bytearray.Length);
            serverStream.Close();
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                int statusCode = (int)response.StatusCode;
                StreamReader reader = new StreamReader(response.GetResponseStream());
            }

        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
