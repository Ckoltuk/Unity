using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class enemyAı : NetworkBehaviour {

    public GameObject[] target;
    public Transform player;
    public float playerDistance;
    public float rotationDamping;
    public float movespeed;
    
    public PlayerWeapon weapon;

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 50;
    [SerializeField]
    private int currentHealth=100;


    // Use this for initialization
    void Start () {

     
		
	}

    // Update is called once per frame
    void Update()
    {

        

            // FindcloseEnemy();

            player = FindcloseEnemy().transform;

            playerDistance = Vector3.Distance(player.position, transform.position);

            if (playerDistance < 15f)
            {
                lookAtPlayer();
            }
            if (playerDistance < 12f)
            {
                if (playerDistance > 2f)
                {
                    chase();
                }
                if (playerDistance < 5f)
                {
                attack();
                



                }
        }

        }
    



    GameObject FindcloseEnemy()
            {

            target = GameObject.FindGameObjectsWithTag("Player");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in target)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }


    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);

    }

    void chase()
    {

        transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
    }


    void attack()
    {
        
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, weapon.range))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);

            }
            if(hit.collider.gameObject.tag == "Enemy")
            {
                aiTakedamage();
            }
        }
        
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage)
    {

        Debug.Log(_playerID + " has been shot by bot");

        Player _player = GameManager.GetPlayer(_playerID);

        _player.RpcTakeDamage(_damage);
        
    }

    ///////////////////////////////////////////////////////////////////////

    
    public void aiTakedamage()
    {
        if (isDead)
            return;

        currentHealth -= 20;

        if (currentHealth <= 0)
        {
            Die();
        }

    }


    private void Die()
    {
        isDead = true;

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Destroy(gameObject);


    }

    public void SetDefaults()
    {

        isDead = false;

        currentHealth = maxHealth;


        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }




















    //void attack() ORJİNAL
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, weapon.range))
    //    {
    //        if (hit.collider.gameObject.tag == "Player")
    //        {
    //            Debug.Log("Bot shoot enemy");
    //        }
    //    }

    //}

}
