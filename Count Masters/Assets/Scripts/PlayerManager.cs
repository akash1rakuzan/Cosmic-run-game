using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberOfStickmans;
    [SerializeField] private TextMeshProUGUI CounterTxt;
    [SerializeField] private GameObject stickMan;

    void Start()
    {
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
    }

    void Update()
    {
        
    }

    private void MakeStickMan(int number) 
    {
    
        for (int i = 0; i < number; i++)
        {

            Instantiate(stickMan, transform.position, Quaternion.identity, transform);
            
        }
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();

    }

    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            
          
        }
    
    }
}
