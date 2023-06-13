using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsCameraController : MonoBehaviour
{
    #region VARIABLES
    private Vector3 offset;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        offset.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerHealthController.instance.transform.position + offset;
    }
}
