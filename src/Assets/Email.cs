using UnityEngine;
[System.Serializable]
public class Email
{


    public string to;
    
    public string from;

    public bool isValid;

    public string subject;

    [TextArea(3, 10)]
    public string description;

    

}
