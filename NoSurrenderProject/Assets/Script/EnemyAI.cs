using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Transform EnemyTransform;
    NavMeshAgent EnemyNavMesh;
    private void Start()
    {
        EnemyNavMesh = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        findClosetEnemy();
    }
    void findClosetEnemy()
    {
        float distanceToClosetEnemy = Mathf.Infinity;
        GameObject closetEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
       

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosetEnemy)
            {
                distanceToClosetEnemy = distanceToEnemy;
                closetEnemy = currentEnemy;
            }
            Debug.DrawLine(this.transform.position, closetEnemy.transform.position);
            Debug.Log("object " + closetEnemy + " distance" + distanceToClosetEnemy + " thisobject " + this.gameObject.name);
            if (distanceToClosetEnemy > 0.2f)
            {
                EnemyTransform = closetEnemy.transform;
                EnemyNavMesh.destination = EnemyTransform.transform.position;
                
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FallArea")
        {
            StartCoroutine(ExampleCoroutine001());
            IEnumerator
               ExampleCoroutine001()
            {

                yield return new
                    WaitForSeconds(0.5f);
                GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().freezeRotation = false;
            }
        }
      

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FallArea")
        {
            other.attachedRigidbody.AddForce(other.transform.forward * 50);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().freezeRotation = true;

        }
    }
    



}