using Woordfrequentie;

namespace Woordfrequentie.Tests;

public class TellerTests
{
    [Fact]
    public void Tel_TeltHerhalingenOp()
    {
        Teller<string> teller = new();
        teller.Tel("appel");
        teller.Tel("appel");
        teller.Tel("peer");

        Assert.Equal(2, teller.Aantal("appel"));
        Assert.Equal(1, teller.Aantal("peer"));
    }

    [Fact]
    public void Aantal_OnbekendeWaarde_GeeftNul()
    {
        Teller<string> teller = new();
        teller.Tel("appel");

        Assert.Equal(0, teller.Aantal("banaan"));
    }

    [Fact]
    public void AantalUnieke_TeltVerschillendeWaarden()
    {
        Teller<string> teller = new();
        teller.Tel("appel");
        teller.Tel("appel");
        teller.Tel("peer");

        Assert.Equal(2, teller.AantalUnieke);
    }

    [Fact]
    public void TelAlles_WerktMetEenReeks()
    {
        Teller<int> teller = new();
        teller.TelAlles([1, 2, 2, 3, 3, 3]);

        Assert.Equal(1, teller.Aantal(1));
        Assert.Equal(2, teller.Aantal(2));
        Assert.Equal(3, teller.Aantal(3));
        Assert.Equal(3, teller.AantalUnieke);
    }
}
