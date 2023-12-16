using System;
using CafeSolution.DTO;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IGeneralEp
{
    AuthDto Auth(string login, string password);
}