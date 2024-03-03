using UnityEngine;
using UnityEngine.SceneManagement;

public class backmethod : MonoBehaviour
{
    public void GoBackToStartScene()
    {
        // Check if the start scene is already loaded
        if (!SceneManager.GetSceneByName("gish").isLoaded)
        {
            // Load the start scene only if it's not already loaded
            SceneManager.LoadScene("gish");
            Debug.Log("Called");
        }
    }
}
