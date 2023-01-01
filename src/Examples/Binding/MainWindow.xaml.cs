using Microsoft.UI.Xaml.Controls;
using System;

namespace Binding;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private TestViewModel TestViewModel { get; } = new();
}