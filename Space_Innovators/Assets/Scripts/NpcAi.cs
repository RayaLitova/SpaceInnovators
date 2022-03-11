using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaipointDistance = 0f;

    Path path;
    int currentWaipoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] Transform NPCGFX;


    bool is_walking_sideways = false;
    bool was_walking_fb = false;
    // Start is called before the first frame update
    void Start()
    {

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = NPCGFX.GetComponent<Animator>();
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaipoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null){
            return;
        }
        if(currentWaipoint >= path.vectorPath.Count){
            reachedEndofPath = true;
            anim.SetBool("up",false);
            anim.SetBool("left",false);
            anim.SetBool("down",false);
            anim.SetBool("right",false);
            return;
        }else{
            reachedEndofPath = false;
        }

        //Vector2 direction = ((Vector2)path.vectorPath[currentWaipoint] - rb.position).normalized;
        //Vector2 force = new Vector2(0.1f,0.1f);

        anim.SetBool("up",false);
        anim.SetBool("left",false);
        anim.SetBool("down",false);
        anim.SetBool("right",false);

        if(path.vectorPath[currentWaipoint].x >= transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){
            anim.SetBool("up",false);
            anim.SetBool("left",false);
            anim.SetBool("down",false);
            anim.SetBool("right",true);
            }
        else if(path.vectorPath[currentWaipoint].x < transform.position.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){
            //if(is_walking_sideways && !was_walking_fb)
            anim.SetBool("up",false);
            anim.SetBool("left",true);
            anim.SetBool("down",false);
            anim.SetBool("right",false);
            //is_walking_sideways = true;
            //was_walking_fb = false;
            //direction = new Vector2(-1f,0);
            //force = direction * speed * Time.deltaTime;
        }
        else if(path.vectorPath[currentWaipoint].y > transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){
            //direction = new Vector2(0,1f);
            //force = direction * speed * Time.deltaTime;
            //if(was_walking_fb && !is_walking_sideways){
            anim.SetBool("up",true);
            anim.SetBool("left",false);
            anim.SetBool("down",false);
            anim.SetBool("right",false);
           // }
            //was_walking_fb = true;
            //is_walking_sideways = false;
           
        }
        else if(path.vectorPath[currentWaipoint].y < transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){
            anim.SetBool("up",false);
            anim.SetBool("left",false);
            anim.SetBool("down",true);
            anim.SetBool("right",false);
        }
        //rb.MovePosition( force*path.vectorPath[currentWaipoint]*Time.deltaTime);
        transform.position =Vector2.Lerp(transform.position,path.vectorPath[currentWaipoint],0.07f);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance){
            currentWaipoint++;
        }

        /*if(target.position == transform.position){
            anim.SetBool("up",false);
            anim.SetBool("left",false);
        }

        //anim.SetBool("up",false);
        //anim.SetBool("left",false);

        if(rb.velocity.x >= 0.01f){

        }else if(rb.velocity.y <= -0.01f){
            anim.SetBool("left",true);
            
        }
        else if(rb.velocity.y >= 0.01f){
            anim.SetBool("up",true);
            
        }
        else if(rb.velocity.y <= -0.01f){

        }*/
        

    }
}
