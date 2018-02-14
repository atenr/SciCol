using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fire_rate = 0.2f;

    public Transform gun_barrel;

    private LineRenderer laser;
    private Camera cam;
    private float next_shot;
    private WaitForSeconds shot_duration = new WaitForSeconds(0.05f);

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        cam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0) && Time.time > next_shot)
        {
            next_shot = Time.time + fire_rate;

            StartCoroutine(ShotFired());

            Vector3 origin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Shoot(origin);
        }

	}

    void Shoot(Vector3 origin)
    {
        RaycastHit hit;

        laser.SetPosition(0, gun_barrel.position);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            laser.SetPosition(1, hit.point);

            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            laser.SetPosition(1, origin + (cam.transform.forward * range));
        }
    }

    private IEnumerator ShotFired()
    {
        laser.enabled = true;

        yield return shot_duration;

        laser.enabled = false;
    }

}
