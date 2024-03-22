using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class update_player_details : MonoBehaviour

{
    // Start is called before the first frame update
    public InputField view_details_box; // Reference to your InputField
    public Button submit_button;            // Reference to your Button
    [SerializeField] InputField inputfield;
    [SerializeField] Text resultText;


    public void ValidateInput()
    { 
        string input =inputfield.text;
        if (input.Length < 4)
        {
            resultText.text = "Invalid Input";
            resultText.color = Color.yellow;
        } else
        {
            resultText.text = "Valid Input";
            resultText.color = Color.red;
        }






     }
    
    
    
    
    
    
    
    
    
    
    
}








