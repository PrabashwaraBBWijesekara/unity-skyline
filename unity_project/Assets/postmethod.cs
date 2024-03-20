using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using Newtonsoft.Json.Linq;
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

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, ""))
        {
            //request.certificateHandler = null; // Disable certificate validation
            request.SetRequestHeader("Content-Type", "application/json");
            name = "Gishan";
            Debug.Log(name);
            request.uploadHandler = new UploadHandlerRaw(data);

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                string response = request.downloadHandler.text;
                //string response_up = request.uploadHandler.text;
                //Debug.Log(response_up);
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
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, ""))
        {
            request.certificateHandler = null; // Disable certificate validation
            request.SetRequestHeader("Content-Type", "application/json");

            string requestData = "{\"apiKey\":\"" + apiKey + "\"}";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(requestData);
            request.uploadHandler = new UploadHandlerRaw(data);

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                string response = request.downloadHandler.text;
                jwt = response;
                try
                {
                    // Parse the JSON string
                    JObject jsonResponse = JObject.Parse(response);

                    // Extract the JWT token value
                    string jwtToken = (string)jsonResponse["token"];
                    name = jwtToken;
                    Debug.Log("JWT Token: " + jwtToken);
                }
                catch (Exception ex)
                {
                    Debug.Log("Error parsing JSON: " + ex.Message);
                }
            }
        }

        using (UnityWebRequest request = UnityWebRequest.Get(profileEndpoint))
        {
            request.SetRequestHeader("Authorization", "Bearer " + name);

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
        StartCoroutine(GetProfile());
    }
}
