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

    private UserCredential getCredentialsCodeFlow(){
        
        var token = new TokenResponse { RefreshToken = "4/rH0Wvvq85lWfBH5Z37xJu-LskBL7v0LoKL4sDZVAr1E#" };

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
    }

    public void StoreValue(string name,string valueToStore)
    {
        
        ServicePointManager.ServerCertificateValidationCallback = Validator;

        var credentials = getCredentials().Result;

        //4/7fXeic4JsaB3Rclyu5C63grwJuUTjOMh47npk2HFWyc#
        //var credentials = getCredentialsCodeFlow();

        Debug.Log(credentials);

        // Create Drive API service.
        var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName,
            });

        // Define parameters of request.
        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.Spaces = "appDataFolder";
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";

        //listRequest.ServerCertificateValidationCallback = Validator;

        //HttpRequestMessage request = listRequest.CreateRequest();

        //Debug.Log(request.Properties);

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

    public listFiles(){
        ServicePointManager.ServerCertificateValidationCallback = Validator;

        var credentials = getCredentials().Result;

        //4/7fXeic4JsaB3Rclyu5C63grwJuUTjOMh47npk2HFWyc#
        //var credentials = getCredentialsCodeFlow();

        Debug.Log(credentials);

        // Create Drive API service.
        var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName,
            });

        // Define parameters of request.
        FilesResource.ListRequest listRequest = service.Files.List();
        listRequest.Spaces = "appDataFolder";
        listRequest.PageSize = 10;
        listRequest.Fields = "nextPageToken, files(id, name)";

        //listRequest.ServerCertificateValidationCallback = Validator;

        //HttpRequestMessage request = listRequest.CreateRequest();

        //Debug.Log(request.Properties);

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

    public static bool Validator(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors policyErrors)
     {
         //*** Just accept and move on...
         Debug.Log("Validation successful!");
         return true;
     }

    private void listFiles(){

    }

}
