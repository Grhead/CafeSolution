using System;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface IGeneralEp
{
     AuthDto Auth(string login, string password);
     string CreateHash(string input);
}