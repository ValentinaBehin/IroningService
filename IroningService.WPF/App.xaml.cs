using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace IroningService.WPF;

public partial class App : Application
{
    public App()
    {
        // 1. Sprječava da se aplikacija ugasi čim se LoginWindow zatvori
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        // 2. Postavlja kulturu na hrvatsku
        var culture = new CultureInfo("hr-HR");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        base.OnStartup(e);
    }
}