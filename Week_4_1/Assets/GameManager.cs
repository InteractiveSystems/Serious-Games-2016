using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Lives = 3;

    public int Score = 0;

    public GameObject Player;

    public GameObject Item;

    void Awake()
    {
        Instance = this;
    }

	void Start () 
    {
	
	}

	void Update () 
    {
	
	}

    public bool CheckStillAlive()
    {
        if(Lives > 0)
        {
            Lives = Lives - 1;
            return true;
        }
        else
        {
            // We are dead!
            return false;
        }
    }

    public void SpawnItem()
    {
        Vector3 location = 
            new Vector3(Random.Range(-5,5), 1, Random.Range(-5,5));

        GameObject newItem = (GameObject)Instantiate(Item, location, Quaternion.identity);
    }
}
