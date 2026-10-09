namespace Woordfrequentie;

/// <summary>
/// Telt hoe vaak elke waarde voorkomt. De klasse is generiek: ze werkt met
/// strings, getallen of om het even welk ander type. De constraint
/// "where T : notnull" maakt duidelijk dat een null-sleutel niet de bedoeling
/// is (een Dictionary laat er geen toe); de compiler waarschuwt bij een
/// null-sleutel (CS8714).
/// </summary>
public class Teller<T> where T : notnull
{
    // Onder de motorkap houden we de tellingen bij in een Dictionary:
    // de waarde is de sleutel, het aantal de bijhorende waarde.
    private readonly Dictionary<T, int> _tellingen = new();

    // Aantal verschillende waarden die minstens een keer geteld zijn.
    public int AantalUnieke => _tellingen.Count;

    // Verhoog de telling van een waarde met een. TryGetValue geeft 0 terug
    // als de waarde nog niet bekend is (out-parameter blijft dan de default).
    public void Tel(T waarde)
    {
        _tellingen.TryGetValue(waarde, out int huidig);
        _tellingen[waarde] = huidig + 1;
    }

    // Tel elke waarde uit een reeks. Werkt met elke IEnumerable<T>,
    // dus met een List<T>, een array of een iterator.
    public void TelAlles(IEnumerable<T> waarden)
    {
        foreach (T waarde in waarden)
        {
            Tel(waarde);
        }
    }

    // Hoe vaak kwam een waarde voor? Onbekende waarden geven 0.
    public int Aantal(T waarde)
    {
        _tellingen.TryGetValue(waarde, out int aantal);
        return aantal;
    }

    // Een eigen iterator: levert elk paar (waarde, aantal) een voor een.
    // Dankzij yield return hoeven we geen nieuwe lijst op te bouwen.
    public IEnumerable<(T Waarde, int Aantal)> Tellingen()
    {
        foreach (KeyValuePair<T, int> paar in _tellingen)
        {
            yield return (paar.Key, paar.Value);
        }
    }
}
