using CafeSolutionWPF.DTO;

namespace CafeSolutionWPF.Interfaces;

public interface IGeneralEp
{
    AuthDto Auth(string login, string password);
    string CreateHash(string input);
}