using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public GameObject projectile;

    public float timeToShoot;
    public float ShootCoolDown;

    public bool freqShooter;
    public bool watcher;


    public float TimeShootDestroy;
    // Start is called before the first frame update
    void Start()
    {
        ShootCoolDown = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (freqShooter)
        {
            ShootCoolDown -= Time.deltaTime;
        }
       
        if(ShootCoolDown < 0f)
        {

            Shoot();
        }

        if (watcher)
        {
            gameObject.GetComponent<PlayerDetect>();
        }
    }

   

    public void Shoot()
    {
        GameObject balaenemigo = Instantiate(projectile, transform.position, Quaternion.identity);

        if (transform.localScale.x < 0f)
        {
            balaenemigo.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f, 0f), ForceMode2D.Force);
        }
        else
        {
            balaenemigo.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f), ForceMode2D.Force);
        }

        ShootCoolDown = timeToShoot;

        StartCoroutine(EsperarTiempoParaDestuir(balaenemigo));
    }

    IEnumerator EsperarTiempoParaDestuir(GameObject bala)
    {
        yield return new WaitForSeconds(TimeShootDestroy);
        Destroy(bala);
    }
}
