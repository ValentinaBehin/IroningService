namespace IroningService.WPF;

public static class UserSession 
{
    // Ovdje čuvamo email prijavljenog korisnika
    public static string TrenutniEmail { get; set; } = string.Empty;

    // Možeš dodati i metodu za brisanje sesije kod odjave
    public static void Odjava()
    {
        TrenutniEmail = string.Empty;
    }
}