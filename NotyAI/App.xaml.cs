﻿using Microsoft.Maui.Controls;

namespace NotyAI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}