using domain.Entities;
using domain.Enum;
using domain.Interface;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Linq.Expressions;

namespace Migracoes.Repositorios
{
    public class DrinksRepositorio : IDrinkRepositorio
    {
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString))
                    {
                    conn.Open();
                    var query = "DELETE FROM Drink WHERE Id =@Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar bomba");
            }

        }

        public List<Drinks> GetAll()
        {
            var listadeDrinks = new List<Drinks>();
            var comando = "SELECT * FROM Drink";
            try
            {
                var str = ConfigurationManager.ConnectionStrings["conexao"]?.ConnectionString;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Drinks drink = new()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)dr["Nome"],
                            Preco = (double)dr["Preco"],
                            Existe = (bool)dr["Existe"],
                            EhAlcoolica = (bool)dr["EhAlcoolica"],
                            Composicao = Enum.Parse<TipoBase>(dr["Composicao"].ToString()!)
                        };
                        listadeDrinks.Add(drink);
                    }
                }

                return listadeDrinks; }
            catch (Exception ex) { throw new Exception("Erro ao obter os drinks");
            }

        }

        public Drinks GetById(int id)
        {
            var drinks = new Drinks();
            var comando = "SELECT FROM * Drink";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        drinks.Id = Convert.ToInt32(dr["Id"]);
                        drinks.Nome = (string)dr["Nome"];
                        drinks.Preco = (double)dr["Preco"];
                        drinks.Existe = (bool)dr["Existe"];
                        drinks.EhAlcoolica = (bool)dr["EhAlcoolica"];
                        drinks.Composicao = Enum.Parse<TipoBase>((dr["Composicao"].ToString()));
                    }
                } return drinks;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao selecionar drink");
            }
        }

        public void Insert(Drinks model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-0S13DMG\\SQLEXPRESS;Database=Bebidas;TrustServerCertificate=True;User ID=sa;Password=debora806"))
                {
                    conn.Open();
                    var query = "INSERT INTO Drink(Nome,Preco,Existe,EhAlcoolica,Composicao) VALUES(@Nome,@Preco,@Existe,@EhAlcoolica,@Composicao)";
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@Nome", model.Nome);
                        comando.Parameters.AddWithValue("@Preco", model.Preco);
                        comando.Parameters.AddWithValue("@Existe", model.Existe);
                        comando.Parameters.AddWithValue("@EhAlcoolica", model.EhAlcoolica);
                        comando.Parameters.AddWithValue("@Composicao", model.Composicao);
                        comando.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir no Cardápio");
            }
        }

        public void Update(Drinks model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
                {
                    conn.Open();
                    var query = "UPDATE Drinks SET Nome= @Nome,Preco=@Preco,Existe=@Existe,EhAlcoolica=@EhAlcoolica,Composicao=@Composicao WHERE Id=@Id";
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@Nome", model.Nome);
                        comando.Parameters.AddWithValue("@Preco", model.Preco);
                        comando.Parameters.AddWithValue("@Existe", model.Existe);
                        comando.Parameters.AddWithValue("@EhAlcoolica", model.EhAlcoolica);
                        comando.Parameters.AddWithValue("@Composicao", model.Composicao);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { throw new Exception("Erro ao modificar drink no cardápio");
            }
        }
    }
}