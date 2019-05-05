using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public List<TileScript> neighboringTiles = new List<TileScript>();

    public int tileNumber;
    public static int tile = 0;

    public GameObject[] cubes;

    public void setColorPercentage(float theColor)
    {
        GetComponentInChildren<Renderer>().material.color = new Color(theColor, theColor, theColor);
        
    }

    public int graph_iteration;
    public int graph_distance;
    public TileScript backPointer;

    // Start is called before the first frame update
    void Start()
    {
        tileNumber = tile++;


    }

    // Update is called once per frame
    void Update()
    {
        /*PlayerScript[] players = GameObject.FindObjectsOfType<PlayerScript>();

        for(int i=0;i<players.Length;i++)
        {
            if(players[i].getCurrentTiles()[tileNumber])
            {
                cubes[i].GetComponent<Renderer>().material.color = players[i].playerColor;
            }

        }*/
    }
}
