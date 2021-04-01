using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float timeBetweenShots = 0.3f;
    private float timerBetweenShots = 0f;

    public float attackDamage = 20f;

    public AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerBetweenShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
                {
                    if (hit.collider.CompareTag("Enemy"))
                        hit.collider.GetComponent<EnemyController>().TakeDamage(attackDamage);

                    shootSound.Play();
                }

                timerBetweenShots = timeBetweenShots;
            }
        }
        else
            timerBetweenShots -= Time.deltaTime;
    }
}
