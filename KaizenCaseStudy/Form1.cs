using KaizenCaseStudy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace KaizenCaseStudy
{
    public partial class Form1 : Form
    {
        private const string charset = "ACDEFGHKLMNPRTXYZ234579";
        private const int length = 8;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Olasi karakter dizini olusturulur          
            List<char> possibleChars = new(charset);

            // Olasi karakterler karistirilir   
            Random random = new();
            for (int i = 0; i < possibleChars.Count; i++)
            {
                int j = random.Next(i, possibleChars.Count);
                (possibleChars[j], possibleChars[i]) = (possibleChars[i], possibleChars[j]);
            }

            // Unique code olusturulur          
            StringBuilder result = new();
            while (result.Length < length)
            {
                int remainingLength = length - result.Length;
                int numPossibleChars = Math.Min(remainingLength, possibleChars.Count);
                for (int i = 0; i < numPossibleChars; i++)
                {
                    result.Append(possibleChars[i]);
                }
            }

            textBox1.Text = result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                return;
            }

            // Gelen deger parse edilir
            JArray arrayList = JArray.Parse(richTextBox1.Text);
            //Parse edilen deger dto ya deserialize edilir
            var map = JsonConvert.DeserializeObject<List<ReceiptDto>>(arrayList.ToString());

            if (map is not null)
            {
                richTextBox2.Text = map.FirstOrDefault(b => !string.IsNullOrEmpty(b.locale)).description;
                return;
            }
        }

        private void GenerateNumber()
        {
            //Extra olarak generate number ornegi
            Random res = new();
            String randomNumber = "";

            for (int i = 0; i < length; i++)
            {
                int x = res.Next(charset.Length);
                randomNumber = randomNumber + charset[x];
            }
            textBox1.Text = randomNumber;
            return;
        }
    }
}