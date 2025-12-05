namespace ordination_test;
using shared.Model;

[TestClass]
public class ModelklasserTest
{
    
    [TestMethod]
    public void TestDoegnDosisPositiv()
    {
        PN pn = new PN(new DateTime(2025, 11, 28),
            new DateTime(2025, 11, 29),
            10);

        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 28) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 29) });
        
        Assert.AreEqual(10, pn.doegnDosis());

    }
    
     
    [TestMethod]
    public void TestDoegnDosisOverTreDage()
    {
        PN pn = new PN(new DateTime(2025, 11, 28),
            new DateTime(2025, 11, 30),
            5);

        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 28) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 29) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 30) });
        
        Assert.AreEqual(5, pn.doegnDosis());

    }
    
    
    [TestMethod]
    public void TestDoegnDosisUdenforOrdination()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            PN pn = new PN(
                new DateTime(2025, 11, 30), 
                new DateTime(2025, 11, 28), 
                10
            );
            
            pn.doegnDosis();
        });

    }
    
    [TestMethod]
    public void TestDoegnDosis0()
    {
        PN pn = new PN(new DateTime(2025, 11, 28),
            new DateTime(2025, 11, 29),
            0);

        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 28) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 11, 29) });
        
        Assert.AreEqual(0, pn.doegnDosis());
    }
    
    [TestMethod]
    public void TestDagligFastDognDosis()
    {
        Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "ml");

        DagligFast df = new DagligFast(
            new DateTime(2025, 11, 28),
            new DateTime(2025, 11, 29),
            
            lm,
            2, // morgen
            4, // middag
            10, // aften
            2  // nat
        );

        double resultat = df.doegnDosis();

        Assert.AreEqual(18, resultat);
    }
    
    [TestMethod]
    public void TestPNStartDatoErStørreEndSlutDato()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            PN pn = new PN(
                new DateTime(2025, 11, 30), // startDato
                new DateTime(2025, 11, 28), // slutDato
                10                         
            );

            pn.doegnDosis();
        });
    }
    
    [TestMethod]
    public void TestPNDosisPrDoegn()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            PN pn = new PN(
                new DateTime(2025, 11, 30), // startDato
                new DateTime(2025, 11, 28), // slutDato
                10                         
            );

            pn.doegnDosis();
        });
    }
    
    

    
    
    
}