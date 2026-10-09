using Woordfrequentie;

string tekst = """
    De kat zat op de mat.
    De hond zat naast de kat.
    De mat was warm.
    """;

Console.WriteLine("Top 3 meest voorkomende woorden:");
foreach ((string woord, int aantal) in Tekst.MeestVoorkomend(tekst, 3))
{
    Console.WriteLine($"  {woord}: {aantal}");
}

Teller<string> teller = Tekst.TelWoorden(tekst);
Console.WriteLine($"Aantal unieke woorden: {teller.AantalUnieke}");
Console.WriteLine($"Aantal keer 'de': {teller.Aantal("de")}");
