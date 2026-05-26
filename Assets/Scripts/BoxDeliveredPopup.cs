using UnityEngine;

public class BoxDeliveredPopup : MonoBehaviour {


    [SerializeField] private Transform canvasTransform;


    private void Awake() {
        canvasTransform.LookAt(canvasTransform.position + (Camera.main.transform.forward));

        Destroy(gameObject, 2f);
    }

    private void Update() {
        float speed = .5f;
        canvasTransform.position += Vector3.up * speed * Time.deltaTime;
    }

}