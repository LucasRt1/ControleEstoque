using Npgsql;
using System.Data;

namespace ControleEstoque
{
    public class ProdutoDAO
    {
        public void CadastrarProduto(string nome, decimal preco, int quantidade)
        {
            using (var conn = Conexao.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO produto (nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void IncrementarQuantidade(int idProduto, int qtd)
        {
            using (var conn = Conexao.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE produto SET quantidade = quantidade + @qtd WHERE id_produto = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@qtd", qtd);
                    cmd.Parameters.AddWithValue("@id", idProduto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DecrementarQuantidade(int idProduto, int qtd)
        {
            using (var conn = Conexao.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE produto SET quantidade = GREATEST(quantidade - @qtd, 0) WHERE id_produto = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@qtd", qtd);
                    cmd.Parameters.AddWithValue("@id", idProduto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ListarProdutos()
        {
            DataTable tabela = new DataTable();

            using (var conn = Conexao.GetConnection())
            {
                conn.Open();
                string sql = "SELECT id_produto, nome, preco, quantidade FROM produto";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    tabela.Load(reader);
                }
            }

            return tabela;
        }

        public int VerificarProdutoExistente(string nome)
        {
            using (var conn = Conexao.GetConnection())
            {
                conn.Open();
                string sql = "SELECT id_produto FROM produto WHERE nome = @nome";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
    }
}