using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveSmoothing =10f;
    private Vector3 target;
    private Vector3 targetDir;
    private Vector3 targetForward;
    private Rigidbody rigidbody;
    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetForward = transform.forward; 
        Debug.Log(targetForward);
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        rotation();
        movePlayer();
    }
    void rotation(){
        //update the forward with slerp to rotate object properly
        transform.forward = Vector3.Slerp(transform.forward,targetForward,Time.deltaTime*moveSmoothing);
        Debug.Log(transform.forward);
    }
    void movePlayer(){
        if(canMove){
        target = new Vector3(Input.GetAxisRaw(TagHolder.MouseX),0,Input.GetAxisRaw(TagHolder.MouseY));
        target.Normalize();
        //turn the direction to cam direction
        target = target.x*Camera.main.transform.right+target.z*Camera.main.transform.forward;
        transform.position += target*Time.fixedDeltaTime*moveSpeed;
        targetForward = Vector3.ProjectOnPlane(-target,Vector3.up);
        // Debug.Log(targetForward);
        }
    }
    void getInput(){
        if(Input.GetMouseButtonDown(0)){
            canMove = true;
        }else if(Input.GetMouseButtonUp(0)){
            canMove = false;
        }
    }

}
