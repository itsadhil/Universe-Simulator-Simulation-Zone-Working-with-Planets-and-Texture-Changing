using UnityEngine;
using System.Collections;

public class ItemGrab : MonoBehaviour
{
    public DisolveEffCall ScriptA;  // Reference to dissolve effect script
    public Transform handPosition;
    public float throwForceMultiplier = 1.5f;
    public GameObject[] grabbableObjects = new GameObject[10];

    private GameObject currentItem;
    private Rigidbody currentItemRb;
    private Vector3 lastHandPosition;
    private GameObject potentialItem;
    private Vector3 originalPosition; // Store original position before grabbing
    private Quaternion originalRotation; // Store original rotation before grabbing

    void Start()
    {
        lastHandPosition = handPosition != null ? handPosition.position : Vector3.zero;

        if (handPosition == null)
        {
            Debug.LogError("Hand Position is not assigned in the inspector!");
        }
        if (ScriptA == null)
        {
            Debug.LogError("ScriptA (Dissolve Effect) is not assigned in the inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < grabbableObjects.Length; i++)
        {
            if (grabbableObjects[i] != null && other.gameObject == grabbableObjects[i])
            {
                potentialItem = grabbableObjects[i];
                break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (potentialItem != null && other.gameObject == potentialItem)
        {
            potentialItem = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentItem != null)
            {
                DropItem();
            }
            else if (potentialItem != null)
            {
                GrabItem(potentialItem);
            }
        }
    }

    void FixedUpdate()
    {
        if (currentItem != null && handPosition != null && currentItem.transform.parent == handPosition)
        {
            lastHandPosition = handPosition.position;
        }
    }

    void GrabItem(GameObject item)
    {
        if (currentItem != null || item == null) return;

        currentItem = item;
        currentItemRb = currentItem.GetComponent<Rigidbody>();

        if (currentItemRb == null)
        {
            Debug.LogError("Grabbed item has no Rigidbody!");
            return;
        }

        // Store original position and rotation
        originalPosition = currentItem.transform.position;
        originalRotation = currentItem.transform.rotation;

        // Stop orbiting if the object has PlanetOrbit script
        PlanetOrbit orbitScript = currentItem.GetComponent<PlanetOrbit>();
        if (orbitScript != null)
        {
            orbitScript.Grabbed();
        }

        // Disable trail effect while grabbing
        TrailRenderer trail = currentItem.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.enabled = false;
        }

        // Parent to hand and reset position
        currentItem.transform.SetParent(handPosition);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        currentItemRb.isKinematic = true;

        // Call dissolve effect when grabbing a planet
        if (ScriptA != null && currentItem.CompareTag("Planet"))
        {
            ScriptA.StartDissolver();
        }
    }

    void DropItem()
    {
        if (currentItem == null || currentItemRb == null) return;

        currentItem.transform.SetParent(null);
        currentItemRb.isKinematic = false;

        // Start returning planet to original position
        StartCoroutine(ReturnToOrbit(currentItem, originalPosition, originalRotation));

        // Re-enable orbit if it was affected
        PlanetOrbit orbitScript = currentItem.GetComponent<PlanetOrbit>();
        if (orbitScript != null)
        {
            orbitScript.Released();
        }

        // Re-enable trail effect
        TrailRenderer trail = currentItem.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.enabled = true;
        }

        // Reset dissolve effect when dropping
        if (ScriptA != null && currentItem.CompareTag("Planet"))
        {
            ScriptA.setItBackToOriginalDissolve();
        }

        currentItem = null;
        currentItemRb = null;
    }

    IEnumerator ReturnToOrbit(GameObject item, Vector3 originalPos, Quaternion originalRot)
    {
        if (item == null) yield break;

        float duration = 1.5f;
        float elapsed = 0f;
        Vector3 startPos = item.transform.position;
        Quaternion startRot = item.transform.rotation;

        while (elapsed < duration)
        {
            if (item == null) yield break;

            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            item.transform.position = Vector3.Lerp(startPos, originalPos, t);
            item.transform.rotation = Quaternion.Lerp(startRot, originalRot, t);
            yield return null;
        }

        if (item != null)
        {
            item.transform.position = originalPos;
            item.transform.rotation = originalRot;
        }
    }
}
