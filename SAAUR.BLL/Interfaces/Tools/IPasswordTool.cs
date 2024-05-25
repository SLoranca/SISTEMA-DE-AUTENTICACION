using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Tools
{
    public interface IPasswordTool
    {
        string Generate(int min_length, int max_length);
        ModelPasswordHash GenerateSalt(string pass);
        bool VerifyHashPassword(string pass, string hash, string salt);
        string CryptoEncrypt(string _cadenaAencriptar);
        string CryptoDecrypt(string _cadenaAdesencriptar);
    }
}
