using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

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
    //public Dropdown drop;
    //private ChageOptions chopt;


    // Start is called before the first frame update
    void Start()
    {
        //chopt = drop.GetComponent<ChageOptions>();
        //target.position = new Vector2(target.position.x, target.position.y-1);
        //if(target==bed){
       // target = bed;
        //new_target = new GameObject();   

        if(transform.tag == "Human"){
            O2_needed = 1;
            H20_needed = 1;
            FOOD_needed = 1;
            max_energy = 100;
            
        }
        else if(transform.tag == "Neuforian"){
            O2_needed = -1;
            H20_needed = 3;
            FOOD_needed = 0;
            max_energy = 150;
        }

        ENGbar.SetMaxEnergy(max_energy);
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
            if(!sleeping){
                energy--;
            }else{
                anim.SetBool("Sleeping", true);
                transform.position = bed.position;
                energy++;
                if(energy==max_energy){
                    movementStopped = false;
                }else{
                    movementStopped = true;
                }
            }

        }else{
            reachedEndofPath = false;
            anim.SetBool("Sleeping", false);
        }

        if(energy <= 0.1*max_energy && sleeping == false){
                RedirectCourse(bed);
                currentWaipoint = 0;
                sleeping = true;
        }else if(energy >= max_energy && sleeping == true){
            RedirectCourse(/**/target.transform);
            currentWaipoint = 0;
            sleeping = false;
        }  
        anim.SetInteger("Speed",0);

        if(path.vectorPath[currentWaipoint].x >= transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){
            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 1);
            anim.SetFloat("Y", 0);
            }
        else if(path.vectorPath[currentWaipoint].x < transform.position.x && 
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

        //transform.position =Vector2.Lerp(transform.position,path.vectorPath[currentWaipoint],0.07f);
        if(!movementStopped){
            rb.velocity = new Vector2(anim.GetFloat("X") * 3, anim.GetFloat("Y") * 3);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance /*&& currentWaipoint < path.vectorPath.Count-1*/){
            currentWaipoint++;
        }
    }
}
