using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class viewprofile : MonoBehaviour

{
    // Start is called before the first frame update


    private string profileEndpoint = "http://20.15.114.131:8080/api/user/profile/view";
    // Update is called once per frame

    public void get_profile_details() 
    {
        //Debug.Log(postmethod.jwtToken);
        StartCoroutine(GetProfile());
    }
    public IEnumerator GetProfile()
    {
        using (UnityWebRequest requestnew = UnityWebRequest.Get(profileEndpoint))
        {

            Debug.Log(name);

            requestnew.SetRequestHeader("Authorization", "Bearer " + postmethod.jwtToken);

            yield return requestnew.SendWebRequest();

            if (requestnew.result == UnityWebRequest.Result.Success)
            {
                string responsenew = requestnew.downloadHandler.text;
                Debug.Log("Profile Data: " + responsenew);
            }
            else if (requestnew.responseCode == 401)
            {
                Debug.LogError("Failed to get profile data: Unauthorized (401). Check the JWT token validity and permissions.");
            }
            else
            {
                Debug.LogError("Failed to get profile data: " + requestnew.error);
            }
        }
    }
}
