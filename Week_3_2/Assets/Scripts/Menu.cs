using UnityEngine;
using System.Collections;

// Those using Unity 5.3 and above. Please use the 
// SceneManager loading method provided by the
// following using statement.

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SelectPNC()
    {
        PlayerPrefs.SetString("Mode", "PNC");

        // For unity versions < 5.3
        // Application.LoadLevel("Gameplay");

        // For unity versions > 5.3
        SceneManager.LoadScene("Gameplay");
        
    }

    public void SelectKNM()
    {
        PlayerPrefs.SetString("Mode", "KNM");

        // For unity versions < 5.3
        // Application.LoadLevel("Gameplay");

        // For unity versions > 5.3
        SceneManager.LoadScene("Gameplay");
    }
}
