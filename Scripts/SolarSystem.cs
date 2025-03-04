using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float rotationSpeed = 1f; // Adjust the speed as needed

    //real value of gravitational constant is 6.67408 � 10-11
    //can increase to make thing go faster instead of increase timestep of Unity
    readonly float G = 1000f;
    GameObject[] celestials;

    [SerializeField]
    bool IsElipticalOrbit = false;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("celestial");

        SetInitialVelocity();
    }

    void Update()
    {
        GameObject[] celestials = GameObject.FindGameObjectsWithTag("celestial");

        foreach (GameObject celestial in celestials)
        {
            celestial.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    if (IsElipticalOrbit)
                    {
                        // Eliptic orbit = G * M  ( 2 / r + 1 / a) where G is the gravitational constant, M is the mass of the central object, r is the distance between the two bodies
                        // and a is the length of the semi major axis (!!! NOT GAMEOBJECT a !!!)
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        //Circular Orbit = ((G * M) / r)^0.5, where G = gravitational constant, M is the mass of the central object and r is the distance between the two objects
                        //We ignore the mass of the orbiting object when the orbiting object's mass is negligible, like the mass of the earth vs. mass of the sun
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }
}