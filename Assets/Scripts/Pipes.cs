using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] float pipesSpeed;
    [SerializeField] float disappearPos;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= disappearPos)
            Destroy(gameObject);
        else if (gameManager.gameStarted)
            transform.position += new Vector3(-pipesSpeed * Time.deltaTime, 0);
    }
}
