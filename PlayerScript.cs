using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Color playerColor;
    public int thePlayer;
    public TileScript currentTile;
    public string playername = "MaryHadALittleLamb";
    public bool playerControlled = false;

    private bool[] vistedTiles;
    private bool[] vistedTilesb;

    public int numberOfTimesTileChanged = 1;

    public void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for(int i=0;i<renderers.Length;i++)
        {
            renderers[i].material.color = playerColor;
        }

        TileScript[] theTiles = GameObject.FindObjectsOfType<TileScript>();

        float distance = 999;

        //get closest tile
        for(int i=0;i<theTiles.Length;i++)
        {
            if (distance > Vector3.Distance(theTiles[i].transform.position, transform.position)) 
            {
                distance = Vector3.Distance(theTiles[i].transform.position, transform.position);
                currentTile = theTiles[i];
            }
        }

        //vistedTiles = new bool[theTiles.Length];
        //vistedTilesb = new bool[theTiles.Length];
    }

    bool hit = false;
    int way = 0;

    public void Update()
    {
        time += Time.deltaTime;

        //vistedTiles[currentTile.tileNumber] = true;

        if (!hit)
            move();
        else
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = new Color(playerColor.r * .3f, playerColor.g * .3f, playerColor.b * .3f);
            }
        }

        if (playerControlled && !hit)
        {
            if (Input.GetKey(KeyCode.S))
            {
                for (int i = 0; i < currentTile.neighboringTiles.Count; i++)
                {
                    if (currentTile.neighboringTiles[i].transform.position == currentTile.transform.position - new Vector3(0, 0, -1))
                    {
                        moveToTile(currentTile.neighboringTiles[i]);
                    }
                }
                way = 0;
            }
            if (Input.GetKey(KeyCode.D))
            {
                for (int i = 0; i < currentTile.neighboringTiles.Count; i++)
                {
                    if (currentTile.neighboringTiles[i].transform.position == currentTile.transform.position - new Vector3(1, 0, 0))
                    {
                        moveToTile(currentTile.neighboringTiles[i]);
                    }
                }
                way = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                for (int i = 0; i < currentTile.neighboringTiles.Count; i++)
                {
                    if (currentTile.neighboringTiles[i].transform.position == currentTile.transform.position - new Vector3(-1, 0, 0))
                    {
                        moveToTile(currentTile.neighboringTiles[i]);
                    }
                }
                way = 3;
            }
            if (Input.GetKey(KeyCode.W))
            {
                for (int i = 0; i < currentTile.neighboringTiles.Count; i++)
                {
                    if (currentTile.neighboringTiles[i].transform.position == currentTile.transform.position - new Vector3(0, 0, 1))
                    {
                        moveToTile(currentTile.neighboringTiles[i]);
                    }
                }
                way = 2;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //print(way);

                if (way == 0)
                {
                    createSphere(new Vector3(0, 0, -1));
                }
                if (way == 2)
                {
                    createSphere(new Vector3(0, 0, 1));
                }
                if (way == 1)
                {
                    createSphere(new Vector3(1, 0, 0));
                }
                if (way == 3)
                {
                    createSphere(new Vector3(-1, 0, 0));
                }
            }
        }
    }

    public float getTime() { return time; }
    public float getMaxTime() { return maxTime; }
    
    private float maxTime=2;
    private float time = 0;

    public void createSphere(Vector3 direction)
    {


        if (time >= maxTime && !hit)
        {
            //create
            GameObject go = GameObject.Instantiate(GameObject.FindObjectOfType<CreateSphereScript>().template, transform.position, transform.rotation, null);
            go.GetComponent<Rigidbody>().velocity = direction * -2;
            go.GetComponent<SphereScript>().setLifeTime(8000000);
            go.GetComponent<SphereScript>().owner = thePlayer;

            go.GetComponent<Renderer>().material.color = playerColor;
            time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SphereScript>() != null && other.GetComponent<SphereScript>().owner!= thePlayer)
        {
            //hit = true;
            ControllerScript.size+=2;
            SceneManager.LoadScene(0);
        }
    }


    public bool [] getCurrentTiles()
    {
        for(int i=0;i< vistedTiles.Length;i++)
        {
            vistedTilesb = vistedTiles;
        }
        return vistedTilesb;
    }

    public int getNumberOfTilesVisitedIncludingDuplicates()
    {
        return numberOfTimesTileChanged;
    }

    public void move()
    {
        if(moving)
        {
            Vector3 way = toMoveTo.transform.position - transform.position;
            way.y = 0;

            way.Normalize();
            way = way *Time.deltaTime;

            float distance = Vector3.Distance(transform.position, toMoveTo.transform.position);


            //Debug.Log("PRE: "+transform.position);
            transform.position += way;
            //Debug.Log("POST: " + transform.position);
            float distance2 = Vector3.Distance(transform.position, toMoveTo.transform.position);

            if(Vector3.Distance(transform.position,toMoveTo.transform.position) < Vector3.Distance(transform.position, currentTile.transform.position))
            {
                currentTile = toMoveTo;
                numberOfTimesTileChanged++;
            }

            if(distance2 >= distance) //i.e. if player crossed point
            {
                //print(playername+" crossed point");

                float y = transform.position.y;
                Vector3 position = toMoveTo.transform.position;
                position.y = y;
                transform.position = position;
                moving = false;
                toMoveTo = null;
            }
        }
    }

    public bool moving = false;
    public TileScript toMoveTo = null;

    public bool isMoving()
    {
        return moving;
    }
    public void moveToTile(TileScript ts)
    {
        /*if(moving)
        {
            return;
        }*/

        bool isNeighboringTile = false;
        for (int i = 0; i < currentTile.neighboringTiles.Count; i++)
        {
            if (ts == currentTile.neighboringTiles[i])
            {
                isNeighboringTile = true;
            }
        }

        if (ts == currentTile)
        {
            if (ts == toMoveTo)
            {
                //nothing to do, already moving to
            }
            else
            {
                toMoveTo = ts;
                moving = true;

                Vector3 newpos = toMoveTo.transform.position;
                newpos.y = transform.position.y;
                transform.LookAt(newpos, Vector3.up);
            }
        }
        else if (isNeighboringTile)
        {
            toMoveTo = ts;
            moving = true;


            Vector3 newpos = toMoveTo.transform.position;
            newpos.y = transform.position.y;
            transform.LookAt(newpos, Vector3.up);
        }
        else
        {
            Debug.Log("Player: " + playername + " invalid order from " + currentTile + " to " + ts);
        }
    }
}
