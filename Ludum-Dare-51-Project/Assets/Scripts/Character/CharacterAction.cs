using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private int damage;
    
    private CinemachineBasicMultiChannelPerlin cameraMultiChannel;
    [SerializeField] private float cameraShakeIntensity;
    [SerializeField] private float shakeTimer;
    private AudioSource audioSource;
    private Controls controls;
    private bool isPaused = false;

    public static event Action GamePause;

    private void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("VirtualCamera");

        if (camera)
        {
             CinemachineVirtualCamera cvm = camera.GetComponent<CinemachineVirtualCamera>();
             if (cvm)
             {
                cameraMultiChannel = cvm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
             }
        }

        audioSource = GetComponent<AudioSource>();

        controls = new Controls();

        controls.Player.Shoot.performed += HandleShoot;

        controls.Player.Pause.performed += HandlePause;

        controls.Enable();

    }

    private void HandlePause(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            controls.Player.Shoot.performed -= HandleShoot;
        }
        else
        {
            controls.Player.Shoot.performed += HandleShoot;
        }
        GamePause?.Invoke();
    }

    private void HandleShoot(InputAction.CallbackContext ctx)
    {
        StartCoroutine(MuzzelAnimation(transform, gunParticle));
        audioSource.Play();
        if (cameraMultiChannel)
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
        cameraMultiChannel.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(shakeTimer);
        cameraMultiChannel.m_AmplitudeGain = 0;
    }

    private void OnDestroy()
    {
        controls.Player.Shoot.performed -= HandleShoot;
        controls.Player.Pause.performed -= HandlePause;
    }
}
