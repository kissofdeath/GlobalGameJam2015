using UnityEngine;
using System.Collections;

public class PikeColliderListener : MonoBehaviour {

    public GameObject klin;
    private Player player;

    void Start()
    {
        player = klin.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("in pike collider triger");
        if (c.gameObject.name.Contains("pike"))
        {
            
            player.Damage(Time.deltaTime * 50f);
        }
    }
}
