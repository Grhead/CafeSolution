using System;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IGeneralEp
{
    string Auth(string login, string password);
}