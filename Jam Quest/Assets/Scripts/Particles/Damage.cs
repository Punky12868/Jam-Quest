using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Damage : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _emission;

    [SerializeField] Volume _volume;
    private MotionBlur _motionBlur;

    [SerializeField] CanvasGroup HurtPanel;
    private void Awake()
    {
        _emission = _particleSystem.emission;
        _emission.enabled = false;

        _volume.profile.TryGet(out _motionBlur);
        _motionBlur.intensity.value = 0f;

        HurtPanel.alpha = 0f;
    }

    public void OnDamage()
    {
        CameraShake.Instance.ShakeCamera(10f, 0.4f);

        _emission.enabled = true;
        _motionBlur.intensity.value = 0.5f;

        HurtPanel.alpha = 1f;

        StartCoroutine(OffDamage());
    }

    IEnumerator OffDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _emission.enabled = false;
        _motionBlur.intensity.value = 0f;
    }

    private void Update()
    {
        HurtPanel.alpha = Mathf.Lerp(HurtPanel.alpha, 0f, Time.deltaTime * 2f);
    }
}
