using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int powerupID;
   // private Player player;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.Find("Player").GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5.76f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if(collision.gameObject.tag == "Player")
        {
            if (player != null)
            {
                if (powerupID == 0)
                    player.TripleShotActive();
                else if (powerupID == 1)
                    player.SpeedUpActive();
                else if (powerupID == 2)
                    player.ActivateShield();
                
            }
            Destroy(this.gameObject);
        }
    }
}
