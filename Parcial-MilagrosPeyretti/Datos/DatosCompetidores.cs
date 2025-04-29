using System.Data.SqlClient;
using Parcial_MilagrosPeyretti.Models;

namespace Parcial_MilagrosPeyretti.Datos
{
    public class DatosCompetidores
    {
        string connectionString = @"Data Source=DESKTOP-OA6PBD9;Initial Catalog=Competidores;Integrated Security=True";

        public List<Competidor> ListaCompetidores ()
        {
            List<Competidor> listaCompetidores = new List<Competidor>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT c.Id, c.Nombre, c.IdDisciplina, c.Edad , c.CiudadRecidencia, d.NombreDisciplina " +
                    $"FROM Competidores c JOIN Disciplinas d ON d.Id = c.IdDisciplina";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaCompetidores.Add(new Competidor
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        IdDisciplina = (int)reader["IdDisciplina"],
                        Edad = (int)reader["Edad"],
                        CuidadRecidencia = reader["CiudadRecidencia"].ToString(),
                        Disciplina = new Disciplina
                        {
                            Id = (int)reader["IdDisciplina"],
                            NombreDisciplina = reader["NombreDisciplina"].ToString()
                        }
                    });
                }
                return listaCompetidores;
            }
        }

        public List<Disciplina> ListaDisciplinas()
        {
            List<Disciplina> listaDisciplinas = new List<Disciplina>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Disciplinas";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaDisciplinas.Add(new Disciplina
                    {
                        Id = (int)reader["Id"],
                        NombreDisciplina = reader["NombreDisciplina"].ToString()
                    });
                }
                return listaDisciplinas;
            }
        }

        public string RegistrarCompetidores (Competidor competidor)
        {
            string query = $"INSERT INTO Competidores (Nombre, IdDisciplina, Edad, CiudadRecidencia) VALUES ('{competidor.Nombre}', {competidor.IdDisciplina}, {competidor.Edad}, '{competidor.CuidadRecidencia}')";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    return "";
                }
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
        public List<Disciplina> ListarCantidadDisciplina()
        {
            List<Disciplina> lista = new List<Disciplina>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT d.NombreDisciplina ,COUNT(*) AS Cantidad FROM Competidores C JOIN Disciplinas d  on c.IdDisciplina = d.Id GROUP BY d.NombreDisciplina";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Disciplina()
                    {
                        
                        NombreDisciplina = reader["NombreDisciplina"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"])
                    });
                }
                return lista;
            }
        }


    }
}
