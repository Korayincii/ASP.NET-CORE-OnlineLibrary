using System.Text;

namespace AspNetLibrary.Helpers
{
    public class Helper : IHelper   
    {
        //To hash passwords before inserting into database
        string IHelper.Hash(string pas)
        {
            if (string.IsNullOrEmpty(pas)) return "";
            byte[] storepas = ASCIIEncoding.ASCII.GetBytes(pas);
            string encrypt = Convert.ToBase64String(storepas);
            return encrypt;
        }

    }
}
