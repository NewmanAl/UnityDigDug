using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    float movementSpeed;

    //animator bools
    bool isMoving;
    bool isFiring;
    bool isAttached;
    bool isPumping;

    //references
    Animator animator;


    
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //movement
        isMoving = false;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //face right
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localScale = newScale;
            
            //move
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        { 
            //face left
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localScale = newScale;

            //move
            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        { 
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
            
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        { 
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
}
