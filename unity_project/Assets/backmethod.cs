using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class backmethod : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoBackToStartScene()
    {
        // Load the first scene (index 0)
        SceneManager.LoadScene(0);
        Debug.Log("Cal");
    }
}
