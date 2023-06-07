using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchProjectile : MonoBehaviour
{
    public GameObject projectilPrefab;

    public void SpwanProjectil()
    {
        GameObject projectil = Instantiate(projectilPrefab, transform.position, projectilPrefab.transform.rotation);
        Vector3 origScale = projectil.transform.localScale;
        projectil.transform.localScale = new Vector3(
                origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
                origScale.y,
                origScale.z
            );
    }
}
