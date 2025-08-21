using System.Collections;
using UnityEngine;

public class destroy : MonoBehaviour
{
    
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    
}
