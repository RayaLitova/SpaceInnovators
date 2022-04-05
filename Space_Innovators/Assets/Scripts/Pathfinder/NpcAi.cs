using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class NpcAi : MonoBehaviour
{

    [SerializeField] public Transform target;
    NPCStats Stats;
    //[SerializeField] NPCStats stats;

    //public float speed = 200f;
    public float nextWaipointDistance = 0f;


    //public int O2_needed;
    //public int H20_needed;
    //public int FOOD_needed;
    //public int max_energy;
    //public int energy = 0;

    //public EnergyScr ENGbar;

    Path path;
    int currentWaipoint = 0;
    //int currentTarget = 0;
    bool reachedEndofPath = false;
    
    bool movementStopped = false;

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
    GameObject new_target;
    
    [SerializeField]public Transform bed;

    void Start()
    {
        Stats = transform.GetComponent<NPCStats>();
        if(/**/target==null){
            return;
        }
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = NPCGFX.GetComponent<Animator>();
        seeker.StartPath(rb.position, /**/target.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaipoint = 0;
            anim.SetInteger("Speed",0);
        }
    }

    void RedirectCourse(Transform t){
        seeker.StartPath(rb.position, t.position, OnPathComplete);
    }

    public void Die(){
        target.tag = "Station";
        bed.tag = "Bed";
        Destroy(gameObject);
        BuildRegulator mario = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
        mario.onBoardCount[System.Array.IndexOf(mario.Tags,transform.tag)]--;
    }

    void FixedUpdate()
    {
        o2_timer += Time.deltaTime;
        h2o_timer += Time.deltaTime;
        food_timer += Time.deltaTime;

        if(o2_timer >= 20f){
            Stats.Consume("O2", Stats.O2);
            o2_timer =0;
        }
        if(h2o_timer >= 30f && !sleeping){
            Stats.Consume("Water", Stats.Water);
            h2o_timer =0;
        }
        if(food_timer >= 40f && !sleeping){
            Stats.Consume("Food", Stats.Food);
            food_timer=0;
        }

        if(path == null){
            return;
        }
        if(currentWaipoint >= path.vectorPath.Count){

            reachedEndofPath = true;
            if(!sleeping){
                Stats.energy--;
                target.parent.gameObject.GetComponent<RoomStatics>().Produce();
            }else{
                anim.SetBool("Sleeping", true);
                transform.position = bed.position;
                Stats.energy++;
                if(Stats.energy==Stats.max_energy){
                    movementStopped = false;
                }else{
                    movementStopped = true;
                }
            }

        }else{
            reachedEndofPath = false;
            anim.SetBool("Sleeping", false);
        }
        if(Stats.energy <= 0.1*Stats.max_energy && sleeping == false){
                RedirectCourse(bed);
                currentWaipoint = 0;
                sleeping = true;
        }else if(Stats.energy >= Stats.max_energy && sleeping == true){
            RedirectCourse(/**/target.transform);
            currentWaipoint = 0;
            sleeping = false;
        }  
        anim.SetInteger("Speed",0);

        if(path.vectorPath[currentWaipoint].x > transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){
            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 1);
            anim.SetFloat("Y", 0);
            }
        else if(path.vectorPath[currentWaipoint].x < transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){

            anim.SetInteger("Speed",1);
            anim.SetFloat("X", -1);
            anim.SetFloat("Y", 0);

        }
        else if(path.vectorPath[currentWaipoint].y > transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){

            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1);

        }
        else if(path.vectorPath[currentWaipoint].y < transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){
            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1);
        }

        if(!movementStopped){
            //rb.velocity = new Vector2(anim.GetFloat("X") * 3, anim.GetFloat("Y") * 3);
            transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWaipoint], 0.05f);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance /*&& currentWaipoint < path.vectorPath.Count-1*/){
            currentWaipoint++;
        }
    }
}
