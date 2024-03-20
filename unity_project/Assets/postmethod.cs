using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class postmethod : MonoBehaviour
{
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIzOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOQ";
    private string loginEndpoint = "http://20.15.114.131:8080/api/login";
    private string profileEndpoint = "http://20.15.114.131:8080/api/user/profile/view";

    
    private string name = "u";
    string jwt = "no";

    public IEnumerator PostRequest()
    {

     
        string json = "{\"apiKey\":\"" + apiKey + "\"}";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        MyCertificateHandler certificateHandler = new MyCertificateHandler();

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, ""))
        {
            request.certificateHandler = certificateHandler;
            request.SetRequestHeader("Content-Type", "application/json");
            name = "Gishan";
            Debug.Log(name);
            request.uploadHandler = new UploadHandlerRaw(data);

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                string response = request.downloadHandler.text;
                jwt = response;
                Debug.Log("You are successfully authenticated. Your JWT token is:" + jwt);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                name = jwt;
                Debug.Log(name);
            }
            else if (request.responseCode == 400)
            {
                Debug.LogError("Bad Request: " + request.downloadHandler.text);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
            else if (request.responseCode == 401)
            {
                Debug.LogError("Unauthorized: " + request.downloadHandler.text);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

            yield return jwt;
        }
    }

    private IEnumerator GetProfile()
    {
        // Parse the JWT token from the provided JSON


        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, ""))
        {
            MyCertificateHandler certificateHandler = new MyCertificateHandler();
            request.certificateHandler = certificateHandler;
            request.SetRequestHeader("Content-Type", "application/json");

            // Construct JSON data for the request
            string requestData = "{\"apiKey\":\"" + apiKey + "\"}";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(requestData);
            request.uploadHandler = new UploadHandlerRaw(data);

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                string response = request.downloadHandler.text;
                jwt = response;
            }
            // Assuming jwt is your JSON response


            // Parse the JSON string into a JSONNode
           


        }








        using (UnityWebRequest request = UnityWebRequest.Get(profileEndpoint))
        {
            request.SetRequestHeader("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzE0IiwiaWF0IjoxNzEwODY4NjAyLCJleHAiOjE3MTA5MDQ2MDJ9.YkrL98jcTHmOuJs8MaWqxVYVBEekgmDQb3OJko5fbCIFgnojtRyhyHl4aOgFacEPNgsnVn6-TVcDL23-MGsX0A");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                Debug.Log("Profile Data: " + response);
            }
            else if (request.responseCode == 401)
            {
                Debug.LogError("Failed to get profile data: Unauthorized (401). Check the JWT token validity and permissions.");
            }
            else
            {
                Debug.LogError("Failed to get profile data: " + request.error);
            }
        }
    }


    public void OnButtonClick()
    {
        StartCoroutine(PostRequest());
    }

    public void OnButtonGETclick()
    {
        StartCoroutine(GetProfile()); // Pass the jwt token to the GetProfile coroutine
    }
}

public class MyCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}





























