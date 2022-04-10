using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{




    //Pubs
    public float MoveSpeed;
    public LayerMask SolidObj;  
    //Privs
    private Animator Anime;
    private bool IsMoving;
    private Vector2 PlayerInput;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMoving)
        {
            PlayerInput.x = Input.GetAxisRaw("Horizontal"); //Gets either a -1, 1, or a  0
            PlayerInput.y = Input.GetAxisRaw("Vertical"); //Gets either a -1, 1, or a  0
            if (PlayerInput.x != 0) PlayerInput.y = 0; //prevent diaganol movement
            if(PlayerInput != Vector2.zero) //If player is moving, only move 1 unit per iter
            {
                var TargetPos = transform.position; //Take the current object transform and assign it to a var
                TargetPos.x += PlayerInput.x;// +=-1/1 if player pressed horizontally
                TargetPos.y += PlayerInput.y;// +=-1/1 if player pressed vertically
                if (IsWalkable(TargetPos))
                {
                    StartCoroutine(Move(TargetPos));//Move over time to the target position 
                }
            }
        }

        //Anime.SetBool("IsMoving", isMoving); //Set animator transition at end of update so it is always current
    }

    IEnumerator Move(Vector3 TargetPos)//Move character over time instead of "teleporting" player 1 unit over;
    {
        IsMoving = true; //set to true to prevent !IsMoving block from changing the target position and calling this IEnumerator 
                        //before it can finish previous calls. 

        while ((TargetPos - transform.position).sqrMagnitude > Mathf.Epsilon)//while target position is still not the transform -V-
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime); //Move to target pos over time. 
            yield return null; //Nothing to return, but not entirely sure why this line is this line....
        }
        transform.position = TargetPos; //reset target position to prevent unwanted movement.
        IsMoving = false; //allow a new target position
    }

    private bool IsWalkable(Vector3 TargetPos)
    {
        if(Physics2D.OverlapCircle(TargetPos, 0.05f, SolidObj) != null)
        {
            return false;
        }
        return true;
    }





}
