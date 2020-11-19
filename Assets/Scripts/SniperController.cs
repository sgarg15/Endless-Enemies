using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : MonoBehaviour
{

    public LayerMask collisionMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 10.0f, collisionMask);

      /*  for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend) {
                // Change the material of all hit colliders
                // to use a transparent shader.
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }*/

    }
}
