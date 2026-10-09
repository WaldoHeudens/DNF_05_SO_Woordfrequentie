namespace Woordfrequentie;

/// <summary>
/// Hulpfuncties om een stuk tekst in woorden te ontleden en de woordfrequentie
/// te bepalen. Bouwt voort op de generieke Teller.
/// </summary>
public static class Tekst
{
    // Een iterator die de woorden uit een tekst een voor een levert. We
    // splitsen op alles wat geen letter of cijfer is, laten lege stukken vallen
    // en zetten elk woord om naar kleine letters. Zo tellen "De" en "de" samen.
    public static IEnumerable<string> SplitsWoorden(string tekst)
    {
        char[] scheidingstekens = BouwScheidingstekens(tekst);

        foreach (string stuk in tekst.Split(scheidingstekens, StringSplitOptions.RemoveEmptyEntries))
        {
            yield return stuk.ToLowerInvariant();
        }
    }

    // Verzamel de tekens waarop we splitsen: alles wat geen letter of cijfer is.
    private static char[] BouwScheidingstekens(string tekst)
    {
        HashSet<char> scheidingstekens = new();
        foreach (char teken in tekst)
        {
            if (!char.IsLetterOrDigit(teken))
            {
                scheidingstekens.Add(teken);
            }
        }

        return [.. scheidingstekens];
    }

    // Tel de woordfrequentie van een volledige tekst. De iterator hierboven
    // voedt rechtstreeks de generieke Teller.
    public static Teller<string> TelWoorden(string tekst)
    {
        Teller<string> teller = new();
        teller.TelAlles(SplitsWoorden(tekst));
        return teller;
    }

    // De meest voorkomende woorden, aflopend gesorteerd op aantal. Bij een
    // gelijk aantal sorteren we alfabetisch, zodat de uitvoer voorspelbaar is.
    public static IReadOnlyList<(string Woord, int Aantal)> MeestVoorkomend(string tekst, int hoeveel)
    {
        Teller<string> teller = TelWoorden(tekst);

        List<(string Woord, int Aantal)> lijst = new();
        foreach ((string woord, int aantal) in teller.Tellingen())
        {
            lijst.Add((woord, aantal));
        }

        lijst.Sort((links, rechts) =>
        {
            int volgorde = rechts.Aantal.CompareTo(links.Aantal);
            return volgorde != 0 ? volgorde : string.CompareOrdinal(links.Woord, rechts.Woord);
        });

        if (hoeveel < lijst.Count)
        {
            return lijst.GetRange(0, hoeveel);
        }

        return lijst;
    }
}
