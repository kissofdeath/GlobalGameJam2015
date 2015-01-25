using UnityEngine;
using System.Collections;

public class ProxiSpawner : MonoBehaviour {

    public GameObject enemyPref;
    public Player Kiln;
    private bool flag = false;
    public float TurretFireSpeed = 1f;
    public float TurretBulletSpeed = 10f;
    public float TurretBulletDamage = 5f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!flag)
        {
            flag = true;
            if (col.gameObject.name == "02.body" || col.gameObject.name == "01.bottom")
            {
                InvokeRepeating("TurdDropper", 0.0f, TurretFireSpeed);
            }
        }
    }

    void TurdDropper()
    {
        GameObject Newbie = (GameObject)GameObject.Instantiate(enemyPref, transform.position, Quaternion.identity);
        BulletMove BMove = Newbie.GetComponent<BulletMove>();

        if (BMove != null)
        {
            BMove.Kiln = Kiln;
            BMove.SetTarget(Kiln.transform.position);
            BMove.speed = TurretBulletSpeed;
            BMove.Damage = TurretBulletDamage;
        }
    }
}
