using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public static int size=5;

    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        AI.CreateTiles(size, this);

        //create goal
        GameObject go = GameObject.Instantiate(GameObject.FindObjectOfType<CreateSphereScript>().template, new Vector3(-size+1, .5f,-size+1), transform.rotation, null);
        //go.GetComponent<Rigidbody>().velocity = direction * -2;
        go.GetComponent<SphereScript>().setLifeTime(8000000);
        go.GetComponent<SphereScript>().owner = 90;



        TileScript[] ts = GameObject.FindObjectsOfType<TileScript>();

       

        for (int i = 0; i < ts.Length; i++)
        {
            for(int j=i+1;j<ts.Length;j++)
            {
                if(ts[i].transform.position.x == ts[j].transform.position.x && ts[i].transform.position.z == ts[j].transform.position.z - 1)
                {
                    ts[i].neighboringTiles.Add(ts[j]);
                    ts[j].neighboringTiles.Add(ts[i]);
                }
                if (ts[i].transform.position.x == ts[j].transform.position.x && ts[i].transform.position.z == ts[j].transform.position.z + 1)
                {
                    ts[i].neighboringTiles.Add(ts[j]);
                    ts[j].neighboringTiles.Add(ts[i]);
                }

                if (ts[i].transform.position.x == ts[j].transform.position.x+1 && ts[i].transform.position.z == ts[j].transform.position.z)
                {
                    ts[i].neighboringTiles.Add(ts[j]);
                    ts[j].neighboringTiles.Add(ts[i]);
                }
                if (ts[i].transform.position.x == ts[j].transform.position.x - 1 && ts[i].transform.position.z == ts[j].transform.position.z)
                {
                    ts[i].neighboringTiles.Add(ts[j]);
                    ts[j].neighboringTiles.Add(ts[i]);
                }
            }

            
        }
    }

    public void Update()
    {
        //colorAGraph(GameObject.FindObjectOfType<TileScript>()); //for this it doesn't matter where we start, other (i.e. when you are trying to find a path from a specific point) times it does
    }

    //example graph algorithm

    float currentColor=0;
    float currentway = 1;

    public static int iteration = 1;
    /*public void colorAGraph(TileScript startNode)
    {
        TileScript start = startNode;

        int currentIteration = iteration++;

        start.graph_iteration = currentIteration;

        Queue<TileScript> theQueue = new Queue<TileScript>();

        theQueue.Enqueue(start);

        start.graph_distance = 0;
        start.backPointer = null;

        while(theQueue.Count > 0)
        {
            TileScript current = theQueue.Dequeue();

            current.graph_iteration = currentIteration;
            current.setColorPercentage(0);

            for(int i=0;i<current.neighboringTiles.Count;i++)
            {
                if(current.neighboringTiles[i].graph_iteration != currentIteration)
                {
                    current.neighboringTiles[i].graph_iteration = currentIteration;
                    theQueue.Enqueue(current.neighboringTiles[i]);
                    current.neighboringTiles[i].graph_distance = current.graph_distance + 1;
                    current.neighboringTiles[i].backPointer = current;
                }
            }
        }
    }*/
}
