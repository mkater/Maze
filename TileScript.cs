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



    public int graph_amanda_int1; //for whatever use you want
    public int graph_amanda_int2; //for whatever use you want
    public int graph_amanda_int3; //for whatever use you want
    public float graph_amanda_float1; //for whatever use you want
    public float graph_amanda_float2; //for whatever use you want
    public float graph_amanda_float3; //for whatever use you want

    public int graph_bryan_int1; //for whatever use you want
    public int graph_bryan_int2; //for whatever use you want
    public int graph_bryan_int3; //for whatever use you want
    public float graph_bryan_float1; //for whatever use you want
    public float graph_bryan_float2; //for whatever use you want
    public float graph_bryan_float3; //for whatever use you want

    public int graph_caleb_int1; //for whatever use you want
    public int graph_caleb_int2; //for whatever use you want
    public int graph_caleb_int3; //for whatever use you want
    public float graph_caleb_float1; //for whatever use you want
    public float graph_caleb_float2; //for whatever use you want
    public float graph_caleb_float3; //for whatever use you want

    public int graph_jorge_int1; //for whatever use you want
    public int graph_jorge_int2; //for whatever use you want
    public int graph_jorge_int3; //for whatever use you want
    public float graph_jorge_float1; //for whatever use you want
    public float graph_jorge_float2; //for whatever use you want
    public float graph_jorge_float3; //for whatever use you want

    public int graph_marisa_int1; //for whatever use you want
    public int graph_marisa_int2; //for whatever use you want
    public int graph_marisa_int3; //for whatever use you want
    public float graph_marisa_float1; //for whatever use you want
    public float graph_marisa_float2; //for whatever use you want
    public float graph_marisa_float3; //for whatever use you want

    public int graph_matt_int1; //for whatever use you want
    public int graph_matt_int2; //for whatever use you want
    public int graph_matt_int3; //for whatever use you want
    public float graph_matt_float1; //for whatever use you want
    public float graph_matt_float2; //for whatever use you want
    public float graph_matt_float3; //for whatever use you want

    public int graph_sebastian_int1; //for whatever use you want
    public int graph_sebastian_int2; //for whatever use you want
    public int graph_sebastian_int3; //for whatever use you want
    public float graph_sebastian_float1; //for whatever use you want
    public float graph_sebastian_float2; //for whatever use you want
    public float graph_sebastian_float3; //for whatever use you want

    public int graph_steven_int1; //for whatever use you want
    public int graph_steven_int2; //for whatever use you want
    public int graph_steven_int3; //for whatever use you want
    public float graph_steven_float1; //for whatever use you want
    public float graph_steven_float2; //for whatever use you want
    public float graph_steven_float3; //for whatever use you want


    public int graph_tai_int1; //for whatever use you want
    public int graph_tai_int2; //for whatever use you want
    public int graph_tai_int3; //for whatever use you want
    public float graph_tai_float1; //for whatever use you want
    public float graph_tai_float2; //for whatever use you want
    public float graph_tai_float3; //for whatever use you want

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
