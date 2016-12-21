using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    public float speed;
    public float acceleration;
    float originalAcceleration;
    public float maxSpeed;
    float originalMaxSpeed;
    public float rotationSpeed;
    public float forceExplosion;
    public float forceConstant;
    public Vector3 cenMass;
    static int score;
    float time;
    bool hasRocket;
    float timeForRocket;
    Rigidbody rb;
    bool grounded;
    Text CarSpeed;
    Text BoostTime;
    Vector3 boostOriginalLocation;
    Quaternion boostRotation;
    bool willReson;
    float timeBoostRespon;
    // Use this for initialization
	void Start ()
    {
        timeBoostRespon = -10.0f;
        originalAcceleration = acceleration;
        originalMaxSpeed = maxSpeed;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cenMass;
        CarSpeed = GameObject.Find("carspeed").GetComponent<Text>();
        BoostTime = GameObject.Find("boostTime").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(timeBoostRespon > 0)
        {
            timeBoostRespon -= Time.deltaTime;
        }
        if (timeBoostRespon < 0.0f && timeBoostRespon != -10.0f)
        {
            willReson = true;
        }
        if(willReson)
        {
            willReson = false;
            timeBoostRespon = -10.0f;
            Instantiate(GameObject.FindGameObjectWithTag("boost"), boostOriginalLocation, boostRotation);
        }
        doBoost();
        CarSpeed.text = speed.ToString();
        if (true)
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rotate back");
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, 180.0f, transform.rotation.z, transform.rotation.w), .1f * Time.deltaTime);
        }
	
	}
    void doBoost()
    {
        if(timeForRocket <= 0)
        {
            hasRocket = false;
        }
        if (hasRocket)
        {
            timeForRocket -= Time.deltaTime;
            BoostTime.text = timeForRocket.ToString();
        }
        else
        {
            BoostTime.text = "No Boost";
            maxSpeed = originalMaxSpeed;
            acceleration = originalAcceleration;
        }
        
        if(hasRocket && Input.GetKeyDown(KeyCode.Space))
        {
            maxSpeed *= 2;
            acceleration *= 5;
            timeForRocket = 2;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        
        if(col.collider.tag == "Ball")
        {
            Debug.Log("IN COLlision");
            col.rigidbody.AddForce(transform.forward * forceExplosion);
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "ground")
        {
            grounded = true;

        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "ground")
        {
            grounded = false;
        }
        

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boost" && !hasRocket)
        {
            hasRocket = true;
            timeForRocket = 10;
            boostOriginalLocation = other.transform.position;
            boostRotation = other.transform.rotation;
            Destroy(other.gameObject);
            timeBoostRespon = 30;
        }
    }
    private void Move()
    {
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        if(translation != 0 && grounded)
        {
            if (speed < maxSpeed)
            {
                speed += acceleration * translation;
            }
        }
        else
        {
            if (speed > 0.0f)
            {
                speed -= acceleration*2;
            }
            else
            {
                speed += acceleration*2;
            }
            if(speed < .5f && speed > -.5f)
            {
                speed = 0;
                
            }
        }
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, 1.0f*speed*Time.deltaTime);
       // rb.velocity = speed * rb.velocity.normalized;
        if (speed > 1.0f || speed < -1.0f)
        {
            transform.Rotate(0, rotation*(speed*rotationSpeed), 0);
        }
    }

    void ApplyDownForce()
    {
        rb.AddForce(-transform.up * forceConstant * rb.velocity.magnitude);
    }
}
