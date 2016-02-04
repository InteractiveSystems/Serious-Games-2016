using UnityEngine;
using System.Collections;

// Those using Unity 5.3 and above. Please use the 
// SceneManager loading method provided by the
// following using statement.

using UnityEngine.SceneManagement;

public class Complete : MonoBehaviour
{
    private bool triggered = false;
    public string SceneToLoad = "Menu";

    void Start () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (!triggered)
        {
            triggered = true;

            Destroy(gameObject);

            // For unity versions < 5.3
            // Application.LoadLevel("Gameplay");

            // For unity versions > 5.3
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
