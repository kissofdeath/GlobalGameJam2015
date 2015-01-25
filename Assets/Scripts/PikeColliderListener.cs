using UnityEngine;
using System.Collections;

public class PikeColliderListener : MonoBehaviour {

    public GameObject klin;
    private Player player;

    void Start()
    {
        player = klin.GetComponent<Player>();
    }

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.name.Contains("pike"))
        {
            player.Damage(25f * Time.deltaTime);
            //player.Respawn();  
        }
    }
}
