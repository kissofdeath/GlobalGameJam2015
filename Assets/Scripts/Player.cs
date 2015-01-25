using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour  {
    private float hp, maxhp;
    private Vector3 respawnPos;

	void Start()
    {
        maxhp = hp = 100;
        respawnPos = transform.position;
	}
	
    public void Heal(float delta)
    {
        hp += delta;
        Mathf.Clamp(hp, 0f, 100f);

        Debug.Log("healed player");
    }

    public void Damage(float delta)
    {
        hp -= delta;
        Mathf.Clamp(hp, 0f, 100f);
        Debug.Log("damaged player");

        if (hp == 0)
            Respawn();
    }

    public void Respawn()
    {
        transform.position = respawnPos;
    }
}
