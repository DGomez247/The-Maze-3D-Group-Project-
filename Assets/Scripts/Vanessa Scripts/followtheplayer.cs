using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class followtheplayer : MonoBehaviour
{


    public GameObject target = null;
    private NavMeshAgent nma = null;
    bool resting = false;
    public float maxBites;
    public AudioSource biteSound;
    float biteCound = 0;
    // Start is called before the first frame update
    public void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    private void Update()
    {
        if(!resting)
        nma.SetDestination(target.transform.position);

        FindClosestPlayer();

    }

    void FindClosestPlayer(){
        float distancetoclosestplayer = Mathf.Infinity;
        Player closestPlayer = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<closestPlayer>();

        foreach (Player currentPlayer in allPlayers){
            float distancetoplayer = (currentPlayer.transform.psoition - this.transform.position).sqrMagnitude;
            if (distancetoplayer < distancetoclosestplayer)
            {
                distancetoclosestplayer = distancetoplayer;
                closestPlayer = currentPlayer;

                GameObject.gameObject.tag = "closestPlayer";
            }
            else{
                GameObject.gameObject.tag = "Player";
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if (collision.collider.CompareTag("closestPlayer")&&!resting)
        {
            StartCoroutine(bite());
        }
    }
    private IEnumerator bite()
    {
        biteSound.Play();
        biteCound++;
        resting = true;
        yield return new WaitForSeconds(1);
        if (biteCound == maxBites)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        resting = false;
    }
}
