using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
   private float ejemplo; //primitiva
   [SerializeField] private float speed;
   [SerializeField] private Transform patrolPath;
   
   private List<Vector3> patrolPoints = new ();

   private int currentIndex = 0; //Saber por que punto voy
   private void Awake()
   {
      foreach (Transform child in patrolPath)
      {
         patrolPoints.Add(child.position);
      }
   }

   void Update()
   {
      //transform.Translate(); //Me muevo en una dirección
      transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentIndex], speed * Time.deltaTime );//Me muevo hasta un punto
      
      if(transform.position == patrolPoints[currentIndex])
      {

         SetNewDestination();
      }

     
   }
   private void SetNewDestination()
   {
       currentIndex =(currentIndex+1) % patrolPoints.Count;
       
       
       transform.eulerAngles = transform.position.x > patrolPoints[currentIndex].x ? new Vector3(0,180,0) : Vector3.zero;
   }  //    ASIGNACIÓN     //  //                    CONDICIÓN                  //   //      SI      //   //     NO     //
}
