using UnityEngine;
using System.Collections;

public class Simple2DFollowCamera : MonoBehaviour {

    public float speed = 15f;
    public float minDistanceX;
    public float minDistanceY;
    
    public GameObject target;
    public Vector3 offset;

    private Vector3 targetPos;

    // Use this for initialization
    void Start() {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (target) {
            Vector3 posNoZ = transform.position + offset;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            bool noXVal = Mathf.Abs(targetDirection.x) < minDistanceX;
            bool noYVal = Mathf.Abs( targetDirection.y) < minDistanceY;
            float interpVelocity = targetDirection.magnitude * speed;
            Vector3 applyMask = new Vector3(noXVal ? 0 : 1, noYVal ? 0 : 1, 1);

            applyMask.Scale(targetDirection.normalized * interpVelocity * Time.deltaTime);
            targetPos = (transform.position) + applyMask;

            float lerpAmount = 0.25f;
            
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpAmount);

        }
    }
}
