using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private TrailRenderer bulletTrail;
    private Controls controls;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        controls = new Controls();

        controls.Player.Shoot.performed += HandleShoot;

        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleShoot(InputAction.CallbackContext ctx)
    {
        StartCoroutine(MuzzelAnimation(transform, gunParticle));
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity)) {
            return;
        }
        Debug.Log(hit.collider);
        StartCoroutine(TrailAnimation(hit));

    }
    private IEnumerator MuzzelAnimation(Transform transform, ParticleSystem particleSystem)
    {
        ParticleSystem ps = Instantiate(particleSystem, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Destroy(ps);
    }

    private IEnumerator TrailAnimation(RaycastHit hit)
    {
        Debug.Log(hit.point);
        float time = 0;
        TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
        Vector3 startPosition = trail.transform.position;
        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;
    }

}
