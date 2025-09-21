using Npgsql;

namespace ControleEstoque
{
    public class Conexao
    {
        private const string connString = "Host=localhost;Port=5432;Username=postgres;Password=Lucasrt2190;Database=controle_estoque";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}