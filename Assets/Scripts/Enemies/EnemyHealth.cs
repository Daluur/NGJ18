using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IEnemy {

    public int Health = 100;
    public GameObject DeathAnim;
    public int Damage = 15;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        if(Health - amount <= 0)
        {
            DoDeath();
            return;
        }
        Health = Health - amount;
    }

    private void DoDeath() {
        Instantiate(DeathAnim, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<IPlayer>().TakeDamage(Damage);
            DoDeath();        
        }
    }
}
