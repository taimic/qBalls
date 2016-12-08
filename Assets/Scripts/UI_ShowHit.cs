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

	void Start () {
        ownRect = GetComponent<RectTransform>();
        hitMarkerImage = GetComponentInChildren<Image>();
        hitMarkerRect = hitMarkerImage.GetComponent<RectTransform>();
        hitMarkerCollider = hitMarkerImage.GetComponent<Collider2D>();

        pos = new Vector2(-1000, -1000);

        TCP_Client.FoundBall += OnFoundBall;

    }

    void OnDestroy() {
        TCP_Client.FoundBall -= OnFoundBall;
    }

    void Update() {
        if (foundBall) {
            hitMarkerRect.localPosition = new Vector2(-pos.x * ownRect.rect.width, -pos.y * ownRect.rect.height);
            hitMarkerCollider.enabled = true;
            foundBall = false;
            StartCoroutine(DeactivateColliderDelayed());
        }
    }

    IEnumerator DeactivateColliderDelayed() {
        yield return new WaitForSeconds(0.1f);
        hitMarkerCollider.enabled = false;
    }

    private void OnFoundBall(float x, float y) {
        pos = new Vector2(x, y);
        foundBall = true;
    }
}
