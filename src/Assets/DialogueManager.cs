using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI nameText;
    [SerializeField]
    public TextMeshProUGUI dialogueText;

    [SerializeField]
    public Animator dialogueAnimator;
    
    private Queue<string> sentences;

    private bool alreadyStart = false;

    [HideInInspector]
    public string currentNPC = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetNpcForStartDialogue(string name)
    {
        currentNPC = name;
    }


    public void StartDialogue( Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        currentNPC = dialogue.name;
        dialogueAnimator.SetBool("IsOpen", true);
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        alreadyStart = true;
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentece = sentences.Dequeue();
        dialogueText.text = sentece;


    }

    public void EndDialogue()
    {
        currentNPC = "";
        dialogueAnimator.SetBool("IsOpen", false);
        alreadyStart = false;
    }

    public bool GetAlreadyStart()
    {
        return alreadyStart;
    }

}
