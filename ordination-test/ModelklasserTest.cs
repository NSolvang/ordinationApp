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
        pn.doegnDosis();

        Assert.Equals(5,pn.doegnDosis());

    }
    
    
    [TestMethod]
    public void TestDoegnDosisUdenforOrdination()
    {
        
    }
    
    
    [TestMethod]
    public void TestDoegnDosisNegativ()
    {
        
    }
}