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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using UnityEngine;

public class Storage : MonoBehaviour {
    
    string[] Scopes = {  DriveService.Scope.DriveAppdata, DriveService.Scope.DriveReadonly };
    string ApplicationName = "Wakeboarding Mayhem";
    Task<UserCredential> credential;

    //client id
    string client_id = "656522469414-4bvr6p7mhu5p627t0bijjvugrfv8a21a.apps.googleusercontent.com";

    //client secret
    string client_secret = "Q3W6eF1oge_6jqR1nBVAi9FK";

    void Start(){
        Debug.Log("started");

    }

    /*private async Task<UserCredential> getCredentials(){
        //FileDataStore dataStore = new FileDataStore(Application.persistentDataPath,true);

        //PreferencesDataStore dataStore = new PreferencesDataStore();

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
    }*/

    public async Task<UserCredential> getCredentialsCodeFlow(){
        
        ClientSecrets secrets = new ClientSecrets();

        secrets.ClientId = client_id;
        secrets.ClientSecret = client_secret;

        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer 
            {
                ClientSecrets = secrets,
                Scopes = Scopes
            });

        if(!PlayerPrefs.HasKey("drivetoken")){
            

            var server = new LocalServerCodeReceiver();

            Debug.Log(server.RedirectUri);

            var url = flow.CreateAuthorizationCodeRequest(server.RedirectUri);
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(20));
            CancellationToken ct = cts.Token;

            Debug.Log(url.Build().AbsoluteUri);

            Application.OpenURL(url.Build().AbsoluteUri);

            var result = await server.ReceiveCodeAsync(url,ct);

            PlayerPrefs.SetString("drivetoken",result.Code);

            Debug.Log(result.Code);
        }

        Debug.Log(PlayerPrefs.GetString("drivetoken"));

        var token = new TokenResponse { RefreshToken = PlayerPrefs.GetString("drivetoken") };

        var credentials = new UserCredential(flow, 
            "user", 
            token);

        return credentials;
    }

    public void StoreValue(string storeAsName,string valueToStore)
    {
        
        DriveService service = getDriveService();

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = storeAsName + ".txt",
            Parents = new List<string>()
            {
                "appDataFolder"
            }
        };
       
        FilesResource.CreateMediaUpload request;
        

        using (var stream = GenerateStreamFromString(valueToStore))
        {
            request = service.Files.Create(
                fileMetadata, stream, "application/json");
            request.Fields = "id";
            request.Upload();
        }
        var file = request.ResponseBody;
        Debug.Log("File ID: " + file.Id);
    }


    public void getValue(string nameToGet){
        DriveService service = getDriveService();

        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.Spaces = "appDataFolder";
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";
        listRequest.Q = "name = '" + nameToGet + ".txt'";

        // List files.
        IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            .Files;
        Debug.Log("Files:");
        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                Debug.Log(file.Name);
                Debug.Log(file.Id);
            }
        }
        else
        {
            Debug.Log("No files found.");
        }

    }


    public void listFiles(){
        
        DriveService service = getDriveService();

        // Define parameters of request.
        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.Spaces = "appDataFolder";
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";

        // List files.
        IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            .Files;
        Debug.Log("Files:");
        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                Debug.Log(file.Name);
                Debug.Log(file.Id);
            }
        }
        else
        {
            Debug.Log("No files found.");
        }
    }

    private DriveService getDriveService(){
        
        ServicePointManager.ServerCertificateValidationCallback = Validator;
        

        var credentials = getCredentialsCodeFlow();

        // Create Drive API service.
        var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials.Result,
                ApplicationName = ApplicationName,
            });

        return service;
    }

    public static bool Validator(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors policyErrors)
     {
         //*** Just accept and move on...
         Debug.Log("Validation successful!");
         return true;
     }


    private static Stream GenerateStreamFromString(string s)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

}
