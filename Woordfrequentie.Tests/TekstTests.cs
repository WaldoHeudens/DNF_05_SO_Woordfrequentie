using Woordfrequentie;

namespace Woordfrequentie.Tests;

public class TekstTests
{
    [Fact]
    public void SplitsWoorden_SplitstOpLeestekensEnSpaties()
    {
        List<string> woorden = new(Tekst.SplitsWoorden("Hallo, wereld! Hallo?"));

        Assert.Equal(["hallo", "wereld", "hallo"], woorden);
    }

    [Fact]
    public void SplitsWoorden_ZetOmNaarKleineLetters()
    {
        List<string> woorden = new(Tekst.SplitsWoorden("De DE de"));

        Assert.Equal(["de", "de", "de"], woorden);
    }

    [Fact]
    public void TelWoorden_TeltHoofdletterongevoelig()
    {
        Teller<string> teller = Tekst.TelWoorden("De kat en de hond en DE muis");

        Assert.Equal(3, teller.Aantal("de"));
        Assert.Equal(2, teller.Aantal("en"));
        Assert.Equal(1, teller.Aantal("kat"));
    }

    [Fact]
    public void MeestVoorkomend_GeeftTopGesorteerdOpAantal()
    {
        string tekst = """
            De kat zat op de mat.
            De hond zat naast de kat.
            De mat was warm.
            """;

        IReadOnlyList<(string Woord, int Aantal)> top = Tekst.MeestVoorkomend(tekst, 3);

        Assert.Equal(3, top.Count);
        Assert.Equal(("de", 5), top[0]);
        // Bij een gelijk aantal (2) sorteren we alfabetisch: kat voor mat.
        Assert.Equal(("kat", 2), top[1]);
        Assert.Equal(("mat", 2), top[2]);
    }

    [Fact]
    public void MeestVoorkomend_VraagtMeerDanBeschikbaar_GeeftAlles()
    {
        IReadOnlyList<(string Woord, int Aantal)> top = Tekst.MeestVoorkomend("een twee twee", 10);

        Assert.Equal(2, top.Count);
        Assert.Equal(("twee", 2), top[0]);
        Assert.Equal(("een", 1), top[1]);
    }
}
