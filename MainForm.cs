using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;

namespace CompletionistPrototype
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {

            List<Game> games = LoadGames();
            LoadImages(games);

        }

        private List<Game> LoadGames() 
        {
            List<Game> gameList = new List<Game>();

            // Database connection string
            string connectionString = @"Data Source = localhost\SQLEXPRESS;
                                        Integrated Security = True";

            // Store data from games.txt into a string array
            string[] data = new string[] { };
            string path = @"C:\Users\Michael\Documents\CSC202\CompletionistPrototype\games.txt";
            data = File.ReadAllLines(path).ToArray();

            // Initialize games to store an array of game data
            string[][] games = new string[data.Length][];

            // Loop through data and split each item in the array
            for (int i = 0; i < data.Length; i++)
            {
                games[i] = data[i].Split(',');
            }

            // Opens the database
            SqlConnection SqlCon = new SqlConnection(connectionString);
            SqlCon.Open();
            
            // Add game data to database
            foreach (string[] parts in games) 
            {
                // Checks if the game already exists
                SqlCommand sqlCmd = SqlCon.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "Select * from Games where title='" + parts[0].Trim() + "'";
                int rows = Convert.ToInt32(sqlCmd.ExecuteScalar());

                // rows == 0 -> Game is not in the database
                // Add it
                if (rows == 0)
                {
                    // Call Stored Procedure: AddGame to insert game data
                    SqlCommand sqlCmd2 = new SqlCommand("AddGame", SqlCon);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    sqlCmd2.Parameters.AddWithValue("@title", parts[0].Trim());
                    sqlCmd2.Parameters.AddWithValue("@developer", parts[1].Trim());
                    sqlCmd2.Parameters.AddWithValue("@genre", parts[2].Trim());
                    sqlCmd2.Parameters.AddWithValue("@boxart", parts[3].Trim());
                    sqlCmd2.ExecuteNonQuery();
                }

                // Create classes of each game
                Game obj = new Game(parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), parts[3].Trim());
                gameList.Add(obj);
            }

            // Close connection to database
            SqlCon.Close();

            // Return list of game objects
            return gameList;

        }

        private void LoadImages(List<Game> games) 
        {
            
        }

    }
}
