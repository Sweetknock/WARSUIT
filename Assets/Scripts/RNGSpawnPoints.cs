using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGSpawnPoints : MonoBehaviour {

    public GameObject[] spawnLocations;
    public GameObject Character;
    public Grid grid;
    public Vector3 respawnLocation;

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Start () {
        Character = (GameObject)Resources.Load("Erza" , typeof(GameObject));

        Debug.Log("RNGSPAWNPOINTS");
        Debug.Log(Character);
        Debug.Log(respawnLocation);
        respawnLocation = Character.transform.position;
        
        SpawnCharacter();
    }

    void Update () {

    }

    private void SpawnCharacter()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(Character, spawnLocations[spawn].transform.position, Quaternion.identity);
    }




  
}
   
