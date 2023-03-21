using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPool
{
    public BulletController bullet;
    public List<BulletController> inActiveObject;
    public List<BulletController> activeObject;

    public BulletController Spawn(Vector3 position, Transform parent)
    {
        if (this.inActiveObject.Count == 0)
        {
            // Debug.Log("Create Obj, Add to Active");
            BulletController newObj = GameObject.Instantiate(this.bullet, parent);
            newObj.transform.position = position;
            this.activeObject.Add(newObj);
            return newObj;
        }
        else
        {
            // Debug.Log("Add to Active from list Inactive");
            BulletController oldObj = this.inActiveObject[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            this.activeObject.Add(oldObj);
            this.inActiveObject.RemoveAt(0);
            return oldObj;
        }
    }

    public void Release(BulletController obj)
    {
        if (this.activeObject.Contains(obj))
        {
            Debug.Log("Release");
            this.activeObject.Remove(obj);
            this.inActiveObject.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
}
