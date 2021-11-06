using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyMove : MonoBehaviour
{
    [HideInInspector] public Vector3[] wayPoints;
    [HideInInspector] public float moveSpeed;

    private int currentWayPointIndex = 0;
    private float lastWayPointSwitchTime;



    private void Awake()
    {
        moveSpeed = 5.0f;
        wayPoints = new Vector3[]
        {   new Vector3(-20.0f, 2.3f),
            new Vector3(10.6f, 2.3f),
            new Vector3(11.3f, -1.3f),
            new Vector3(-11.6f, -1.5f),
            new Vector3(-11.6f, -5.6f),
            new Vector3(12.0f, -5.6f)
        };
        
    }


    // Start is called before the first frame update
    void Start()
    {
        lastWayPointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPoint = wayPoints[currentWayPointIndex];
        Vector3 endPoint = wayPoints[currentWayPointIndex + 1];

        float pathLength = Vector3.Distance(startPoint, endPoint);
        float totalTimeForPath = pathLength / moveSpeed;
        float currentTimeOnPath = Time.time - lastWayPointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPoint))
        {
            if (currentWayPointIndex <= wayPoints.Length - 2)
            {
                currentWayPointIndex++;
                lastWayPointSwitchTime = Time.time;
            }
            if (currentWayPointIndex == 1)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }

            if (currentWayPointIndex == 2)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(-180, Vector3.forward);
            }

            if (currentWayPointIndex == 3)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }

            if (currentWayPointIndex == 4)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
            if (currentWayPointIndex == 5)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

        }

    }
}
