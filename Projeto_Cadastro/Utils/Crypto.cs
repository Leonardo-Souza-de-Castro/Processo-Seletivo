namespace Projeto_Cadastro.Utils
{
    public class Crypto
    {
        public static string Gerar_Hash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool Comparar(string senhalogin, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senhalogin, hash);
        }
    }
}
