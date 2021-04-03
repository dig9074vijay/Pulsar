using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        
        if(transform.position.y < -5.4f)
        {
            float randomX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(randomX, 7.3f, 0f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        if(other.gameObject.tag == "Player")
        {
            //Damage the Player
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
            //Destroy us
            Destroy(this.gameObject);
        }

        //If other is Laser
        if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(player != null)
            player.EnemyKilled(10);
            Destroy(this.gameObject);
        }
    }
}
