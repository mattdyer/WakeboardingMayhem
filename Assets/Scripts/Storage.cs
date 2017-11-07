using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Storage : MonoBehaviour {
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/drive-dotnet-quickstart.json
    string[] Scopes = {  DriveService.Scope.DriveReadonly };
    string ApplicationName = "Wakeboarding Mayhem";
    Task<UserCredential> credential;

    //client id
    string client_id = "656522469414-4bvr6p7mhu5p627t0bijjvugrfv8a21a.apps.googleusercontent.com";

    //client secret
    string client_secret = "Q3W6eF1oge_6jqR1nBVAi9FK";

    void Start(){
        Debug.Log("started");

    }

    private async Task<UserCredential> getCredentials(){
        //FileDataStore dataStore = new FileDataStore("",true);

        ClientSecrets secrets = new ClientSecrets();

        secrets.ClientId = client_id;
        secrets.ClientSecret = client_secret;

        CancellationTokenSource cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(20));
        CancellationToken ct = cts.Token;

        var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                Scopes,
                "user",
                ct);

        return result;
    }

    /*private UserCredential getCredentialsCodeFlow(){
        
        var token = new TokenResponse { RefreshToken = "abc" };

        ClientSecrets secrets = new ClientSecrets();

        secrets.ClientId = client_id;
        secrets.ClientSecret = client_secret;

        var credentials = new UserCredential(new GoogleAuthorizationCodeFlow(
            new GoogleAuthorizationCodeFlow.Initializer 
            {
                ClientSecrets = secrets
            }), 
            "user", 
            token);

        return credentials;
    }*/

    public void StoreValue(string valueToStore)
    {
        

        var credentials = getCredentials().Result;

        // Create Drive API service.
        var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName,
            });

        // Define parameters of request.
        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";

        // List files.
        IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            .Files;
        Console.WriteLine("Files:");
        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                Console.WriteLine("{0} ({1})", file.Name, file.Id);
            }
        }
        else
        {
            Console.WriteLine("No files found.");
        }
        Console.Read();

    }
}
