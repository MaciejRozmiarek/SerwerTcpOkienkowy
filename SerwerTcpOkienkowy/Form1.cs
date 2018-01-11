using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Net;

namespace SerwerTcpOkienkowy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string connectionString = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", ipBazyDanychTextBox.Text , nazwaBazyDanychtextBox.Text , loginTextBox.Text , hasłoTextBox.Text);
            listBox1.Items.Add(connectionString);

            MySqlDatabase dbConnection = new MySqlDatabase();
            bool status = dbConnection.OpenConnection(connectionString);

            TcpListener server = null;
            MySqlDatabase conn = new MySqlDatabase();
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                // Set the TcpListener on port 13000.
                Int32 port = 5000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string query = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Received query: {0}", query);

                        // Process the data sent by the client.
                        var ds = dbConnection.getDataSet(query);

                        // Send back a response.
                        binaryFormatter.Serialize(stream, ds);

                        Console.WriteLine("Sent.");
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();

                dbConnection.CloseConnection();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", ipBazyDanychTextBox.Text, nazwaBazyDanychtextBox.Text, loginTextBox.Text, hasłoTextBox.Text);
            //listBox1.Items.Add(connectionString);

            MySqlDatabase dbConnection = new MySqlDatabase();
            bool status = dbConnection.OpenConnection(connectionString);

            if (status == true)
            {
                MessageBox.Show("Nawiązałeś połączenie z bazą danych ", "Testowe połączenie do bazy");
                string time = DateTime.Now.ToString("h:mm:ss tt");
                listBox2.Items.Add(time + " Nawiązano testowe połączenie z bazą danych");
                dbConnection.CloseConnection();
            }
            else
            {
                MessageBox.Show(@"Nie nawiązano testowego połączenia z bazą danych ", "Testowe połączenie do bazy");

                string time = DateTime.Now.ToString("h:mm:ss tt");
                listBox2.Items.Add(time + " Nie nawiązano testowego połączenia z bazą danych");
            }

            

            
            
        }
    }
}
