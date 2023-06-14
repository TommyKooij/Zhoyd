using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;
    #region VARIABLES
    private Vector3 respawnPoint;
    public float waitToRespawn;
    private GameObject thePlayer;
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
        thePlayer = PlayerHealthController.instance.gameObject;

        respawnPoint = thePlayer.transform.position;
    }

    #region METHODS
    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCO());
    }

    IEnumerator RespawnCO()
    {
        thePlayer.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(PlayerPrefs.GetString("LoadLevel"));

        thePlayer.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealthbar();
    }
    #endregion
}
