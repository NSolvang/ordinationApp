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
        
        Assert.AreEqual(5, pn.doegnDosis());

    }
    
    
    [TestMethod]
    public void TestDoegnDosisUdenforOrdination()
    {
        PN pn = new PN(new DateTime(2025, 11, 30),
            new DateTime(2025, 11, 28),
            10);
        
        Assert.ThrowsException<ArgumentException>(() =>
        {
            pn.doegnDosis();
        });

    }
    
    
    [TestMethod]
    public void TestDoegnDosisNegativ()
    {
        
    }
}