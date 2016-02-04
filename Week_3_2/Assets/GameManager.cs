using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject EnemyObject;
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

	void Start ()
    {
        SpawnEnemy();
    }
	
	void Update ()
    {
	
	}

    public void SpawnEnemy()
    {
        GameObject Enemy = (GameObject)Instantiate(EnemyObject, new Vector3(0, 5, 0), Quaternion.identity);
    }
}
