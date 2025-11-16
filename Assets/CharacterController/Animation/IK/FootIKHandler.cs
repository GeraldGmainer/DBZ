using UnityEngine;

public class FootIKHandler : MonoBehaviour {
	private float offsetY = 0.101f;

	private Animator animator;

	private Quaternion leftIKRotation;
	private Quaternion rightIKRotation;
	private Transform leftFoot;
	private Transform rightFoot;
	private Vector3 leftIKPosition;
	private Vector3 rightIKPosition;

	void Awake () {
		animator = GetComponentInParent<Animator>();

		leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
		rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

		leftIKRotation = leftFoot.rotation;
		rightIKRotation = rightFoot.rotation;
	}
	
	void Update () {
		getLeftIKValues();
		getRightIKValues();
	}

	private void getLeftIKValues() {
		RaycastHit leftRaycast;
		Vector3 leftPosition = leftFoot.TransformPoint(Vector3.zero);

		if(Physics.Raycast(leftPosition, -Vector3.up, out leftRaycast, 1)) {
			leftIKPosition = leftRaycast.point;
			leftIKRotation = Quaternion.FromToRotation(transform.up, leftRaycast.normal) *  transform.rotation;
		}
	}

	private void getRightIKValues() {
		RaycastHit rightRaycast;
		Vector3 rightPosition = rightFoot.TransformPoint(Vector3.zero);

		if(Physics.Raycast(rightPosition, -Vector3.up, out rightRaycast, 1)) {
			rightIKPosition = rightRaycast.point;
			rightIKRotation = Quaternion.FromToRotation(transform.up, rightRaycast.normal) *   transform.rotation;
		}
	}

	void OnAnimatorIK() {
		float leftIKWeight = animator.GetFloat(AnimatorHashIDs.leftFootIKWeight);
		float rightIKWeight = animator.GetFloat(AnimatorHashIDs.rightFootIKWeight);

		if(leftIKWeight < 0.9 || rightIKWeight < 0.9) {
			return;
		}

		updateLeftIKRotation(leftIKWeight);
		updateRightIKRotation(rightIKWeight);
		updateLeftIKPosition(leftIKWeight);
		updateRightIKPosition(rightIKWeight);
	}

	private void updateLeftIKPosition(float weight) {
		animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, weight);
		animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftIKPosition + new Vector3(0, offsetY, 0));
	}

	private void updateLeftIKRotation(float weight) {
		animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, weight);
		animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftIKRotation);
	}

	private void updateRightIKPosition(float weight) {
		animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, weight);
		animator.SetIKPosition(AvatarIKGoal.RightFoot, rightIKPosition + new Vector3(0, offsetY, 0));
	}

	private void updateRightIKRotation(float weight) {
		animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, weight);
		animator.SetIKRotation(AvatarIKGoal.RightFoot, rightIKRotation);
	}
}
