using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mario : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float Vertical;

    public float distance;
    public RaycastHit2D surface;
    public LayerMask layerMask;

    public float jumpForce;
    public float speed = 1;

    public bool isBig;
    public bool floor;
    public bool isCrouch;
    public bool isDead;

    public int lives ;
    public SaveLoad objectSaveLoad;
    public GameObject audioSource;

    private SpriteRenderer sprite;

    public Animator _animation;
    public BoxCollider2D box;
    public int deathControl;
    public float gravity;

 
    void Start()
    {
        lives = PlayerPrefs.GetInt("Loves");
        distance = 0.7f;
        isDead = false;
        gravity = 0.25F;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        isBig = false;
        _animation = GetComponent<Animator>();
        objectSaveLoad = GameObject.Find("GameManager").GetComponent<SaveLoad>();
    }
  
    void Update()
    {
        AirOrFloor();
        if (deathControl > 0)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
        }
        
       
        //Sets where mario looks and tge movement animation.
        if (Horizontal < 0.0f && !isCrouch || Horizontal < 0.0f && isCrouch && !isBig)
        {
            transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);
            _animation.SetInteger("VelocidadMovimiento", 1);
        }
        else if (Horizontal > 0.0f && !isCrouch || Horizontal > 0.0f && isCrouch && !isBig)
        {
            transform.localScale = new Vector3(7.0f, 7.0f, 1.0f);
            _animation.SetInteger("VelocidadMovimiento", -1);
        }
        else if (Horizontal == 0)
        {
            _animation.SetInteger("VelocidadMovimiento", 0);
        }
        //Sets Coruch animation.
        if(Vertical < 0)
        {

            _animation.SetBool("Crouch", true);
            isCrouch = true;
        }
        else
        {
            _animation.SetBool("Crouch", false);
            isCrouch = false;
        }

        //Sets jump animation.
        if (Input.GetButtonDown("Jump") && floor)
        {
            Jump(jumpForce,true, Vector2.up);
        }

    }

    //Moves or stops character.
    private void FixedUpdate()
    {
       if(isBig && isCrouch)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * 0, Rigidbody2D.velocity.y);
            _animation.SetInteger("VelocidadMovimiento", 0);

        }
        else
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
        }
       
    }

    //Makes Mario able to jump.
    public void Jump(float _fuerzaSalto,bool isSound,Vector2 dir)
    {
        if(isSound)
        {
            GameObject jump = Instantiate(Resources.Load("JumpVariant", typeof(GameObject))) as GameObject;
            Destroy(jump, 6);
        }
       
        Rigidbody2D.AddForce(dir* _fuerzaSalto);
        _animation.SetBool("Suelo", false);
        _animation.SetBool("Jump", true);
    }

    //Converts Mario into BigMario.
    private void Big()
    {
        
        deathControl ++;
        GameObject powerUp = Instantiate(Resources.Load("PowerUpVariant", typeof(GameObject))) as GameObject;
        Destroy(powerUp, 6);
        _animation.SetBool("IsBig", true);
        box.size = new Vector2(0.08f, 0.32f);
        box.offset = new Vector2(0, 0.08f);
        isBig = true;

    }

    //Converts Big Mario in to Small Mario , and checks if he dies.
    private void Small()
    {
        
        deathControl -=1;
        print(deathControl);
        if (deathControl > 0)
        {
            StartCoroutine(Blink());
            GameObject warp = Instantiate(Resources.Load("WarpVariant", typeof(GameObject))) as GameObject;
            Destroy(warp, 6);
        }
        
        
        _animation.SetBool("IsBig",false);
        box.size = new Vector2(0.08f, 0.16f);
        box.offset = new Vector2(0, 0);
        isBig = false;
        if(deathControl == 0)
        {
            Muerte();
        }
    }

    //Kills Mario and manages his lives to check if he continues or its gameover.
    public void Muerte()
    {
        lives--;
        audioSource.SetActive(false);
        isDead = true;
        GameObject dieSound = Instantiate(Resources.Load("DieSoundVariant", typeof(GameObject))) as GameObject;
        Destroy(dieSound, 6);
        box.enabled = false;
        _animation.SetBool("Death", true);
        _animation.SetBool("Suelo", true);
        speed = 0.0f;
        gravity = 0.0001f;
        _animation.SetInteger("VelocidadMovimiento", 0);
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        Rigidbody2D.AddForce(Vector2.up * jumpForce / 1f);

        objectSaveLoad.objetoGameData.lives = lives;
        PlayerPrefs.SetInt("Loves", objectSaveLoad.objetoGameData.lives);
        PlayerPrefs.Save();
        if (lives < 0)
        {
            lives = 0;
            StartCoroutine(ChangeScene(3));

        }
        else
        {
            StartCoroutine(ChangeScene(2));
        }
        

    }

    //Changes Scene.
    public IEnumerator ChangeScene(int scene)
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(scene);
    }

    //Makes Mario Blink after he was hit , he cant be hitten  for 3 seconds.
    public IEnumerator Blink()
    {
        Physics2D.IgnoreLayerCollision(8, 7, true);
        sprite.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(8, 7, false);
        sprite.color = new Color(1, 1, 1, 1f);
       
    }

    //Checks if Mario is on the floor or in the air , to check if he can jump.
    public void AirOrFloor()
    {
        surface = Physics2D.Raycast(transform.position, -Vector2.up, distance, layerMask);
        if(surface.collider != null)
        {
            _animation.SetBool("Suelo", true);
            _animation.SetBool("Jump", false);
            floor = true;
        }
        else
        {
            _animation.SetBool("Suelo", false);
            _animation.SetBool("Jump", true);
            floor = false;
        }
    }



}

