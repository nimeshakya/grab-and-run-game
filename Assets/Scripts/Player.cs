using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 8f;
    [SerializeField]
    private float jumpForce = 10f;

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;

    private float movementX;

    private string IS_RUNNING = "isRunning";
    private string IS_JUMPING = "isJumping";
    private string TRIGGER_LAUNCH = "triggerLaunch";

    private bool isGrounded = true;

    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private string RED_FLAG_TAG = "RedFlag";
    private string BLUE_FLAG_TAG = "BlueFlag";
    private string RED_FLAG_COLLECTOR_TAG = "RedFlagCollector";
    private string BLUE_FLAG_COLLECTOR_TAG = "BlueFlagCollector";

    // which flag is touched
    [HideInInspector]
    public bool redFlagContact = false;
    [HideInInspector]
    public bool blueFlagContact = false;

    // flag spawnner
    private GameObject FlagSpawnner;
    private FlagSpawnner _flagSpawnnerScript;


    // score
    private GameObject score;
    private Score _scoreScript;

    // player death event and delegate
    public delegate void PlayerDied();
    public static event PlayerDied playerDiedInfo;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FlagSpawnner = GameObject.FindWithTag("FlagSpawnner");
        _flagSpawnnerScript = FlagSpawnner.GetComponent<FlagSpawnner>();
        score = GameObject.FindWithTag("Score");
        _scoreScript = score.GetComponent<Score>();
    }

    void ExecuteEvent()
    {
        if(playerDiedInfo != null)
        {
            playerDiedInfo();
        }
    }

    private void Update()
    {
        PlayerMovement();
        PlayerMoveAnimation();
        PlayerJump();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void PlayerMoveAnimation()
    {
        if(movementX > 0)
        {
            sr.flipX = false;
            anim.SetBool(IS_RUNNING, true);
        }
        else if(movementX < 0)
        {
            sr.flipX = true;
            anim.SetBool(IS_RUNNING, true);
        }
        else
        {
            anim.SetBool(IS_RUNNING, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger(TRIGGER_LAUNCH);
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (isGrounded)
        {
            anim.SetBool(IS_JUMPING, false);
        }
        else
        {
            anim.SetBool(IS_JUMPING, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            ExecuteEvent();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(RED_FLAG_TAG) && !blueFlagContact)
            redFlagContact = true;
        if (collision.CompareTag(BLUE_FLAG_TAG) && !redFlagContact)
            blueFlagContact = true;

        if (collision.CompareTag(RED_FLAG_COLLECTOR_TAG) && redFlagContact)
        {
            Destroy(GameObject.FindWithTag(RED_FLAG_TAG));
            redFlagContact = false;
            _flagSpawnnerScript.SpawnFlag();
            _scoreScript.PlayerScored();
        }
        if (collision.CompareTag(BLUE_FLAG_COLLECTOR_TAG) && blueFlagContact)
        {
            Destroy(GameObject.FindWithTag(BLUE_FLAG_TAG));
            blueFlagContact = false;
            _flagSpawnnerScript.SpawnFlag();
            _scoreScript.PlayerScored();
        }
    }
}
