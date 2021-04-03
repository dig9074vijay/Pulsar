using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //an attribute so that it can be accessed in Inspector view but is not accessible by other gameobjects
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShot;
 
    [SerializeField]
    private bool _isSpeedPowerupActive = false;
    private float _speedMultiplier = 4f;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score;
    private UI_Manager UI;



    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        UI = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            shootLaser();    
    }

    /// <summary>
    /// After UPDATE
    /// </summary>

    //Logic for shooting laser and tripleshot powerup
    void shootLaser()
    {   
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShot, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + new Vector3(0f, 0.9f, 0f), Quaternion.identity);
        }
       

    }

    //Logic for movement and speed up powerup
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if(_isSpeedPowerupActive == true)
        {
            transform.Translate(direction * Time.deltaTime * _speed * _speedMultiplier);
        }
        else
        transform.Translate(direction * Time.deltaTime * _speed);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.82f, 0f), 0);

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }

    }

    //Damage the player if hit by laser 
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _shield.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives -= 1;
        //check if dead
        if(_lives < 1)
        {
            _spawnManager.playerIsDead();
            Destroy(this.gameObject);
        }
    }

    //Triple Shot Powerup
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotActiveRoutine());
    }

    //Coroutine for triple shot powerup
    IEnumerator TripleShotActiveRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    //Speed Up Powerup
    public void SpeedUpActive()
    {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedUpActiveRoutine());
    }

    //Coroutine for speedup powerup
    IEnumerator SpeedUpActiveRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedPowerupActive = false;
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void EnemyKilled(int points)
    {
        _score += points;
        if(UI != null)
        UI.ScoreUpdate(_score.ToString());
    }
}
