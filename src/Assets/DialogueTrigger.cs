using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    [SerializeField]
    public float detectionRadius = 5f;

    [SerializeField] 
    private float dialogueCooldown = 1.5f;

    [SerializeField]
    public AudioClip[] audioClipsToTalk;

    [SerializeField]
    public GameObject informationUI;


    public DialogueManager manager;
    private float lastDialogueTime = 0f;
    private Animator animator;
    private bool hasAnimator;
    private AudioSource audioSource;
    private int lastAudioIndex = -1;


    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        bool playerDetected = false;

        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders) {

                if (collider.CompareTag("Player"))
                {
                    manager.SetNpcForStartDialogue(dialogue.name);
                    playerDetected = true;
                    float timeToPressFAgain = Time.time - lastDialogueTime;
                    
                    

                    if (Input.GetKeyDown(KeyCode.F) && timeToPressFAgain >= dialogueCooldown)
                    {
                        lastDialogueTime = Time.time;
                        TriggerDialogue();
                    }
                }
            }
        }
        if(manager.currentNPC == dialogue.name)
        {
            if (!manager.GetAlreadyStart())
            {
                informationUI.gameObject.SetActive(true);
                if (hasAnimator)
                {
                    animator.SetBool("IsTalking", false);
                }
            }
            else
            {
                informationUI.gameObject.SetActive(false);
            }


            if (!playerDetected)
            {
                OnPlayerExit();
            }

        }



    }

    private void PickAudioClip()
    {
        if (audioClipsToTalk.Length > 0)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, audioClipsToTalk.Length);
            } while (randomIndex == lastAudioIndex && audioClipsToTalk.Length > 1);

            lastAudioIndex = randomIndex;
            audioSource.clip = audioClipsToTalk[randomIndex];
            audioSource.Play();
        }

    }

    private void OnPlayerExit()
    {
        informationUI.gameObject.SetActive(false);
        manager.EndDialogue();

    }
    public void TriggerDialogue()
    {
        if (hasAnimator)
        {
            animator.SetBool("IsTalking", true);
        }
        PickAudioClip();
        if (!manager.GetAlreadyStart())
        {
            manager.StartDialogue(dialogue);
        }
        else
        {
            manager.DisplayNextSentence();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
