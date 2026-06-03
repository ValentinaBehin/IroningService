using System.Windows;

namespace IroningService.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
{
    // Postavlja kulturu na hrvatsku, koja koristi Euro
    var culture = new System.Globalization.CultureInfo("hr-HR");
    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

    base.OnStartup(e);
}
}