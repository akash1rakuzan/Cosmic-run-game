using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public Transform player;
    private int numberOfStickmans,numberOfEnemyStickMans;
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject stickMan;
    //******************************************

    [Range(0f,1f)][SerializeField] private float DistanceFactor, Radius;

    //----------move the player -------------------
    public bool moveByTouch, gameState;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed,roadSpeed;
    private new Camera camera;

    [SerializeField] private Transform road;
    [SerializeField] private Transform enemy;
    private bool attack;
    public static PlayerManager PlayerManagerInstance;

    public static int JumpCounter = 0;
    void Start()
    {
        player = transform;
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
        camera = Camera.main;
        PlayerManagerInstance = this;

        
    }


    void Update()
    {
        if (transform.childCount == 1)
        {
            SceneManager.LoadScene(0);
        }


        if (attack)
        {

            var enemyDirection = new Vector3(enemy.position.x,transform.position.y,enemy.position.z) - transform.position;
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 2f);
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;
                    if (Distance.magnitude < 8f)
                    {

                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, new Vector3(enemy.GetChild(1).GetChild(0).position.x, transform.GetChild(i).position.y, enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 1.5f);

                    }

                }


            }
            else 
            {

                attack = false;
                roadSpeed = 4f;

                
                FormatStickMan();
               
                for (int i = 1; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.identity;

                }

                    enemy.gameObject.SetActive(false);
            }
            if (transform.childCount == 1)
            {
                enemy.transform.GetChild(1).GetComponent<enemyManager>().StopAttacking();
                gameObject.SetActive(false);
            
            }
            

        }
        else 
        {
            
            MoveThePlayer();
        }

        if (gameState)
        {

            road.Translate(road.forward * Time.deltaTime * roadSpeed);
            for (int i = 1; i < transform.childCount; i++)
            {

                transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);

            }
        }


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
                    control.x = Mathf.Clamp(control.x, 4.15f, 9.5f);
                else if (numberOfStickmans > 30)
                    control.x = Mathf.Clamp(control.x, 3.15f, 10.5f);
                else if (numberOfStickmans > 20)
                    control.x = Mathf.Clamp(control.x, 2f, 11.65f);
                else if (numberOfStickmans > 10)
                    control.x = Mathf.Clamp(control.x, 1.3f, 12.35f);
                else
                    control.x = Mathf.Clamp(control.x, 0.65f, 13f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed)
                    , transform.position.y, transform.position.z);

            }
        }

        
    }

    public void FormatStickMan()
    {
        for (int i = 2; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            var NewPos = new Vector3(x, 0f, z);
            player.transform.GetChild(i).DOLocalMove(NewPos, 1f).SetEase(Ease.OutBack);
        }
       


    }

    private void MakeStickMan(int number) 
    {
    
        for (int i = numberOfStickmans; i < number; i++)
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

        if (other.CompareTag("enemy")) 
        {
            enemy = other.transform;
            attack = true;
            roadSpeed = 2.5f;

            other.transform.GetChild(1).GetComponent<enemyManager>().AttackThem(transform);

            StartCoroutine(UpdateTheEnemyAndPlayerStickMansNumbers());
        }
    
    }
    IEnumerator UpdateTheEnemyAndPlayerStickMansNumbers() 
    {
        numberOfEnemyStickMans = enemy.transform.GetChild(1).childCount-1;
        numberOfStickmans = transform.childCount - 1;

        while (numberOfEnemyStickMans > 0 && numberOfStickmans > 0) 
        {
            numberOfEnemyStickMans--;
            numberOfStickmans--;
           
            enemy.transform.GetChild(1).GetComponent<enemyManager>().CounterTxt.text = numberOfEnemyStickMans.ToString();
            CounterTxt.text = numberOfStickmans.ToString();
            yield return null;
        }

        if (numberOfEnemyStickMans == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.identity;
                enemy.transform.GetChild(1).GetComponent<enemyManager>().CounterTxt.text = "win";


            }

        }
        else 
        {

            CounterTxt.text = "lost";

        }

    }
}
