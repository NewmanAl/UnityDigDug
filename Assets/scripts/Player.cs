using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    float movementSpeed;

    //animator bools
    bool isMoving = false;
    bool isFiring = false;
    bool isAttached = false;
    bool isPumping = false;

    //references
    Animator animator;

    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    LevelBlock inBlock;

    public Direction facing = Direction.right;

    
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        //movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move(Direction.right);

            isMoving = true;
            facing = Direction.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            move(Direction.left);

            isMoving = true;
            facing = Direction.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            move(Direction.up);
            
            isMoving = true;
            facing = Direction.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move(Direction.down);

            isMoving = true;
            facing = Direction.down;
        }
        else
        {
            if(isMoving)
            {
                //keep moving until fully in block
                switch (facing)
                {
                    case Direction.right:
                        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        if (transform.position.x - 0.08f >= inBlock.transform.position.x)
                            isMoving = false;
                        break;
                    case Direction.left:
                        transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
                        if (transform.position.x + 0.08f <= inBlock.transform.position.x + 0.16f)
                            isMoving = false;
                        break;
                    case Direction.up:
                        if(transform.localScale.x > 0)
                            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        else
                            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);

                        if (transform.position.y + 0.08f > inBlock.transform.position.y)
                            isMoving = false;
                            
                        break;
                    case Direction.down:
                        if(transform.localScale.x > 0)
                            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                        else
                            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);

                        if (transform.position.y + 0.08f < inBlock.transform.position.y)
                            isMoving = false;
                        break;
                }
            }
        }


        updateAnimator();
	}

    private void updateAnimator()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isFiring", isFiring);
        animator.SetBool("isAttached", isAttached);
        animator.SetBool("isPumping", isPumping);
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

        inBlock = other.gameObject.GetComponent<LevelBlock>();
        //Debug.Log("Block " + currBlock.transform.position.ToString() + " | Me " + transform.position);
        //Debug.Log(currBlock.transform.position.x + ", " + currBlock.transform.position.y);
    }
}
