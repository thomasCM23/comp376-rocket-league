using UnityEngine;
using System.Collections;

public class movePillar : MonoBehaviour {

    // Use this for initialization
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        if(startMarker.position.z == transform.position.z)
        {
            Transform temp = startMarker;
            startMarker = endMarker;
            endMarker = temp;
        }
        if(endMarker.position.z == transform.position.z)
        {
            Transform temp = endMarker;
            endMarker = startMarker;
            startMarker = temp;
        }
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
}
