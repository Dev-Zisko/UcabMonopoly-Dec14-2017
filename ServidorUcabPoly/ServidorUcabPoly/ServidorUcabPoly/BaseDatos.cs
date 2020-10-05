using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace ServidorUcabPoly
{
    class BaseDatos
    {
        string usuario;
        string contraseña;

        public BaseDatos(string usuario, string contraseña)
        {
            this.usuario = usuario;
            this.contraseña = contraseña;
        }
        public BaseDatos() { }

        public MySqlConnection obtenerConexion()
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "15121995";
            builder.Database = "ucabpoly";
            builder.IntegratedSecurity = true;
            conn = new MySqlConnection(builder.ToString());
            return conn;
        }

        public int validarUsuario(string nombre, string clave)
        {
            int valor = 0;
            MySqlConnection conn = obtenerConexion();
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "select 1 from jugador where nick=@nombre and pass=@clave;";
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@clave", clave);
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                valor = int.Parse(reader[0].ToString());
            }

            if (valor == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
