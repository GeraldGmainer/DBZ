using UnityEngine;
using System.Collections;

public class DrawSwordHandler : Photon.MonoBehaviour {
    private const float DRAWING_TIME = 0.6f;

    private DrawSwordTimer drawSwordTimer;
    private DrawSwordAttacher drawSwordAttacher;
    private DrawSwordAnimator drawSwordAnimator;
    private SwordHitBoxController swordHitBoxController;

    public bool isDrawing { get; private set; }

    void Awake() {
        drawSwordTimer = GetComponent<DrawSwordTimer>();
        drawSwordAttacher = GetComponent<DrawSwordAttacher>();
        drawSwordAnimator = GetComponent<DrawSwordAnimator>();
        swordHitBoxController = GetComponent<SwordHitBoxController>();
    }

    void Start() {
        drawSwordAttacher.init();
        swordHitBoxController.init();
    }

    public void updateAttachment() {
        if (drawSwordTimer.isCooldown) {
            return;
        }
        drawSwordTimer.cooldown();
        updateAnimator();
        isDrawing = true;
        StartCoroutine("resetDrawingTime");
    }

    public bool isAttachedToSheath() {
        return drawSwordAttacher.isAttachedToSheath();
    }

    public bool isAttachedToGrip() {
        return !drawSwordAttacher.isAttachedToSheath();
    }

    private void updateAnimator() {
        if (isAttachedToSheath()) {
            drawSwordAnimator.update(1);
        }
        else {
            drawSwordAnimator.update(2);
        }
    }

    IEnumerator resetDrawingTime() {
        yield return new WaitForSeconds(DRAWING_TIME);
        isDrawing = false;
        drawSwordAnimator.update(0);
    }
}
