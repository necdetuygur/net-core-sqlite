using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            connectionStringBuilder.DataSource = "./SqliteDB.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Ulkeler";
                delTableCmd.ExecuteNonQuery();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Ulkeler(Ad VARCHAR(50))";
                createTableCmd.ExecuteNonQuery();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = "INSERT INTO Ulkeler VALUES('Türkiye')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO Ulkeler VALUES('Almanistan')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO Ulkeler VALUES('Japonistan')";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Ad FROM Ulkeler";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        MessageBox.Show(message);
                    }
                }
            }

            Application.Exit();
        }
    }
}
