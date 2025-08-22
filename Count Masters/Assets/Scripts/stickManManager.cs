using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class stickManManager : MonoBehaviour
{
    [SerializeField] ParticleSystem blood;
    private void OnTriggerEnter(Collider other ) 
    {
        //if (other.CompareTag("red") && other.transform.parent.childCount > 0) 
        //{

        //    Destroy(other.gameObject);
        //    Destroy(gameObject);

        //    Instantiate(blood,new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z),Quaternion.identity);
        
        //}

        switch (other.tag) 
        {
        
            case "red":
                if (other.transform.parent.childCount > 0) 
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    Instantiate(blood, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                }

                break;


            case "jump":
                transform.DOJump(transform.position,5f,1,1.5f).SetEase(Ease.Flash).OnComplete(PlayerManager.PlayerManagerInstance.FormatStickMan);
                
                break;

        }


    }
   
    
}

