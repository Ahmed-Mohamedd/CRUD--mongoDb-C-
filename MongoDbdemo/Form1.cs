using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.IO;



namespace MongoDbdemo
{
    public partial class Form1 : Form
    {

        mongoCrud db = new mongoCrud("Registeration");
        ErrorProvider errorProvider = new ErrorProvider();
        string yourGender = "";
        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  // insert button
        {
            Users user = new Users
            {
                firstName = textBox1.Text,
                lastName = textBox2.Text,
                email = textBox3.Text,
                address = textBox5.Text,
                dateTime = dateTimePicker1.Value,
                phoneNumber = int.Parse(textBox4.Text),
                Gender = yourGender ,
                img = imgToBytes(pictureBox1.Image)

            };
            db.insertRecord("users", user);
            
        }

        private void button2_Click(object sender, EventArgs e)  //  show button
        {

            var recs = db.readRecord<Users>("users");
            dg.DataSource = recs;
        }

        private void button3_Click(object sender, EventArgs e)  // update button
        {
           
            db.updateRecord<Users>("users", textBox1.Text ,textBox2.Text , dateTimePicker1.Value  , textBox3.Text ,textBox5.Text ,  int.Parse(textBox4.Text) , yourGender , imgToBytes(pictureBox1.Image));
            var recs = db.readRecord<Users>("users");
            dg.DataSource = recs;


        }

        private void button4_Click(object sender, EventArgs e)  // delete button
        {
            db.deleteRecord<Users>("users", textBox1.Text);
            var recs = db.readRecord<Users>("users");
            dg.DataSource = recs;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            yourGender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            yourGender = "Female";
        }

        private void textBox1_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text=="Enter ur first name")
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider.SetError(textBox1, "please enter a your first name!");
                button1.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                button1.Enabled = true;
                errorProvider.SetError(textBox1, null);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter ur first name")
            {
                textBox1.Text ="";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text ="Enter ur first name";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter ur last name")
            {
                textBox2.Text ="";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text ="Enter ur last name";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Someone@example.com")
            {
                textBox3.Text ="";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text ="Someone@example.com";
                textBox3.ForeColor = Color.Silver;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Phone Number")
            {
                textBox4.Text ="";
                textBox4.ForeColor = Color.Black;
            }
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

            if (textBox4.Text == "")
            {
                textBox4.Text ="Phone Number";
                textBox4.ForeColor = Color.Silver;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Write ur address")
            {
                textBox5.Text ="";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text ="Write ur address";
                textBox5.ForeColor = Color.Silver;
            }
        }

        private void button5_Click(object sender, EventArgs e) // button is used to select an img from pc to display in picture box
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select img";
                dlg.Filter = "jpg files (*.jpg ) | *.jpg";
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }
        }
        public byte[] imgToBytes(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
