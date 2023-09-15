using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Av någon anledning vill programmet spara alla användare/passwords/memberships i txt filer under \source\repos\Lab2-master\Lab2\bin\Debug\net7.0
            // Om jag ville spara i txt filer tillsammans med klasserna var jag tvungen att ange den fulla sökvägen och då skulle ni som rättar få ändra sökvägen för er dator.
            // så jag valde att inte mixtra med det för mycket
            // men uppstår problemet bara kolla under bin/debug/net7.0 och klistra in sökvägarna i store och customer

            // Första gången ni startar programmet stäng av och öppna igen för att få tillgång till alla Knatte, Fnatte, Tjatte konton. Och varje gång ni registrerar nytt konto starta om appen för att få discount aktiverad.
            Store store = new Store();
        }
    }
}