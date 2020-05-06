using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class followtheplayer : MonoBehaviour
{
    GameObject closestPlayer = null;
    public GameObject target = null;
    private NavMeshAgent nma = null;
    bool resting = false;
    public float maxBites;
    public AudioSource biteSound;
    float biteCound = 0;
    public Animator animator;
    // Start is called before the first frame update
    public void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (!resting)
        {

            FindClosestPlayer();

            nma.SetDestination(closestPlayer.transform.position);



        }
    }

    void FindClosestPlayer()
    {
        float distancetoclosestplayer = Mathf.Infinity;

        thisPlayer[] allPlayers = GameObject.FindObjectsOfType<thisPlayer>();

        foreach (thisPlayer currentPlayer in allPlayers)
        {
            float distancetoplayer = (currentPlayer.transform.position - this.transform.position).sqrMagnitude;
            if (distancetoplayer < distancetoclosestplayer)
            {
                distancetoclosestplayer = distancetoplayer;
                closestPlayer = currentPlayer.gameObject;

            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if (collision.collider.CompareTag("Player")&&!resting)
        {
            
            StartCoroutine(bite());
        }
    }
    private IEnumerator bite()
    {
        biteSound.Play();
        biteCound++;
        FindObjectOfType<countdown1>().time-=30;
        animator.SetTrigger("attack");
        resting = true;
        yield return new WaitForSeconds(1);
        if (biteCound == maxBites)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        resting = false;
    }
    public void hit(){
        StartCoroutine(stun());
    }
    public IEnumerator stun()
    {
        biteSound.Play();
        animator.SetBool("idle",true);
        resting = true;
        yield return new WaitForSeconds(5);
        resting = false;
        animator.SetBool("idle",false);
    }
}
