using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    public DisolveEffCall ScriptA;
    public OrbitingObjects ScriptB;

    public Transform handPosition;
    public float throwForceMultiplier = 1.5f;
    public GameObject[] grabbableObjects = new GameObject[10];
    private GameObject currentItem;
    private Rigidbody currentItemRb;
    private Vector3 lastHandPosition;
    private GameObject potentialItem;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        lastHandPosition = handPosition.position;
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < grabbableObjects.Length; i++)
        {
            if (other.gameObject == grabbableObjects[i])
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
        if (currentItem != null && currentItem.transform.parent == handPosition)
        {
            lastHandPosition = handPosition.position;
        }
    }

    void GrabItem(GameObject item)
    {
        if (currentItem != null) return;

        currentItem = item;
        currentItemRb = currentItem.GetComponent<Rigidbody>();

        originalPosition = currentItem.transform.position;
        originalRotation = currentItem.transform.rotation;

        currentItem.transform.SetParent(handPosition);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        currentItemRb.isKinematic = true;

        int index = System.Array.IndexOf(grabbableObjects, currentItem);
        if (index >= 0)
        {
            switch (index)
            {
                case 0: OnGrabItem1(); break;
                case 1: OnGrabItem2(); break;
                case 2: OnGrabItem3(); break;
                case 3: OnGrabItem4(); break;
                case 4: OnGrabItem5(); break;
                case 5: OnGrabItem6(); break;
                case 6: OnGrabItem7(); break;
                case 7: OnGrabItem8(); break;
                case 8: OnGrabItem9(); break;
                case 9: OnGrabItem10(); break;
            }
        }
    }

    void DropItem()
    {
        if (currentItem == null) return;

        currentItem.transform.SetParent(null);
        currentItem.transform.position = originalPosition;
        currentItem.transform.rotation = originalRotation;

        currentItemRb.isKinematic = false;

        currentItem = null;
        currentItemRb = null;

        ScriptA.setItBackToOriginalDissolve();
        ScriptB.StartOrbiting1();
    }

    // Empty functions for each object
    void OnGrabItem1()
    {
        ScriptA.StartDissolver();
        ScriptB.StopOrbiting1();
    }
    void OnGrabItem2() { }
    void OnGrabItem3() { }
    void OnGrabItem4() { }
    void OnGrabItem5() { }
    void OnGrabItem6() { }
    void OnGrabItem7() { }
    void OnGrabItem8() { }
    void OnGrabItem9() { }
    void OnGrabItem10() { }
}
