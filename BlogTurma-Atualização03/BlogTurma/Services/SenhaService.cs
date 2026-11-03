using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;

namespace BlogDaTurma.Services;

public class SenhaService
{
    public string GerarHash(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 3,
            MemoryCost = 65536,
            Lanes = 4,
            Threads = 2,
            Password = Encoding.UTF8.GetBytes(senha),
            Salt = salt
        };
        
        using var argon2 = new Argon2(config);
        using var hash = argon2.Hash();

        return config.EncodeString(hash.Buffer);
    }

    public bool Verificar(string senha, string hashArmazenado)
    {
        return Argon2.Verify(hashArmazenado, senha);
    }
}