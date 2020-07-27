using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [SerializeField] GameObject pipes;
    [SerializeField] float distanceBetweenPipes;

    [SerializeField] float pipesRange;
    [SerializeField] float pipesAppearPoint;

    public bool gameStarted = false;

    Transform previousPipes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((previousPipes == null || previousPipes.position.x + distanceBetweenPipes < pipesAppearPoint) && gameStarted)
        {
            GameObject newPipes = Instantiate(pipes);
            newPipes.GetComponent<Pipes>().gameManager = this;
            newPipes.transform.position = new Vector3(pipesAppearPoint, (Random.value - 0.5f) * 2 * pipesRange, 0);
            previousPipes = newPipes.transform;
        }
    }

    public AudioSource AddAudio(GameObject gameObject, AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }
}
