﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CafeSolutionWPF;

public class UpdateProperty : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}