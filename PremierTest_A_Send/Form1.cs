using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PremierTest_A_Send
{
    public partial class Form1 : Form
    {
        private SerialPort com;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.com != null && this.com.IsOpen)
                {
                    this.com.Close();
                }
                string selectedPort = comboBox1.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedPort))
                {
                    MessageBox.Show("Veuillez sélectionner un port COM dans la liste avant d'ouvrir la connexion.",
                        "Port non sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.com = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                this.com.Open();
                MessageBox.Show("Le port COM " + selectedPort + " est ouvert.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.com != null && this.com.IsOpen)
            {
                this.com.Close();
                MessageBox.Show("Le port COM est fermé.");
            }
            else
            {
                MessageBox.Show("Aucun port COM n'est ouvert.");
            }
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsend_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.com == null || !this.com.IsOpen)
                {
                    MessageBox.Show("Le port COM n'est pas ouvert. Impossible d'envoyer des données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                byte[] msg = new byte[2];
                msg[0] = Convert.ToByte(txt1.Text); // Assuming txt1 is a TextBox
                msg[1] = Convert.ToByte(txt2.Text); // Assuming txt2 is a TextBox
                this.com.Write(msg, 0, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi des données : " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] myComPorts = SerialPort.GetPortNames();
            foreach (string port in myComPorts)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

