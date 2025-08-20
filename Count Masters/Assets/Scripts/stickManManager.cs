using UnityEngine;

public class stickManManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other ) 
    {
        if (other.CompareTag("red") && other.transform.parent.childCount > 0) 
        {

            Destroy(other.gameObject);
            Destroy(gameObject);
        
        }
    
    
    }
   
    
}

