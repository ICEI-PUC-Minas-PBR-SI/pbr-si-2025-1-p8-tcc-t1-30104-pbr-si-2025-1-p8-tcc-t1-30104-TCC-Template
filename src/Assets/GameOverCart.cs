using UnityEngine;

public class GameOverCart : MonoBehaviour
{

    [SerializeField] public GameOverScreenController gameOverScreenController;


    private HealthSystem healthSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     healthSystem = GetComponent<HealthSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.health <= 0 && gameObject.CompareTag("Cart"))
        {
            //gameOverScreenController.EnableGameOverScreen();
        }
    }
}
