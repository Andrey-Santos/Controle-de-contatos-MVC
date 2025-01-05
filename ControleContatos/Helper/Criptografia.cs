using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            var hash = SHA1.Create();
            var encode = new ASCIIEncoding();
            var array = Encoding.ASCII.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("X2"));
            }

            return strHexa.ToString();
        }
    }
}
