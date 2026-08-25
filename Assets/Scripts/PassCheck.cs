using UnityEngine;

public class PassCheck : MonoBehaviour
{
    public float forceMaxSpeed = 30;

    private GameObject mainCamera;
    private Vector3 cameraPosition;
    private void Start()
    {
        if (mainCamera == null) mainCamera = GameObject.Find("Main Camera");
        cameraPosition = mainCamera.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.singleton.AddScore(1);

            // Handheld.Vibrate();

            DestroyPlatforms();
        }
    }

    public void DestroyPlatforms()
    {
        Component[] allPlatformsMesh = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider jointp in allPlatformsMesh)
        {
            jointp.enabled = false;
        }
        Component[] allPlatformsRb = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody joint in allPlatformsRb)
        {
            joint.isKinematic = false;
            //joint.AddForce(new Vector3(Random.Range(-50,50), Random.Range(-50, 50), Random.Range(-50, 50)) * Random.Range(0.1f, forceMaxSpeed), ForceMode.Impulse);

            Vector3 direction = (cameraPosition - joint.transform.position);
            Vector3 forward = Quaternion.Euler(0, -52.5f, 0) * joint.transform.forward * -1;
            float diff = Mathf.Abs(direction.x - forward.x) + Mathf.Abs(direction.z - forward.z);

            if (diff < 8.2)
            {
                bool isLeft = (direction.x - forward.x) < 0;
                if (isLeft) forward = Quaternion.Euler(0, -60, 0) * forward;
                else forward = Quaternion.Euler(0, 60, 0) * forward;
            }

            joint.AddForce(new Vector3(forward.x, 0, forward.z) * Random.Range(10, forceMaxSpeed), ForceMode.Impulse);
        }

        gameObject.transform.SetParent(null);
        Destroy(gameObject, 1);
    }
       
}
