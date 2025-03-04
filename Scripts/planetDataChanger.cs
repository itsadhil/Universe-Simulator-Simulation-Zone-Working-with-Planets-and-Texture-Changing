using UnityEngine;
using System.Collections.Generic;

public class ObjectController : MonoBehaviour
{
    public List<GameObject> objects; // Assign in Inspector
    public Material newMaterial; // Assign in Inspector
    private int currentIndex = 0;
    private Dictionary<GameObject, ObjectData> objectData = new Dictionary<GameObject, ObjectData>();

    private class ObjectData
    {
        public Vector3 originalSize;
        public float originalMass;
        public bool isDisabled = false;
        public bool materialApplied = false;
        public Rigidbody rb;
        public Renderer renderer;
    }

    void Start()
    {
        foreach (var obj in objects)
        {
            ObjectData data = new ObjectData();
            data.originalSize = obj.transform.localScale;
            data.rb = obj.GetComponent<Rigidbody>();
            data.renderer = obj.GetComponent<Renderer>();
            if (data.rb != null)
            {
                data.originalMass = data.rb.mass;
            }
            objectData[obj] = data;
        }
    }

    private GameObject GetCurrentObject()
    {
        return objects.Count > 0 ? objects[currentIndex] : null;
    }

    public void SelectObject(int index)
    {
        if (index >= 0 && index < objects.Count)
        {
            currentIndex = index;
        }
    }

    public void IncreaseSize()
    {
        var obj = GetCurrentObject();
        if (obj) obj.transform.localScale *= 2f;
    }

    public void DecreaseSize()
    {
        var obj = GetCurrentObject();
        if (obj != null)
        {
            ObjectData data = objectData[obj];
            obj.transform.localScale *= 0.5f;
            if (obj.transform.localScale.magnitude < data.originalSize.magnitude / 8f && newMaterial != null && !data.materialApplied)
            {
                data.renderer.material = newMaterial;
                obj.transform.localScale = data.originalSize * 3f;
                data.materialApplied = true;
            }
        }
    }

    public void DisableObject()
    {
        var obj = GetCurrentObject();
        if (obj != null)
        {
            ObjectData data = objectData[obj];
            if (data.rb != null)
            {
                data.originalMass = data.rb.mass;
                data.rb.mass = 0f;
            }
            obj.SetActive(false);
            data.isDisabled = true;
        }
    }

    public void EnableObject()
    {
        var obj = GetCurrentObject();
        if (obj != null)
        {
            ObjectData data = objectData[obj];
            if (data.isDisabled)
            {
                obj.SetActive(true);
                if (data.rb != null)
                {
                    data.rb.mass = data.originalMass;
                }
                data.isDisabled = false;
            }
        }
    }

    public void IncreaseMass()
    {
        var obj = GetCurrentObject();
        if (obj != null)
        {
            ObjectData data = objectData[obj];
            if (data.rb != null)
            {
                data.rb.mass *= 2f;
                if (data.rb.mass >= data.originalMass * 4f && newMaterial != null && !data.materialApplied)
                {
                    data.renderer.material = newMaterial;
                    obj.transform.localScale *= 3f;
                    data.materialApplied = true;
                }
            }
        }
    }

    public void DecreaseMass()
    {
        var obj = GetCurrentObject();
        if (obj != null)
        {
            ObjectData data = objectData[obj];
            if (data.rb != null)
            {
                data.rb.mass *= 0.5f;
            }
        }
    }

    // Selection functions for up to 9 objects
    public void SelectObject1() { SelectObject(0); }
    public void SelectObject2() { SelectObject(1); }
    public void SelectObject3() { SelectObject(2); }
    public void SelectObject4() { SelectObject(3); }
    public void SelectObject5() { SelectObject(4); }
    public void SelectObject6() { SelectObject(5); }
    public void SelectObject7() { SelectObject(6); }
    public void SelectObject8() { SelectObject(7); }
    public void SelectObject9() { SelectObject(8); }
    public void SelectObject10() { SelectObject(9); }
}
