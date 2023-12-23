using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   
       public void OnCollisionEnter(Collision other)
       {
            if (other.gameObject.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemyComponent))
            {
                Debug.Log("vurduı");
                enemyComponent.TakeDamage(1);
                Destroy(gameObject);
                
            }
            
            
       }
   
}
