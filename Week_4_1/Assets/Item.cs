using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (!triggered)
        {
            triggered = true;

            Destroy(gameObject);

            print("Collision Enter: " + col.name);

            GameManager.Instance.Score += 1;

            GameManager.Instance.SpawnItem();
        }
    }

    void OnTriggerExit(Collider col)
    {
        // print("Collision Exit!");
    }

    void OnTriggerStay(Collider col)
    {
        // print("Collision Stay!");
    }
}
