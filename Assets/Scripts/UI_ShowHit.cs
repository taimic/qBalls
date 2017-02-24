using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_ShowHit : MonoBehaviour {

    private RectTransform ownRect;
    private Image hitMarkerImage;
    private RectTransform hitMarkerRect;
    private Collider2D hitMarkerCollider;

    private Vector2 pos;
    private bool foundBall;

    public ParticleSystem hitParticlesPrefab;
    private AudioSource audioSource;

	void Start () {
        ownRect = GetComponent<RectTransform>();
        hitMarkerImage = GetComponentInChildren<Image>();
        hitMarkerRect = hitMarkerImage.GetComponent<RectTransform>();
        hitMarkerCollider = hitMarkerImage.GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        pos = new Vector2(-1000, -1000);

        TCP_Client.FoundBall += OnFoundBall;

        //StartCoroutine(TestEffects());
    }

    void OnDestroy() {
        TCP_Client.FoundBall -= OnFoundBall;
    }

    void Update() {
        if (foundBall) {
            hitMarkerRect.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 180));
            hitMarkerRect.localPosition = new Vector2(-pos.x * ownRect.rect.width, -pos.y * ownRect.rect.height);
            hitMarkerCollider.enabled = true;
            foundBall = false;
            StartCoroutine(DeactivateColliderDelayed());
            ShowParticles();
            PlaySound();
        }
    }

    IEnumerator DeactivateColliderDelayed() {
        yield return new WaitForSeconds(0.1f);
        hitMarkerCollider.enabled = false;
    }

    private void ShowParticles() {
        ParticleSystem obj = Instantiate(hitParticlesPrefab, transform);
        obj.transform.localPosition = hitMarkerCollider.transform.localPosition + Vector3.back*10;
        obj.transform.localScale = Vector3.one * 0.5f;

        Destroy(obj, obj.main.startLifetime.constant);
    }

    private void PlaySound() {
        audioSource.pitch = UnityEngine.Random.Range(0.65f, 0.67f);
        audioSource.Play();
    }

    private void OnFoundBall(float x, float y) {
        pos = new Vector2(x, y);
        foundBall = true;
    }

    private IEnumerator TestEffects() {
        for (int i = 0; i < 15; i++) {
            yield return new WaitForSeconds(1.5f);
            OnFoundBall(UnityEngine.Random.Range(0.1f, 0.9f), UnityEngine.Random.Range(0.2f, 0.8f));
        }
    }
}