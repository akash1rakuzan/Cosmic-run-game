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

    //----------move the player -------------------
    public bool moveByTouch, gameState;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed;
    private Camera camera;


    void Start()
    {
        player = transform;
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
        camera = Camera.main;
    }


    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            moveByTouch = true;

            var plane = new Plane(Vector3.up, 0f);

            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;

        }

        if (moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);

                var move = mousePos - mouseStartPos;

                var control = playerStartPos + move;


                if (numberOfStickmans > 50)
                    control.x = Mathf.Clamp(control.x, 3f, 10f);
                else
                    control.x = Mathf.Clamp(control.x, 0f, 13f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed)
                    , transform.position.y, transform.position.z);

            }
        }
    }

    private void FormatStickMan()
    {
        for (int i = 1; i < player.childCount; i++)
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
