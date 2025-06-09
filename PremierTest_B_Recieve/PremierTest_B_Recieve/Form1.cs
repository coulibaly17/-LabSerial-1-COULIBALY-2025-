using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace PremierTest_B_Recieve
{
    public partial class Form1 : Form
    {
        private SerialPort com;

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear(); 
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }

                if (ports.Length == 0)
                {
                    MessageBox.Show("Aucun port COM détecté.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez d'abord cliquer sur 'List' puis sélectionner un port COM.");
                    return;
                }

                string selectedPort = comboBox1.SelectedItem.ToString();
                com = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                com.ReceivedBytesThreshold = 2;
                com.DataReceived += Decript;
                com.Open();
                MessageBox.Show("Connexion ouverte sur " + selectedPort + " !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        
        private void Decript(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (com != null && com.IsOpen && com.BytesToRead >= 2)
                {
                    int info1 = com.ReadByte();
                    int info2 = com.ReadByte();

                    this.Invoke(new Action(() =>
                    {
                        textBox1.Text = info1.ToString();
                        textBox2.Text = info2.ToString();
                        label2.Text = "Heure : " + DateTime.Now.ToString("HH:mm:ss");
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Erreur réception : " + ex.Message);
                }));
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (com != null && com.IsOpen)
            {
                com.Close();
                MessageBox.Show("Connexion fermée !");
            }
        }

        
        private void label1_Click(object sender, EventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
