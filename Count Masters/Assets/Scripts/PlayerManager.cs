using UnityEngine;
using TMPro;
using DG.Tweening;
public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberOfStickmans;
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject stickMan;
    //******************************************

    [Range(0f,1f)][SerializeField] private float DistanceFactor, Radius;
    void Start()
    {
        player = transform;
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
    }

    void Update()
    {
        
    }

    private void FormatStickMan()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            var NewPos = new Vector3(x, 0f, z);
            player.transform.GetChild(i).DOLocalMove(NewPos, 1f).SetEase(Ease.OutBack);
        }
        Debug.Log($"Format Stickmans called on {Time.fixedTime}");


    }

    private void MakeStickMan(int number) 
    {
    
        for (int i = 0; i < number; i++)
        {

            Instantiate(stickMan, transform.position, Quaternion.identity, transform);
            
        }
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
        FormatStickMan();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateManager = other.GetComponent <GateManager>();

            if (gateManager.multiply)
            {
                MakeStickMan(numberOfStickmans * gateManager.randomNumber);


            }
            else 
            {
                MakeStickMan(numberOfStickmans + gateManager.randomNumber);

            }
            

        }
    
    }
}
