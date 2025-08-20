using DG.Tweening;
using TMPro;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject stickMan;
    //******************************************

    [Range(0f, 1f)][SerializeField] private float DistanceFactor, Radius;

    void Start()
    {
        for (int i = 0; i < Random.Range(20, 120); i++)
        {
            Instantiate(stickMan, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        
        }

        CounterTxt.text = (transform.childCount - 1).ToString();
        FormatStickMan();


    }

    private void FormatStickMan()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            var NewPos = new Vector3(x, 0f, z);
            transform.transform.GetChild(i).localPosition = NewPos;
        }



    }
    void Update()
    {
        
    }
}
