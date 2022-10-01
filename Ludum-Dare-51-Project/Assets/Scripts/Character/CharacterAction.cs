using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private int damage;
    
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private float cameraShakeIntensity;
    [SerializeField] private float shakeTimer;
    private Controls controls;

    private void Start()
    {
        controls = new Controls();

        controls.Player.Shoot.performed += HandleShoot;

        controls.Enable();
    }


    private void HandleShoot(InputAction.CallbackContext ctx)
    {
        StartCoroutine(MuzzelAnimation(transform, gunParticle));
        if (camera)
        {
            StartCoroutine(ShakeCamera(shakeTimer));
        }
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity)) {
            return;
        }
        StartCoroutine(TrailAnimation(hit));
        Health health = hit.collider.gameObject.GetComponent<Health>();
        if (!health)
        {
            return;
        }
        health.DealDamage(damage);
    }
    private IEnumerator MuzzelAnimation(Transform transform, ParticleSystem particleSystem)
    {
        ParticleSystem ps = Instantiate(particleSystem, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Destroy(ps);
    }

    private IEnumerator TrailAnimation(RaycastHit hit)
    {
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

    private IEnumerator ShakeCamera(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(shakeTimer);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
