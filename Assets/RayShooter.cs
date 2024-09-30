using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Tidak ada komponen Camera ditemukan pada objek ini.");
        }

        // Mengunci kursor ke tengah layar dan menyembunyikannya (opsional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tombol kiri mouse
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Mengatur titik di tengah layar
        Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
        Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;

        // Visualisasi raycast di Scene View (hanya terlihat di editor)
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null)
            {
                Debug.Log("Target hit: " + hitObject.name);
                target.ReactToHit(); // Pastikan metode ini ada di skrip ReactiveTarget
            }
            else
            {
                StartCoroutine(SphereIndicator(hit.point));
                Debug.Log("Hit non-target: " + hitObject.name);
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = Vector3.one * 0.2f; // Ukuran kecil
        sphere.GetComponent<Renderer>().material.color = Color.red; // Warna merah

        // Nonaktifkan collider agar tidak mengganggu fisika
        Collider collider = sphere.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
