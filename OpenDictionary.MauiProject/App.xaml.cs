﻿using Microsoft.Maui.Controls;

namespace OpenDictionary.MauiProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}