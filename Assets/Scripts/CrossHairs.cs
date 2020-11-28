using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairs : MonoBehaviour {

    public LayerMask targetMask;
    public SpriteRenderer dot;
    Color originalDotColour;

    void Start() {
        Cursor.visible = false;
        originalDotColour = dot.color;
    }

    void Update() {
        transform.Rotate(Vector3.forward * -40 * Time.deltaTime);
    }

    public void DetectTarget(Ray ray) {
        if (Physics.Raycast(ray, 100, targetMask)) {
            dot.color = Color.red; ;
        } else {
            dot.color = originalDotColour;
        }
    }
}
