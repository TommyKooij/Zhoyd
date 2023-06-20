using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    #region VARIABLES
    private PlayerController player;

    public float timeBeforeFade;
    public float timeAfterFade;
    public Vector3 positionToGo;
    public GameObject placePlayer;
    public float moveSpeed;
    public string levelToLoad;

    public bool up, down = false;
    public bool usingElevator = false;
    private ElevatorController elevators;
    private ElevatorActivator activator;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();

        activator = FindObjectOfType<ElevatorActivator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingElevator == true)
        {
            player.canMove = false;
            player.isUsingElevator = true;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, positionToGo, moveSpeed * Time.deltaTime);
            placePlayer.SetActive(true);
            player.transform.position = placePlayer.transform.position;

            if (gameObject.transform.position == positionToGo)
            {
                player.canMove = true;
                player.isUsingElevator = false;
                activator = FindObjectOfType<ElevatorActivator>();
                activator.SpawnElevator();
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && ((Input.GetAxisRaw("Vertical") < -.9f && down == true) || (Input.GetAxisRaw("Vertical") > .9f && up == true)))
        {
            usingElevator = true;
            //StartCoroutine(UseElevatorCo());
        }
    }

    IEnumerator UseElevatorCo()
    {
        player.canMove = false;
        player.isUsingElevator = true;

        yield return new WaitForSeconds(timeBeforeFade);

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1f);

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(levelToLoad);

        yield return new WaitForSeconds(2f);

        UIController.instance.StartFadeFromBlack();

        yield return new WaitForSeconds(.1f);
        activator = FindObjectOfType<ElevatorActivator>();
        activator.DestroyElevator();

        yield return new WaitForSeconds(timeAfterFade);

        placePlayer.SetActive(false);
        player.canMove = true;
        usingElevator = false;

        player.isUsingElevator = false;
        activator.SpawnElevator();
        Destroy(gameObject);
    }
}
