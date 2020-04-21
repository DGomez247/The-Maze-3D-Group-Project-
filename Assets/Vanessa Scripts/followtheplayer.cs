using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followtheplayer : MonoBehaviour
{


    public GameObject target = null;
    private NavMeshAgent nma = null;

    // Start is called before the first frame update
    public void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    private void Update()
    {
        nma.SetDestination(target.transform.position);
    }

}
