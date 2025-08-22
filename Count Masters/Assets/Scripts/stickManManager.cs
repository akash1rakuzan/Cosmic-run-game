using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;
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
                PlayerManager.JumpCounter++;

                Vector3 startPos = transform.position;
                float jumpDuration = 1.5f;
                float jumpHeight = 5f;

                // Store offset relative to player
                float xOffset = transform.position.x - PlayerManager.PlayerManagerInstance.transform.position.x;

                // Animate Y only (jump arc)
                transform.DOMoveY(startPos.y + jumpHeight, jumpDuration / 2f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        transform.DOMoveY(startPos.y, jumpDuration / 2f)
                            .SetEase(Ease.InQuad);
                    });

                // Follow player but keep original offset
                StartCoroutine(FollowPlayerDuringJump(jumpDuration, xOffset));

                break;


        }


    }

    private IEnumerator FollowPlayerDuringJump(float duration, float xOffset)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 pos = transform.position;
            pos.x = PlayerManager.PlayerManagerInstance.transform.position.x + xOffset; // keep offset
            transform.position = pos;

            timer += Time.deltaTime;
            yield return null;
        }

        // Jump finished
        PlayerManager.JumpCounter--;

        if (PlayerManager.JumpCounter == 0)
        {
            PlayerManager.PlayerManagerInstance.FormatStickMan();
        }
    }

}

