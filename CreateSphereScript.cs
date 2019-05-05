using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSphereScript : MonoBehaviour
{

    public float howOften = 3f;
    public float howLongLive = 5f;
    float currentTime = 0;

    public GameObject template;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > howOften)
        {
            //create
            GameObject go = GameObject.Instantiate(template, transform.position, transform.rotation, null);
            //Destroy(go, howLongLive);
            currentTime = 0;
            go.GetComponent<Rigidbody>().velocity = transform.forward * 2;
            go.GetComponent<SphereScript>().setLifeTime(howLongLive);
            go.GetComponent<SphereScript>().owner = -1;
        }
    }
}
