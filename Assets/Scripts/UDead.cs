using UnityEngine;
using System.Collections;

public class UDead : MonoBehaviour {

    public Player Klin;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("U Dead!");
        PikeColliderListener DeadBoy = other.GetComponent<PikeColliderListener>();

        if (DeadBoy != null && Klin != null)
        {
            Klin.Respawn();
        }
    }
}
