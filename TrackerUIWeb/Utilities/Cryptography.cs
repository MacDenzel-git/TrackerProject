using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace TrackerUIWeb.Utilities
{

    public class Cryptography
    {
        private MD5 md5Hash = MD5.Create();
        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            int i = 0;
            for (i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();

        }
        private string HashPassword(string Password)
        {
            //Requires
            //Install System.Text.Encoding.CodePages
            //Register in Program.cs -- Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                string sBuffer = "";
                int l = 0;
                for (l = 1; l <= Strings.Len(Password); l++)
                {
                    sBuffer += Strings.Chr(Strings.Asc(Strings.Mid(Password, l, 1)) ^ 200);
                }
            return sBuffer;

            }
            catch (Exception e)
            {
                
                throw;
            }
        }
        public string EncryptText(string pass)
        {
            string tier1 = null;
            string tier2 = null;

            tier1 = GetMd5Hash(md5Hash, pass);
            tier2 = HashPassword(tier1);

            return tier2;
        }

        public string CoDecodeXOR(string Cadena)
        {
            string sBuffer = "";
            int l = 0;

            for (l = 1; l <= Strings.Len(Cadena); l++)
            {
                sBuffer += Strings.Chr(Strings.Asc(Strings.Mid(Cadena, l, 1)) ^ 200);
            }

            return sBuffer;
        }
    }
}
