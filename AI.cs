using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    //Node class to give desired characteristics to each of the possible tile spaces.
    public class Node
    {
        public List<Node> adjacentNodes = new List<Node>(); //Nodes next to this Node
        public Vector2 position;
        public bool specialTile;    //used for beginning and end
        public bool isActive;       //used to set the active Node
        public bool currentTile = false;
        public bool isOrphan = false;
        public bool mustStay = false;
        public bool ontop = false;      //if Node is on top row
        public bool onRight = false;      //if Node is on right column
        public bool notSquare = false;   //if the Node is not part of a square
    }


    public static void CreateTiles(int size, ControllerScript sc)
    {
        int count = 0;
        Node[,] graph = new Node[size, size];   //create a Node array equal to the size of table
        int[,] mapData = new int[size, size];   //do same thing for mapdata

        //nested for loop to go through and set the Nodes
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                graph[i, j] = new Node();
                mapData[i, j] = 0;          //sets all to 0, this means they're inactive
                graph[i, j].isActive = (mapData[i, j] == 1);    //if the graph of i,j is active that means its corresponding mapdata is set to 1, being active.
                graph[i, j].position = new Vector2(-i, -j); //negatives may not be necessary
               
            }
        }
        graph[0, 0].specialTile = true; //start and end are special and active
        graph[0, 0].isActive = true;
        graph[size - 1, size - 1].specialTile = true;
        graph[size - 1, size - 1].isActive = true;
        Node endGraph = graph[size - 1, size - 1];  //used to simplify things later
        Node startGraph = graph[0, 0];
        Node current = startGraph; //just for the first step, will be changing throughout
        Node temp = startGraph;
        //int[][] endPoint = mapData[size - 1, size - 1].transform.position;
        //  Vector2 distance = endGraph.transform.position - closest.transform.position;
        
        //Nested for loop to set the adjacent nodes of the graph.
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //in bounds check for these four situations
                if (i + 1 < size)
                {
                    graph[i, j].adjacentNodes.Add(graph[i + 1, j]);     //each node has a list of its adjacent nodes attached to it
                   
                }
                if (i - 1 >= -0)
                {
                    graph[i, j].adjacentNodes.Add(graph[i - 1, j]);
                    
                }
                if (j + 1 < size)
                {
                    graph[i, j].adjacentNodes.Add(graph[i, j + 1]);
                   
                }
                if (j - 1 >= 0)
                {
                    graph[i, j].adjacentNodes.Add(graph[i, j - 1]);
                   
                }
            }
        }

        while (noActiveNeighbors(endGraph) == true)   //sets a random path from the start location to the end.  Will end when the endGraph has an active neighbor, which means path is complete.
        {
            count = 0;
            //nested for loop.  If i/j = current, then do the loop through it's neighbors with the checks of i+1, i-1, see if it's on the ends etc.
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (graph[i, j] == current)   //start on first Node, then whatever current will be.
                    {
                        if (i + 1 == size)  //means we're on right column, need to be careful about being trapped
                        {
                            current.onRight = true;

                        }
                        else if (j + 1 == size) //on top row, same issue
                        {
                            current.ontop = true;

                        }
                        int random = Random.Range(0, current.adjacentNodes.Count);  //used to randomize starting path
                        Debug.Log("Random value outloop is: " + random);
                        for (int k = 0; k < current.adjacentNodes.Count; k++)   //going through neighboring tiles of current
                        {
                            if (!current.adjacentNodes[k].isActive && checkifSquares(graph, current.adjacentNodes[k]) == false)
                            {
                                temp = current.adjacentNodes[k];    //set a temp in case we need it later.
                            }
                            
                            while (current.adjacentNodes[random].notSquare == false && count < 10)    //this while loop resets the random number if the random is assigned to an already active node.
                                //count used here to break out of possible infinite loop.
                            {
                                Debug.Log("Check if squares is: " + checkifSquares(graph, current.adjacentNodes[random]));
                                if (checkifSquares(graph, current.adjacentNodes[random]) == true)
                                {
                                    random = Random.Range(0, current.adjacentNodes.Count);
                                    count++;
                                }
                                else
                                {
                                    current.adjacentNodes[random].notSquare = true;
                                }
                            }
                            
                           
                            if (current.onRight == false && current.ontop == false && k == random)  //if it's not on top or right, go random
                            {
                                if (current.adjacentNodes[k].isActive == false && checkifSquares(graph, current.adjacentNodes[k]) == false)
                                {
                                    Debug.Log("Printing a random path");
                                    current.adjacentNodes[k].isActive = true;   //set the neighboring to active.
                                    current = current.adjacentNodes[k];         //current is now the tile that was just set to active.
                                    break;
                                }
                                else if (!temp.isActive)    //otherwise use the temp which is another non active tile
                                {
                                    Debug.Log("using the temp Node");
                                    current = temp;         
                                    current.isActive = true;
                                    break;
                                }
                                else
                                {
                                    current.adjacentNodes[k].isActive = true;   //otherwise just make a tile active.
                                    current = current.adjacentNodes[k];
                                    Debug.Log("in the default loop");
                                    break;
                                }
                            }
                            else if (current.onRight == true && (j + 1) < size) //if it's on the right but not top yet
                            {
                                if (checkifSquares(graph, current.adjacentNodes[k]) == false)   //if it won't cause a square to be formed
                                {
                                    Debug.Log("deviate from up");
                                    current.adjacentNodes[k].isActive = true;   //go to the available tile and make it active
                                    current = current.adjacentNodes[k];
                                    break;
                                }
                                else if (current.adjacentNodes[k] == graph[i, j + 1])   //if not an option, then go up to avoid being trapped.
                                {
                                    Debug.Log("Going straight up");
                                    current.adjacentNodes[k].isActive = true;
                                    current = current.adjacentNodes[k];
                                    break;
                                }
                            }
                            else if (current.ontop == true && (i + 1) < size)   //we repeat with tiles on the top
                            {
                                if(checkifSquares(graph, current.adjacentNodes[k]) == false)    //if it doesn't cause square
                                {
                                    Debug.Log("Deviate from right");
                                    current.adjacentNodes[k].isActive = true;   //go to available tile and set to active
                                    current = current.adjacentNodes[k];
                                    break;
                                }
                                else if (current.adjacentNodes[k] == graph[i + 1, j])   //else go right
                                {
                                    Debug.Log("going straight to the right");
                                    current.adjacentNodes[k].isActive = true;
                                    current = current.adjacentNodes[k];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }//end of while loop setting starting path.

        while(activePercent(graph) < 0.6f) //This will keep looping until at least 60% of the graph has been filled.  Picked through trial and error.
            //too low and the maze may not be filled much.  Too high and it will keep looping forever as there is no legal place to activate Node.
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(graph[i,j].isActive)//only going off active tiles, this ensures no orphan tiles.
                    {
                        current = graph[i, j];  //moves to current.
                        int newRandom = Random.Range(0, current.adjacentNodes.Count);
                        for(int k = 0; k < current.adjacentNodes.Count; k++)    //does similar thing as setting the start path to make them active.
                        {
                            //picks random neighbor node to activate.
                            if(!current.adjacentNodes[k].isActive && checkifSquares(graph, current.adjacentNodes[k]) == false && k == newRandom && !current.specialTile)
                            {
                                current.adjacentNodes[k].isActive = true;
                            }
                        }
                    }
                }
            }
        }
       /* if (checkifAllEndActive(endGraph) == true)  //if there are multiple tiles connected to the end tile, it will remove and ensure only one tile to end.
        {
            for (int i = 0; i < endGraph.adjacentNodes.Count; i++)
            {
                if (endGraph.adjacentNodes[i].isActive)
                {
                    endGraph.adjacentNodes[i].isActive = false;
                    break;
                }
            }
        }*/
        

        setOrphan(graph);
        removeOrphan(graph);    //may not be necessary but for any case that maybe not thought of.
        //should be towards the end.
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                mapData[i, j] = graph[i, j].isActive ? 1 : 0;   //setting the active ones to 0.  
            }
        }

        /*for (int i = size*2/3; i < size; i++)
        {
            for (int j = size * 2 / 3; j < size-1; j++)
            {
                mapData[i, j] = 0;
            }
        }*/



        /* do not change this loop*/
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (mapData[i, j] == 0)
                {
                    //empty space
                }
                else if (mapData[i, j] == 1)
                {
                    GameObject.Instantiate(sc.tilePrefab, new Vector3(-i, 0, -j), Quaternion.identity, null);
                }
            }
        }
    }

    //used to see how much of the graph has been made active, once at a certain value can stop filling it in.
    public static float activePercent(Node[,] theGraph)
    {
        int count = 0;
        int totalSize = theGraph.GetLength(0) * theGraph.GetLength(1);
        for (int i = 0; i < theGraph.GetLength(0);i++)
        {
            for (int j = 0; j < theGraph.GetLength(0); j++)
            {
                if(theGraph[i,j].isActive)
                {
                    count++;
                }
            }
        }
        float activePercentage = (float)count / totalSize;
        return activePercentage;
    }
    //returns true if all neighboring tiles of the Node are active, used for end node.
    public static bool checkifAllEndActive(Node theNode)
    {
        for(int i = 0; i < theNode.adjacentNodes.Count; i++)
        {
            if(theNode.adjacentNodes[i].isActive == false)
            {
                return false;
            }
        }
        return true;
    }

    /*This function checks if a Node will be a square 2x2 by checking combinations of its neighboring tiles
     * First all eight possible neighbors as checked if active and if so the corresponding bool is set to positive
     * Afterwards, the combos of bools that would cause squares (i.e. above, above-left, and left) are checked and if positive it returns true.
     * */
    public static bool checkifSquares(Node[,] theGraph, Node theNode)
    {
        bool leftSquare = false;
        bool rightSquare = false;
        bool upSquare = false;
        bool downSquare = false;
        bool downleft = false;
        bool downright = false;
        bool upright = false;
        bool upleft = false;
        int rowsLength = theGraph.GetLength(0);
        int colLength = theGraph.GetLength(1);
        //Debug.Log("rowlength is: " + rowsLength);
        //Debug.Log("collength is: " + colLength);
        for (int i = 0; i < rowsLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                if (theGraph[i, j] == theNode)
                {
                    if ((i + 1) < rowsLength)
                    {
                        if (theGraph[i + 1, j].isActive)
                        {
                            rightSquare = true;
                        }
                    }

                    if ((i - 1) >= 0)
                    {
                        if (theGraph[i - 1, j].isActive)
                        {
                            leftSquare = true;
                        }
                    }

                    if ((j + 1) < colLength)
                    {
                        if (theGraph[i, j + 1].isActive)
                        {
                            upSquare = true;
                        }
                    }

                    if ((j - 1) >= 0)
                    {
                        if (theGraph[i, j - 1].isActive)
                        {
                            downSquare = true;
                        }
                    }
                    if ((i - 1) >= 0 && (j + 1) < colLength)
                    {
                        if (theGraph[i - 1, j + 1].isActive)
                        {
                            upleft = true;
                        }
                    }
                    if ((i - 1) >= 0 && (j - 1) >= 0)
                    {
                        if (theGraph[i - 1, j - 1].isActive)
                        {
                            downleft = true;
                        }
                    }
                    if ((i + 1) < rowsLength && (j - 1) >= 0)
                    {
                        if (theGraph[i + 1, j - 1].isActive)
                        {
                            downright = true;
                        }
                    }
                    if ((i + 1) < rowsLength && (j + 1) < colLength)
                    {
                        if (theGraph[i + 1, j + 1].isActive)
                        {
                            upright = true;
                        }
                    }
                }
            }
        }//end of long for loop thing.
        if (upright && upSquare && rightSquare)
        {
            return true;
        }
        else if (upleft && upSquare && leftSquare)
        {
            return true;
        }
        else if (downleft && leftSquare && downSquare)
        {
            return true;
        }
        else if (downright && rightSquare && downSquare)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //sets any tiles on graph that are by themselves to be an orphan.
    public static void setOrphan(Node[,] theGraph)
    {
        int rowsLength = theGraph.GetLength(0);
        int colLength = theGraph.GetLength(1);  //I think just need one since it's a square, but might as well make it scalable.
        for (int i = 0; i < rowsLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                if (noActiveNeighbors(theGraph[i, j]) == true && theGraph[i, j].isActive)
                {
                    theGraph[i, j].isOrphan = true;
                }
            }
        }
    }

    //this function gets rid of any orphan tiles.  I'm pretty sure it's not needed the way it's implemented
    public static void removeOrphan(Node[,] theGraph)
    {
        int rowsLength = theGraph.GetLength(0);
        int colLength = theGraph.GetLength(1);  //I think just need one since it's a square, but might as well make it scalable.
        for (int i = 0; i < rowsLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                if (theGraph[i, j].isOrphan == true)
                {
                    theGraph[i, j].isActive = false;
                }
            }
        }
    }

    //function that checks if a square has no active neighbors, used to end the loop placing the path.
    public static bool noActiveNeighbors(Node theNode)
    {
        for (int i = 0; i < theNode.adjacentNodes.Count; i++)
        {
            if (theNode.adjacentNodes[i].isActive == true)
            {
                return false;
            }
        }
        return true;
    }
}