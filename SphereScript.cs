using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    float lifetimex=-10;

    bool lifetimeset = false;

    public int owner = 1;

    // Update is called once per frame
    void Update()
    {
        lifetimex -= Time.deltaTime;
        
        if(lifetimex < 0 && lifetimeset)
        {
            Destroy(gameObject);
        }
    }

    public void setLifeTime(float lifeTime)
    {
        lifetimex = lifeTime;
        lifetimeset = true;
    }

    public float getLifeTime()
    {
        return lifetimex;
    }
}
