#region Imports

using System;
using System.Collections;
using System.Net;
using Jayrock;
using AgileAPI;
using Jayrock.Json;

#endregion
/*
 * This program is a demo tool to show how to use the API 
 * It should not be used for production.
 * 
 */
internal sealed class Program
{
    [STAThread]
    static void Main()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        Console.Write("File To Upload: ");
        string fileToSend = Console.ReadLine();
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;          
        AgileAPIClient client = new AgileAPIClient();
        //Set the base URL
        client.Url = "https://api.agile.lldns.net";
        //Override existing Content types or add new
        client.ContentTypeMappings[".mp3"] = "audio/mpeg";
        //Authenticate to the api
        client.Authenticate(username, password);
        client.logout();
        try
        {
            var statout = client.stat("/" + fileToSend, true);
            Console.WriteLine(statout);
        }
        catch (ApiException e)
        {
            Console.WriteLine("file doesn't exist, uplaoding file");
            Console.WriteLine(e);
            byte[] response = client.upload(fileToSend, "/");
        }
        
        Console.WriteLine(client.listDir("/"));
        //byte[] response =  client.upload(fileToSend, "/");
        Console.WriteLine(client.listFile("/"));
        Console.ReadLine();

    }
}
