

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLibrosCMVVM2.Model;

namespace WpfLibrosCMVVM2.Conexiones
{
    public class Conexion
    {
        // 1) Creamos una variable de referencia a la cadena de conexion almacenada en la configuración del proyecto
        private string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\WS\\WSDI\\1EV\\appWPF\\cap8\\Librerias.accdb";


        // 2) Variables para recuperar información de la BDD
        private OleDbConnection CONN;
        private OleDbCommand CMD;
        private OleDbDataReader RDR;


        //METODO PARA OBTENER TODOS LOS LIBROS
        public ObservableCollection<CLibro> ObtenerLibros()
        {
            //Instanciamos la variable CONN pasándole a su constructor la variable "cadenaConexion"
            CONN = new OleDbConnection(cadenaConexion);

            //Instanciamos la variable CMD pasándole a su constructor la instrucción OleDb que debe ejecutar
            //así como la variable CONN que le indica en qué BDD debe ejecutar dicha instrucción.
            CMD = new OleDbCommand("SELECT * FROM Libros", CONN);

            //Tipo de conexion
            CMD.CommandType = CommandType.Text;

            //Creamos una colección de objetos CLibro que "envuelve" 
            //a los registros de la tabla que se van a recuperar.
            ObservableCollection<CLibro> listaDeLibros = new ObservableCollection<CLibro>();

            try //Intentamos...
            {
                CONN.Open(); // Abrimos la conexión
                RDR = CMD.ExecuteReader(); //Ejecutamos la consulta SELECT

                while (RDR.Read()) //Recorremos todos los registros uno a uno
                {
                    //Creamos un objeto que envuelve al registro actual
                    CLibro libroActual = new CLibro();
                    libroActual.Titulo = (string)RDR["Titulo"];
                    libroActual.ISBN = (string)RDR["ISBN"];
                    libroActual.Autor = (string)RDR["Autor"];
                    libroActual.Editorial = (string)RDR["Editorial"];

                    //Agregamos el objeto a la colección
                    listaDeLibros.Add(libroActual);
                }
            }
            catch (Exception ex)
            {
                throw ex; // Lanzamos la excepción
            }
            finally
            {
                CONN.Close();
            }

            return listaDeLibros;
        }


        //METODO PARA INSERTAR UN NUEVO LIBRO
        public void GuardarNuevoLibro(CLibro nuevoLibro)
        {
            CONN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand();
            CMD.Connection = CONN;
            CMD.CommandType = CommandType.Text;

            CMD.CommandText = "INSERT INTO Libros (Titulo, ISBN, Autor, Editorial) VALUES (@p1,@p2,@p3,@p4);";

            //Establecemos los valores que tendrán los parámetros del comando CMD
            CMD.Parameters.AddWithValue("@p1", nuevoLibro.Titulo);
            CMD.Parameters.AddWithValue("@p2", nuevoLibro.ISBN);
            CMD.Parameters.AddWithValue("@p3", nuevoLibro.Autor);
            CMD.Parameters.AddWithValue("@p4", nuevoLibro.Editorial);

            Exception exception = null;

            try
            {
                CONN.Open();
                CMD.ExecuteNonQuery();
            } catch (Exception ex)
            {
                exception = ex;
                MessageBox.Show("Error: La clave primaria ya existe.\n" +
                                "Por favor inserte otro ISBN diferente.");
                throw ex;
            }
            finally
            {
                if (exception == null)
                    MessageBox.Show("Registro agregado exitosamente");

                CONN.Close();
            }
        }


        //METODO PARA BORRAR UN LIBRO
        public int BorrarLibro(string isbn)
        {
            CONN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand("DELETE FROM Libros WHERE ISBN = @p0;", CONN);
            CMD.CommandType = CommandType.Text;

            CMD.Parameters.AddWithValue("@p0", isbn);

            try
            {
                CONN.Open();
                return CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CONN.Close();
            }
        }


        //METODO PARA MODIFICAR UN LIBRO
        public int ActualizarLibroExistente(CLibro libroExistente, string isbn)
        {
            CONN = new OleDbConnection(cadenaConexion);
            CMD = new OleDbCommand();
            CMD.Connection = CONN;
            CMD.CommandType = CommandType.Text;

            CMD.CommandText = "UPDATE Libros SET Titulo = @p1, ISBN = @p2, Autor = @p3, Editorial = @p4 " +
                "WHERE ISBN = @p5";

            CMD.Parameters.AddWithValue("@p1", libroExistente.Titulo);
            CMD.Parameters.AddWithValue("@p2", libroExistente.ISBN);
            CMD.Parameters.AddWithValue("@p3", libroExistente.Autor);
            CMD.Parameters.AddWithValue("@p4", libroExistente.Editorial);
            CMD.Parameters.AddWithValue("@p5", isbn);

            try
            {
                CONN.Open();
                return CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CONN.Close();
            }
        }

    }
}
