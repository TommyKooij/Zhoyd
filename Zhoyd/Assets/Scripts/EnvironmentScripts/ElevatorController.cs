using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    public static ElevatorController instance;
    #region VARIABLES
    private PlayerController player;

    public float timeBeforeFade;
    public Vector3 positionToGo;
    public string levelToLoad;
    public float moveSpeed;

    public bool isGoingDown;
    public bool isGoingUp;

    private bool hasArrived = false;
    private bool usingElevator = false;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingElevator == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, positionToGo, moveSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetAxisRaw("Vertical") < -.9f)
        {
            if (!hasArrived)
            {
                player.canMove = false;
                usingElevator = true;
                StartCoroutine(UseElevatorCo());
            }
        }
    }

    IEnumerator UseElevatorCo()
    {
        yield return new WaitForSeconds(timeBeforeFade);

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);

        UIController.instance.StartFadeFromBlack();

        SceneManager.LoadScene(levelToLoad);

        if (gameObject.transform.position == positionToGo)
        {
            player.canMove = true;
            hasArrived = true;
            usingElevator = false;
        }
    }
}
