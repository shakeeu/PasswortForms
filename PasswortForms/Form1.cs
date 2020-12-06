using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using PasswortBibliothek;

namespace PasswortForms
{
    public partial class Form1 : Form
    {

        PasswortVerwaltung mitarbeiter = new PasswortVerwaltung();
        OleDbDataReader dataReader = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataReader = mitarbeiter.laden();
            while (dataReader.Read())
            {
                listBox1.Items.Add(dataReader.GetInt32(0).ToString());
            }
            mitarbeiter.close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataReader = mitarbeiter.select(Convert.ToInt32(listBox1.SelectedItem));
            dataReader.Read();

            labelNummer.Text = dataReader.GetInt32(0).ToString();
            labelName.Text = dataReader.GetString(1);
            mitarbeiter.close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxNeuPass.Text == textBoxBestPass.Text)
            {
                mitarbeiter.update(textBoxNeuPass.Text, Convert.ToInt32(listBox1.SelectedItem));
                MessageBox.Show("Passwort erfolgreich geändert.");
            }
            else
               MessageBox.Show("Passwörter stimmen nicht überein!", "Fehlermeldung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
  
            mitarbeiter.close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
