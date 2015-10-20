using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float firingTotalTime;
    float currentFiringTime = 0;

    //animator bools
    bool isMoving = false;
    bool isFiring = false;
    bool isAttached = false;
    bool isPumping = false;
    bool isDigging = false;
    bool isDieing = false;
    int deathState = 0;
    [SerializeField]
    float deathStateWaitTime;
    float currentDeathStateTime;


    //references
    Animator animator;
    AudioSource deathSound;
    AudioSource shootSound;

    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    LevelBlock inBlock;
    List<LevelBlock> blocksColliding;
    bool isAdjusting = false; //used for auto movement between tiles

    public Direction facing = Direction.right;

    
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        blocksColliding = new List<LevelBlock>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        shootSound = audioSources[0];
        deathSound = audioSources[1];
	}
	
	// Update is called once per frame
	void Update () {
        //movement
        
        //adjust if colliding with multiple blocks
        if (blocksColliding.Count > 1)
        {
            float distance = 100.0f;
            LevelBlock closestBlock = null;
            Vector3 playerPos = new Vector3(transform.position.x - 0.08f, transform.position.y + 0.08f, 0);

            foreach (LevelBlock l in blocksColliding)
                if (Vector3.Distance(playerPos, l.transform.position) < distance)
                {
                    distance = Vector3.Distance(playerPos, l.transform.position);
                    closestBlock = l;
                }

            inBlock = closestBlock;

            if (facing == Direction.left || facing == Direction.right)
                transform.position = new Vector3(transform.position.x, inBlock.transform.position.y - 0.08f, 0);
            else
                transform.position = new Vector3(inBlock.transform.position.x + 0.08f, transform.position.y, 0);
        }
        else
            if (blocksColliding.Count == 1)
                inBlock = blocksColliding[0];



        if (!isDieing)
        {
            if (!isFiring)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (!isAdjusting)
                    {
                        move(Direction.right);

                        isMoving = true;
                        isAdjusting = false;
                        facing = Direction.right;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (!isAdjusting)
                    {
                        move(Direction.left);

                        isMoving = true;
                        isAdjusting = false;
                        facing = Direction.left;
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (!isAdjusting)
                    {
                        move(Direction.up);

                        isMoving = true;
                        isAdjusting = false;
                        facing = Direction.up;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (!isAdjusting)
                    {
                        move(Direction.down);

                        isMoving = true;
                        facing = Direction.down;
                    }
                }
                else
                {
                    if (isMoving)
                    {
                        isAdjusting = true;
                    }
                    else
                        isAdjusting = false;
                }
            }

            //keep moving if not in center of tile
            if (isAdjusting)
            {
                //keep moving until fully in block
                switch (facing)
                {
                    case Direction.right:
                        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        if (transform.position.x - 0.08f >= inBlock.transform.position.x)
                        {
                            isMoving = false;
                            isAdjusting = false;
                        }
                        break;
                    case Direction.left:
                        transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                        if (transform.position.x + 0.08f <= inBlock.transform.position.x + 0.16f)
                        {
                            isMoving = false;
                            isAdjusting = false;
                        }
                        break;
                    case Direction.up:
                        if (transform.localScale.x > 0)
                            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        else
                            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);

                        if (transform.position.y + 0.08f > inBlock.transform.position.y)
                        {
                            isMoving = false;
                            isAdjusting = false;
                        }

                        break;
                    case Direction.down:
                        if (transform.localScale.x > 0)
                            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        else
                            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);

                        if (transform.position.y + 0.08f < inBlock.transform.position.y)
                        {
                            isMoving = false;
                            isAdjusting = false;
                        }
                        break;
                }
            }

            //fire
            if (Input.GetKey(KeyCode.Space) && !isMoving)
            {
                isFiring = true;
                shootSound.Play();
            }

            if (isFiring)
            {
                currentFiringTime += Time.deltaTime;
                if (currentFiringTime >= firingTotalTime)
                {
                    isFiring = false;
                    currentFiringTime = 0;
                }
            }
        }
        else
        { 
            //currently dieing
            currentDeathStateTime += Time.deltaTime;
            if(currentDeathStateTime >= deathStateWaitTime)
            {
                deathState++;
                currentDeathStateTime = 0;
                if (deathState == 3)
                    deathSound.Play();
            }

            if (deathState == 4)
            {
                Destroy(gameObject);
            }
        }


        updateAnimator();

        blocksColliding.Clear();
	}

    private void updateAnimator()
    {
        if (isMoving)
        {
            //check if digging
            Level l = GameObject.Find("Level").GetComponent<Level>();

            int[] tilePos = l.GetTileCoords(inBlock);

            switch (facing)
            { 
                case Direction.right:
                    if (tilePos[0] != 11)
                    {
                        if (!l.levelBlocks[tilePos[0] + 1][tilePos[1]].dug)
                            isDigging = true;
                        else
                            isDigging = false;
                    }
                    else
                        isDigging = false;

                    break;

                case Direction.left:
                    if (tilePos[0] != 0)
                    {
                        if (!l.levelBlocks[tilePos[0] - 1][tilePos[1]].dug)
                            isDigging = true;
                        else
                            isDigging = false;
                    }
                    else
                        isDigging = false;

                    break;

                case Direction.up:
                    if (tilePos[1] != 0)
                    {
                        if (!l.levelBlocks[tilePos[0]][tilePos[1] - 1].dug)
                            isDigging = true;
                        else
                            isDigging = false;
                    }
                    else
                        isDigging = false;

                    break;

                case Direction.down:
                    if (tilePos[1] != 11)
                    {
                        if (!l.levelBlocks[tilePos[0]][tilePos[1] + 1].dug)
                            isDigging = true;
                        else
                            isDigging = false;
                    }
                    else
                        isDigging = false;
                    break;
            }
        }
        
        
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isFiring", isFiring);
        animator.SetBool("isAttached", isAttached);
        animator.SetBool("isPumping", isPumping);
        animator.SetBool("isDigging", isDigging);
        animator.SetBool("isDieing", isDieing);
        animator.SetInteger("deathState", deathState);
    }

    private void move(Direction d)
    {
        switch (d)
        {
            case Direction.right:
                //face right
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
                transform.localScale = newScale;

                //move
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                break;
            case Direction.left:
                //face left
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                newScale = new Vector3(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
                transform.localScale = newScale;

                //move
                transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                break;
            case Direction.up:
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
            case Direction.down:
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

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<LevelBlock>() != null)
            blocksColliding.Add(other.gameObject.GetComponent<LevelBlock>());
        //inBlock = other.gameObject.GetComponent<LevelBlock>();
        //Debug.Log("Block " + currBlock.transform.position.ToString() + " | Me " + transform.position);
        //Debug.Log(currBlock.transform.position.x + ", " + currBlock.transform.position.y);

        if (other.gameObject.GetComponent<Dino>() != null)
        {
            if (!isDieing) 
            {
                isDieing = true;
                deathState = 0;
            }
        }
    }
}
