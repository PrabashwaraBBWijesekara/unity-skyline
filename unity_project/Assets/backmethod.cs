using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backmethod : MonoBehaviour
{
    // Variables for authentication
    public string username;
    public string password;

    public void Startone()
    {
        // Fetch player details directly when the script starts
        FetchPlayerDetails();
    }

    public void GoBackToStartScene()
    {
        // Check if the start scene is already loaded
        if (!SceneManager.GetSceneByName("gish").isLoaded)
        {
            // Load the start scene only if it's not already loaded
            SceneManager.LoadScene("gish");
            Debug.Log("Scene 'gish' loaded.");
        }
    }

    void FetchPlayerDetails()
    {
        StartCoroutine(GetPlayerDetails());
    }

    IEnumerator GetPlayerDetails()
    {
        string url = "http://20.15.114.131:8080/api/user/profile/view";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";

        // Add basic authentication header
        string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
        request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    string json = reader.ReadToEnd();
                    Debug.Log(json); // Display JSON response in the console
                }
            }
            else
            {
                Debug.LogError("Failed to fetch player details. Error: " + response.StatusCode);
            }
        }

        yield return null;
    }
}
