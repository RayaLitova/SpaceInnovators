using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class NpcAi : MonoBehaviour
{

    [SerializeField] public Transform actualTarget;
    //[SerializeField] public Transform TempTarget;
    NPCStats Stats;
    //[SerializeField] NPCStats stats;

    float speed = 0.02f;
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
    private Animator bedAnim;
    [SerializeField] Transform NPCGFX;
    GetWorkNeeded Working;

    bool sleeping = false;
    bool wanderingForUpgrade = false;
    bool wanderingForWork = false;
    bool isPanicked = false;

    public GameObject currentRoom;

    int currentPanicWaypoint = 0;

    Transform currentTarget;
    
    [SerializeField]public Transform bed;

    void Start()
    {
        Stats = transform.GetComponent<NPCStats>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = NPCGFX.GetComponent<Animator>();
        bedAnim = bed.GetComponent<Animator>();

        if(/**/actualTarget==null){
            wanderingForWork = true;
            currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
        }else{
            if(actualTarget.tag == "Upgrading"){
                wanderingForUpgrade=true;
            }
        }

        seeker.StartPath(rb.position, /**/currentTarget.transform.position, OnPathComplete);
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
        currentWaipoint = 0;
    }

    public void Die(){
        actualTarget.tag = actualTarget.tag.Split(':')[1];
        bed.tag = "Bed";
        Destroy(gameObject);
        BuildRegulator mario = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
        mario.onBoardCount[transform.tag.Split('-')[0]]--;
    }

    GameObject GetPanicWaypoint(){
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("Panic")){
            Debug.Log(i.name);
            if(i.name == "Panic(" + currentPanicWaypoint + ")" && i.transform.parent.parent.gameObject == currentRoom){
                return i;
            }
        }
        return null;
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
        if(h2o_timer >= 30f ){
            Stats.Consume("Water", Stats.Water);
            h2o_timer =0;
        }
        if(food_timer >= 40f ){
            Stats.Consume("Food", Stats.Food);
            food_timer=0;
        }

        if(path == null){
            return;
        }

        if(isPanicked){
        }else if(wanderingForWork){
            if(GameObject.FindGameObjectsWithTag(Stats.targetTag).Length!=0){
                actualTarget = GameObject.FindGameObjectsWithTag(Stats.targetTag)[0].transform;
                actualTarget.tag = "Used:"+ actualTarget.tag;
                currentTarget = actualTarget;
                wanderingForWork=false;
            }
        }else if(wanderingForUpgrade){
            if(actualTarget.tag != "Upgrading" && actualTarget.tag != "Closed"){
                currentTarget=actualTarget;
                wanderingForUpgrade = false;
            }
        }else if(actualTarget != null && (actualTarget.tag == "Upgrading" || actualTarget.tag == "Closed")){
            wanderingForUpgrade = true;
        }/*else{
             // za mehanicite i doktorite trqbva da se napravi kato wandervat za rabota da se setva actual target na null \/
            if(actualTarget.tag != "Upgrading" && actualTarget.tag != "Closed" && actualTarget.tag != Stats.targetTag){
                actualTarget = null;
                //currentTarget=actualTarget;
                //eventualno tuka shte bugne posle kato nqkoi mehanik se opita da spi dokato wanderva zaradi movementStopped promenite
                wanderingForWork = true;
                movementStopped = false;
            }
        }    */

        if(currentRoom.tag == "Closed"){
            if(!isPanicked){
                sleeping = false;
                anim.SetBool("Sleeping", false);
                bedAnim.SetBool("Sleeping", false);
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
                currentPanicWaypoint++;
                isPanicked = true;   
            }
            movementStopped = false;
        }else if(currentTarget != null && currentTarget.parent.gameObject.tag == "Closed"){
            if(currentTarget==actualTarget || actualTarget == null){
                do{
                    currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                    if(actualTarget == null) actualTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                }while(currentTarget.parent.gameObject.tag == "Closed" || actualTarget == currentTarget);
                RedirectCourse(currentTarget);
                isPanicked = false;
                currentPanicWaypoint = 0;
            }
        }else{
            currentTarget = actualTarget;
            if(currentTarget == null)
                currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
            currentPanicWaypoint = 0;
            isPanicked = false;
        }

        if(currentWaipoint >= path.vectorPath.Count){
            reachedEndofPath = true;
            if(isPanicked){  
                sleeping = false;
                anim.SetBool("Sleeping", false);
                bedAnim.SetBool("Sleeping", false);
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
                currentPanicWaypoint++;   
                if(currentPanicWaypoint == 3) currentPanicWaypoint = 0;
            }else if(wanderingForUpgrade || wanderingForWork){
                sleeping = false;
                anim.SetBool("Sleeping", false);
                bedAnim.SetBool("Sleeping", false);
                speed = 0.02f;           
                RedirectCourse(GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform);
            }else{
                speed = 0.04f;
                if(!sleeping){
                    Stats.energy--;
                    currentTarget.parent.gameObject.GetComponent<RoomStatics>().Produce();
                }else{
                    bedAnim.SetBool("Sleeping", true);
                    anim.SetBool("Sleeping", true);
                    transform.position = bed.position;
                    Stats.energy++;
                    if(Stats.energy==Stats.max_energy){
                        movementStopped = false;
                    }else{
                        movementStopped = true;
                    }
                }
            }

        }else{
            reachedEndofPath = false;
            bedAnim.SetBool("Sleeping", false);
            anim.SetBool("Sleeping", false);
        }
        if(Stats.energy <= 0.1*Stats.max_energy && sleeping == false ){
                RedirectCourse(bed);
                //currentWaipoint = 0;
                sleeping = true;
        }else if(Stats.energy >= Stats.max_energy && sleeping == true ){
            RedirectCourse(/**/currentTarget.transform);
            //currentWaipoint = 0;
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
            transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWaipoint], speed);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance /*&& currentWaipoint < path.vectorPath.Count-1*/){
            currentWaipoint++;
        }
    }
}
