using UnityEngine;
using System.Collections;

public class TomatoThing : MonoBehaviour {

    [SerializeField]
    public float movementSpeed;

    //animator bools
    bool isMoving = false;
    bool isBeingPumped = false;
    int pumpState = -1;
    Animator animator;

    AudioSource blowupSound;

    public Player.Direction facing;

    LevelBlock inBlock;


	// Use this for initialization
	void Start () {
        isMoving = true;
        animator = GetComponent<Animator>();
        blowupSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isMoving)
            move();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false;
            isBeingPumped = true;
            pumpState++;

            if(pumpState == 3)
                blowupSound.Play();

            if (pumpState == 4)
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetKeyUp(KeyCode.Backspace) && isBeingPumped)
        {
            pumpState--;

            if (pumpState == -1)
            {
                isBeingPumped = false;
                isMoving = true;
            }
        }

        updateAnimator();
	}

    private void updateAnimator()
    {
        animator.SetBool("isMoving", isMoving); ;
        animator.SetBool("isPumping", isBeingPumped);
        animator.SetInteger("pumpState", pumpState);
    }

    private void move()
    {
        switch (facing)
        {
            case Player.Direction.right:
                //face right
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
                transform.localScale = newScale;

                //move
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                break;
            case Player.Direction.left:
                //face left
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                newScale = new Vector3(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
                transform.localScale = newScale;

                //move
                transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                break;
            case Player.Direction.up:
                //face up and move
                if (transform.localScale.x > 0)
                {
                    transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                    transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(270.0f, Vector3.forward);
                    transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                }
                break;
            case Player.Direction.down:
                //face down and move
                if (transform.localScale.x > 0)
                {
                    transform.rotation = Quaternion.AngleAxis(270.0f, Vector3.forward);
                    transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                    transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LevelBlock>() != null)
        {
            Level l = GameObject.Find("Level").GetComponent<Level>();
            inBlock = other.GetComponent<LevelBlock>();
            int[] myPos = l.GetTileCoords(inBlock);

            bool dugRight, dugLeft, dugUp, dugDown;
            dugRight = dugLeft = dugUp = dugDown = false;

            if (myPos[0] != 0)
                dugLeft = l.levelBlocks[myPos[0] - 1][myPos[1]].dug;

            if (myPos[0] != 11)
                dugRight = l.levelBlocks[myPos[0] + 1][myPos[1]].dug;

            if (myPos[1] != 0)
                dugUp = l.levelBlocks[myPos[0]][myPos[1] - 1].dug;

            if (myPos[1] != 11)
                dugDown = l.levelBlocks[myPos[0]][myPos[1] + 1].dug;

            Player.Direction prevFacing = facing;

            switch (facing)
            {
                case Player.Direction.right:
                    if (!dugRight && !dugUp && !dugDown)//turn around
                        facing = Player.Direction.left;

                    if (!dugRight && dugUp && !dugDown)// turn up
                        facing = Player.Direction.up;

                    if (!dugRight && !dugUp && dugDown)// turn down
                        facing = Player.Direction.down;

                    if (!dugRight && dugUp && dugDown)// turn up or down
                    {
                        int rand = Random.Range(0, 2);
                        facing = (rand == 0 ? Player.Direction.up : Player.Direction.down);
                    }

                    break;

                case Player.Direction.left:
                    if (!dugLeft && !dugUp && !dugDown)// turn around
                        facing = Player.Direction.right;

                    if (!dugLeft && dugUp && !dugDown)// turn up
                        facing = Player.Direction.up;

                    if (!dugLeft && !dugUp && dugDown) //turn down
                        facing = Player.Direction.down;

                    if (!dugLeft && dugUp && dugDown) //turn up or down
                    {
                        int rand = Random.Range(0, 2);
                        facing = (rand == 0 ? Player.Direction.up : Player.Direction.down);
                    }

                    break;

                case Player.Direction.up:
                    if (!dugUp && !dugLeft && !dugRight)// turn around
                        facing = Player.Direction.down;

                    if (!dugUp && dugLeft && !dugRight) //turn left
                        facing = Player.Direction.left;

                    if (!dugUp && !dugLeft && dugLeft) // turn right
                        facing = Player.Direction.right;

                    if (!dugUp && dugLeft && dugLeft) // turn right or left
                    {
                        int rand = Random.Range(0, 2);
                        facing = (rand == 0 ? Player.Direction.left : Player.Direction.right);
                    }

                    break;

                case Player.Direction.down:
                    if (!dugDown && !dugLeft && !dugRight) //turn around
                        facing = Player.Direction.up;

                    if (!dugDown && dugLeft && !dugRight)
                        //turn left
                        facing = Player.Direction.left;


                    if (!dugDown && !dugLeft && dugRight) // turn right
                        facing = Player.Direction.right;

                    if (!dugDown && dugLeft && dugRight) //turn right or left
                    {
                        int rand = Random.Range(0, 2);
                        facing = (rand == 0 ? Player.Direction.left : Player.Direction.right);
                    }

                    break;
            }

            if (prevFacing != facing)
            {
                Vector3 blockPos = other.gameObject.transform.position;
                transform.position = new Vector3(blockPos.x + 0.08f, blockPos.y - 0.08f, 0);
            }
        }
    }
}
