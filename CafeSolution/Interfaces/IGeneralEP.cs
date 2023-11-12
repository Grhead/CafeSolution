using System;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IGeneralEp
{
    Employee Auth(string login, string password);
}