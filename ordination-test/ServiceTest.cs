namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void OpretDagligSkæv()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
        
        int initialCount = service.GetDagligSkæve().Count(); 
        
        Dosis[] doser = new Dosis[]
        {
            new Dosis(DateTime.Now.AddHours(12).AddMinutes(0), 0.5), 
            new Dosis(DateTime.Now.AddHours(12).AddMinutes(40), 1),
            new Dosis(DateTime.Now.AddHours(16).AddMinutes(0), 2.5),
            new Dosis(DateTime.Now.AddHours(18).AddMinutes(45), 3)
        };
        
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId, doser, DateTime.Now, DateTime.Now.AddDays(3));
        
        Assert.AreEqual(initialCount + 1, service.GetDagligSkæve().Count(), "Antallet af DagligSkæv ordinationer burde stige med én.");
    }
    
    

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
    
    [TestMethod]
    public void TestOpretPNMedKorrektAntalEnheder()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
    

        PN nyPN = service.OpretPN(1, 1, 5, DateTime.Now, DateTime.Now.AddDays(3));
    

        Assert.AreEqual(5, nyPN.antalEnheder);
    }

    [TestMethod]
    public void TestOpretPNPåPatientDerIkkeFindes()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            service.OpretPN(0, 1, 5, DateTime.Now, DateTime.Now.AddDays(3));
        });
    }

    [TestMethod]
    public void TestOpretPNPåLaegemiddelDerIkkeFindes()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            service.OpretPN(1, 0, 5, DateTime.Now, DateTime.Now.AddDays(3));
        });
    }
    

}