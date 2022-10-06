using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] spawnLocations;
    public GameObject Character;

    private Vector3 respawnLocation;

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Start () {
        Character = (GameObject)Resources.Load("Player" , typeof(GameObject));
        
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
   
