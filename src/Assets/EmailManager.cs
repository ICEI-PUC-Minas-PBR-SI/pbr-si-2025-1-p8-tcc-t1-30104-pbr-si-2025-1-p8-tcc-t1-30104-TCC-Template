using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class EmailManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI toText;
    [SerializeField]
    public TextMeshProUGUI fromText;
    [SerializeField]
    public TextMeshProUGUI subjectText;
    [SerializeField]
    public TextMeshProUGUI descriptionText;

    [SerializeField]
    public Animator emailAnimator;

    [SerializeField]
    public float gameTime = 180f;

    [SerializeField] private GameWinScreenController gameWinScreenController;
    [SerializeField] private GameOverScreenController gameOverScreenController;

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI timerText;

    [SerializeField] 
    public AudioClip correctClip;
    [SerializeField]
    public AudioClip wrongClip;
    


    private int playerScore = 0;

    private float timer;


    private List<Email> emailsOptions;

    private bool emailIsShow = false;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emailsOptions = new List<Email>();
        LoadEmailsFromJSON();
        timer = gameTime;
        UpdateHUD();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScore < 10) 
        {
            timer -= Time.deltaTime;
            UpdateHUD();
            if (timer <= 0)
            {
                gameOverScreenController.EnableGameOverScreen();
            }
        }

    }


    public void StartShowEmail(Email email)
    {
        toText.text = email.to;
        fromText.text = email.from;
        subjectText.text = email.subject;
        descriptionText.text = email.description;

        emailAnimator.SetBool("IsOpen", true);
        emailIsShow = true;
    }


    public void EndEmail(bool isPhishing)
    {
        emailAnimator.SetBool("IsOpen", false);
        emailIsShow = false;

        if (isPhishing)
        {
            audioSource.PlayOneShot(correctClip);
            playerScore++;
            UpdateHUD();

            if (playerScore >= 10)
            {
                gameWinScreenController.EnableGameWinScreen();
            }
        }
        else
        {
            audioSource.PlayOneShot(wrongClip);

        }
    }

    public bool GetEmailIsShow()
    {
        return emailIsShow;
    }

    public Email GetRandomEmail()
    {
        if (emailsOptions.Count == 0) return null;
        return emailsOptions[Random.Range(0, emailsOptions.Count)];
    }


    private void LoadEmailsFromJSON()
    {


        TextAsset jsonFile = Resources.Load<TextAsset>("emails");

        if (jsonFile != null)
        {
            EmailData emailData = JsonUtility.FromJson<EmailData>(jsonFile.text);
            emailsOptions = emailData.emails;
        }
        else
        {
            Debug.LogError("Arquivo JSON de e-mails não encontrado!");
        }
    }

    private void UpdateHUD()
    {
        scoreText.text = playerScore.ToString();

   
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
