using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour  {
    public GUIBarScript healthBar;
    private float hp, maxhp;
    private Vector3 respawnPos;
    public MultiPlayerController mpc;
    public GameObject platformSpawner;


	void Start()
    {
        maxhp = hp = 100;
        respawnPos = transform.position;
	}

    void Update()
    {
        healthBar.SetNewValue(hp/maxhp);
    }
	
    public void Heal(float delta)
    {
        hp += delta;
        Mathf.Clamp(hp, 0f, 100f);

        //Debug.Log("healed player");
    }

    public void Damage(float delta)
    {
        hp -= delta;
        Mathf.Clamp(hp, 0f, 100f);
        Debug.Log("damaged player");

        if (hp <= 0)
        {
            if (mpc != null)
                mpc.MapControlsToActions();
            Respawn();
        }
    }

    public void Respawn()
    {
        Time.timeScale = 0.5f;
        // add death animation
        transform.position = respawnPos;
        platformSpawner.transform.position = respawnPos;
        Time.timeScale = 1.0f;

        hp = maxhp;
    }
}
