using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcAi : MonoBehaviour
{

    [SerializeField] public Transform target;

    //public float speed = 200f;
    public float nextWaipointDistance = 0f;


    public int O2_needed;
    public int H20_needed;
    public int FOOD_needed;
    public int max_energy;
    public int energy = 0;

    public EnergyScr ENGbar;

    Path path;
    int currentWaipoint = 0;
    //int currentTarget = 0;
    bool reachedEndofPath = false;
    

    float _t=0;
    float o2_timer = 0;
    float h2o_timer = 0;
    float food_timer = 0;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] Transform NPCGFX;
    GetWorkNeeded Working;

    bool sleeping = false;

    
    [SerializeField]public Transform bed;


    // Start is called before the first frame update
    void Start()
    {
        if(transform.tag == "Human"){
            O2_needed = 1;
            H20_needed = 1;
            FOOD_needed = 1;
            max_energy = 100;
            
        }
        else if(transform.tag == "Neuforian"){
            O2_needed = -1;
            H20_needed = 2;
            FOOD_needed = 0;
            max_energy = 150;
        }

        ENGbar.SetMaxEnergy(max_energy);

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

    void RedirectCourse(Transform t){
        seeker.StartPath(rb.position, t.position, OnPathComplete);
    }

    void FixedUpdate()
    {
        o2_timer += Time.deltaTime;
        h2o_timer += Time.deltaTime;
        food_timer += Time.deltaTime;

        if(o2_timer >= 20f){
            //obsht kislorod --
            o2_timer =0;
        }
        if(h2o_timer >= 30f && !sleeping){
            //obsht kislorod --
            h2o_timer =0;
        }
        if(food_timer >= 40f && !sleeping){
            //obsht kislorod --
            food_timer=0;
        }

        if(path == null){
            return;
        }
        if(currentWaipoint >= path.vectorPath.Count){
            reachedEndofPath = true;
            anim.SetBool("up",false);
            anim.SetBool("left",false);
            anim.SetBool("down",false);
            anim.SetBool("right",false);
            if(energy <= 0.1*max_energy ){
                RedirectCourse(bed);
                sleeping = true;
            }
            _t += Time.deltaTime;
            if(sleeping){
                 
 
                if (_t >= 1f)
                {
                    _t = 0f;
                    energy++;
                }
            }
            else{
                if (_t >= 1f){   
                    _t = 0f;
                    energy--;
                    Working = target.GetComponent<GetWorkNeeded>();
                    Working.Produce();
                }
            }
            ENGbar.SetEnergy(energy);
            if(energy>=max_energy){
                RedirectCourse(target);
                sleeping = false;
            }

        }else{
            reachedEndofPath = false;
        }

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

            anim.SetBool("up",false);
            anim.SetBool("left",true);
            anim.SetBool("down",false);
            anim.SetBool("right",false);

        }
        else if(path.vectorPath[currentWaipoint].y > transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){

            anim.SetBool("up",true);
            anim.SetBool("left",false);
            anim.SetBool("down",false);
            anim.SetBool("right",false);

        }
        else if(path.vectorPath[currentWaipoint].y < transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){
            anim.SetBool("up",false);
            anim.SetBool("left",false);
            anim.SetBool("down",true);
            anim.SetBool("right",false);
        }

        transform.position =Vector2.Lerp(transform.position,path.vectorPath[currentWaipoint],0.07f);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance){
            currentWaipoint++;
        }

        

    }
}
