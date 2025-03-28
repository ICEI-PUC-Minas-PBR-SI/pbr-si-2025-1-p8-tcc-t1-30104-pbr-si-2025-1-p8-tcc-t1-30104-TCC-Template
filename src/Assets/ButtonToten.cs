using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToten : MonoBehaviour
{
    [SerializeField]
    public float detectionRadius = 5f;

    [SerializeField]
    public string nameScene;

    [SerializeField]
    private GameObject uiMessage;

    private bool playerInside = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playerDetected = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {

                if (collider.CompareTag("Player"))
                {
                    playerDetected = true;

                    if(!playerInside)
                    {
                        playerInside = true;
                        uiMessage.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        SceneManager.LoadScene(nameScene);
                    }
                }
            }
        }

        if (!playerDetected && playerInside)
        {
            uiMessage.SetActive(false);
            playerInside = false;


        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
