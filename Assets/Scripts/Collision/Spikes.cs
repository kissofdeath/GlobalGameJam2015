using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour 
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "spike")
        {
            Debug.Log("Die");

            ///1. play effect 
            ///2. Destroy player
            ///3. Restart level
        }
    }
}
