#if UNITY_EDITOR
using System;
using System.Linq;

using UnityEditor;
using UnityEngine;

using VRC.SDK3.Dynamics.PhysBone.Components;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	[AddComponentMenu("VRSuya/VRSuya SkirtColliderGenerator")]
	public class SkirtColliderGenerator : MonoBehaviour {

		public Vector3 TopCirclePoint_12 = new Vector3(0f, 0.976f, 0.0865f);
		public Vector3 TopCirclePoint_3 = new Vector3(0.069f, 0.98f, 0.028f);
		public Vector3 TopCirclePoint_6 = new Vector3(0f, 0.985f, -0.0265f);
		public Vector3 TopCirclePoint_9 = new Vector3(-0.069f, 0.98f, 0.028f);

		public Vector3 BottomCirclePoint_12 = new Vector3(0f, 0.695f, 0.115f);
		public Vector3 BottomCirclePoint_3 = new Vector3(0.195f, 0.71f, -0.009f);
		public Vector3 BottomCirclePoint_6 = new Vector3(0f, 0.705f, -0.173f);
		public Vector3 BottomCirclePoint_9 = new Vector3(-0.195f, 0.71f, -0.009f);

		[Range(-0.1f, 0.1f)]
		public float FrontCurvature = 0.03f;

		[Range(-0.1f, 0.1f)]
		public float TopFrontCurvature = -0.01f;

		[Range(-0.1f, 0.1f)]
		public float BottomFrontCurvature = 0.015f;

		[Range(-0.1f, 0.1f)]
		public float BackCurvature = 0.003f;

		[Range(-0.1f, 0.1f)]
		public float TopBackCurvature = 0f;

		[Range(-0.1f, 0.1f)]
		public float BottomBackCurvature = 0.041f;

		[Range(0, 5)]
		public int SampleCount = 2;

		[Range(-0.1f, 0.1f)]
		public float TargetOffset = 0f;

		[Range(-0.1f, 0.1f)]
		public float TargetShiftOffset = 0f;

		[Range(0.01f, 1f)]
		public float TargetRadius = 0.09f;

		[Range(0.01f, 1f)]
		public float TargetHeight = 0.5f;

		[Range(0.001f, 0.1f)]
		public float GizmoSize = 0.005f;

		public Transform HipsTransform;
		public Transform LeftLegTransform;
		public Transform RightLegTransform;
		public string ColliderNamePrefix = "SkirtCollider";

		private int ColliderCount;
		private Vector3[] TopCircle;
		private Vector3[] BottomCircle;

		private static string UndoGroupName = "VRSuya SkirtColliderGenerator";
		private static int UndoGroupIndex;

		void Start() {
			GetHumanoidTransform();
			UpdatePropertys();
			return;
		}

		void OnValidate() {
			UpdatePropertys();
			return;
		}

		private void UpdatePropertys() {
			TopCircle = new Vector3[] { TopCirclePoint_12, TopCirclePoint_3, TopCirclePoint_6, TopCirclePoint_9 };
			BottomCircle = new Vector3[] { BottomCirclePoint_12, BottomCirclePoint_3, BottomCirclePoint_6, BottomCirclePoint_9 };
			ColliderCount = 4 + SampleCount * 4;
			return;
		}

		private void GetHumanoidTransform() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			GameObject TargetGameObject = this.gameObject;
			GameObject AvatarGameObject = AvatarInstance.GetAvatarGameObject(TargetGameObject);
			if (AvatarGameObject) {
				Animator AvatarAnimator = AvatarGameObject.GetComponent<Animator>();
				if (AvatarAnimator) {
					HipsTransform = AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
					LeftLegTransform = AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
					RightLegTransform = AvatarAnimator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
				}
			}
			return;
		}

		public void GeneratePhysBoneColliders() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			TopCircle = new Vector3[] { TopCirclePoint_12, TopCirclePoint_3, TopCirclePoint_6, TopCirclePoint_9 };
			BottomCircle = new Vector3[] { BottomCirclePoint_12, BottomCirclePoint_3, BottomCirclePoint_6, BottomCirclePoint_9 };
			VRSuya.Core.Unity UnityInstance = new VRSuya.Core.Unity();
			Transform[] ChildTransforms = this.gameObject.GetComponentsInChildren<Transform>();
			float AngleStep = 360f / ColliderCount;
			for (int Step = 0; Step < ColliderCount; Step++) {
				float NewAngle = Step * AngleStep;
				Vector3 TopPosition = GetInterpolatedPosition(TopCircle, NewAngle);
				Vector3 BottomPosition = GetInterpolatedPosition(BottomCircle, NewAngle);
				Vector3 CenterPosition = (TopPosition + BottomPosition) * 0.5f;

				Vector3 VectorDirection = GetRadialDirection(NewAngle);
				Vector3 LineDirection = (TopPosition - BottomPosition).normalized;

				Vector3 NewPosition = CenterPosition + VectorDirection * TargetRadius + LineDirection * TargetShiftOffset;
				Quaternion NewRotation = Quaternion.LookRotation(Vector3.Cross(LineDirection, Vector3.up), LineDirection);

				Transform ReferenceTransform = GetReferenceTransform(NewAngle);
				Vector3 LocalPosition = NewPosition;
				Quaternion LocalRotation = NewRotation;
				if (ReferenceTransform != null) {
					LocalPosition = ReferenceTransform.InverseTransformPoint(NewPosition);
					LocalRotation = Quaternion.Inverse(ReferenceTransform.rotation) * NewRotation;
				}

				GameObject NewGameObject;
				string NewGameObjectName = $"{ColliderNamePrefix}_{Step + 1}";
				if (Array.Exists(ChildTransforms, Item => Item.name == NewGameObjectName)) {
					NewGameObject = ChildTransforms.First(Child => Child.name == NewGameObjectName).gameObject;
				} else {
					NewGameObject = new GameObject(NewGameObjectName);
					NewGameObject.transform.parent = this.transform;
					Undo.RegisterCreatedObjectUndo(NewGameObject, UndoGroupName);
				}

				VRCPhysBoneCollider NewCollider = GetOrCreateComponent<VRCPhysBoneCollider>(NewGameObject);
				Undo.RecordObject(NewCollider, UndoGroupName);
				NewCollider.rootTransform = ReferenceTransform;
				NewCollider.shapeType = VRC.Dynamics.VRCPhysBoneColliderBase.ShapeType.Capsule;
				NewCollider.radius = TargetRadius;
				NewCollider.height = TargetHeight;
				NewCollider.position = LocalPosition;
				NewCollider.rotation = LocalRotation;
				NewCollider.insideBounds = true;
				EditorUtility.SetDirty(NewCollider);
				Undo.CollapseUndoOperations(UndoGroupIndex);
			}
			Debug.Log($"[VRSuya] Generated {ColliderCount} PhysBone Colliders");
			return;
		}

		private Vector3 GetInterpolatedPosition(Vector3[] TargetCircle, float TargetAngle) {
			TargetAngle = TargetAngle % 360f;
			if (TargetAngle < 0) TargetAngle += 360f;

			int CirclePoint1, CirclePoint2;
			float NewPoint;

			if (TargetAngle <= 90f) {
				CirclePoint1 = 0; CirclePoint2 = 1;
				NewPoint = TargetAngle / 90f;
			} else if (TargetAngle <= 180f) {
				CirclePoint1 = 1; CirclePoint2 = 2;
				NewPoint = (TargetAngle - 90f) / 90f;
			} else if (TargetAngle <= 270f) {
				CirclePoint1 = 2; CirclePoint2 = 3;
				NewPoint = (TargetAngle - 180f) / 90f;
			} else {
				CirclePoint1 = 3; CirclePoint2 = 0;
				NewPoint = (TargetAngle - 270f) / 90f;
			}

			Vector3 BasePoint = Vector3.Lerp(TargetCircle[CirclePoint1], TargetCircle[CirclePoint2], NewPoint);
			float TargetCurvature = (TargetAngle <= 270f && TargetAngle > 90f) ? BackCurvature : FrontCurvature;
			float FullDistance = CalculateCurvatureDistance(NewPoint, TargetCurvature);
			float TargetSubCurvature = 0.0f;
			if (TargetCircle == TopCircle) {
				TargetSubCurvature = (TargetAngle <= 270f && TargetAngle > 90f) ? TopBackCurvature : TopFrontCurvature;
			} else {
				TargetSubCurvature = (TargetAngle <= 270f && TargetAngle > 90f) ? BottomBackCurvature : BottomFrontCurvature;
			}
			float SubDistance = CalculateCurvatureDistance(NewPoint, TargetSubCurvature);
			Vector3 RadialDirection = GetRadialDirection(TargetAngle);
			return BasePoint + RadialDirection * TargetOffset + RadialDirection * FullDistance + RadialDirection * SubDistance;
		}

		private float CalculateCurvatureDistance(float TargetPoint, float Curvature) {
			if (Mathf.Abs(Curvature) < 0.001f) {
				return 0f;
			}
			float NormalizedDistance = 4f * TargetPoint * (1f - TargetPoint);
			return Curvature * NormalizedDistance;
		}

		private Vector3 GetRadialDirection(float TargetAngle) {
			float NewRadian = TargetAngle * Mathf.Deg2Rad;
			return new Vector3(Mathf.Sin(NewRadian), 0, Mathf.Cos(NewRadian));
		}

		private Transform GetReferenceTransform(float TargetAngle) {
			TargetAngle = TargetAngle % 360f;
			if (TargetAngle < 0) TargetAngle += 360f;
			if (TargetAngle == 0f || TargetAngle == 180f) {
				return HipsTransform;
			} else if (TargetAngle > 0f && TargetAngle < 180f) {
				return RightLegTransform;
			} else if (TargetAngle > 180f && TargetAngle < 360f) {
				return LeftLegTransform;
			}
			return null;
		}

		void OnDrawGizmos() {
			if (TopCircle == null || BottomCircle == null) return;

			Gizmos.color = Color.green;
			foreach (Vector3 PointPosition in TopCircle) {
				Gizmos.DrawWireSphere(transform.TransformPoint(PointPosition), GizmoSize);
			}

			Gizmos.color = Color.red;
			foreach (Vector3 PointPosition in BottomCircle) {
				Gizmos.DrawWireSphere(transform.TransformPoint(PointPosition), GizmoSize);
			}

			if (ColliderCount > 0) {
				float AngleStep = 360f / ColliderCount;
				for (int Step = 0; Step < ColliderCount; Step++) {
					float TargetAngle = Step * AngleStep;
					Vector3 TopPosition = GetInterpolatedPosition(TopCircle, TargetAngle);
					Vector3 BottomPosition = GetInterpolatedPosition(BottomCircle, TargetAngle);

					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(transform.TransformPoint(TopPosition), transform.TransformPoint(BottomPosition));

					Vector3 CenterPosition = (TopPosition + BottomPosition) * 0.5f;
					Vector3 VectorDirection = GetRadialDirection(TargetAngle);
					Vector3 LineDirection = (TopPosition - BottomPosition).normalized;
					Vector3 NewPosition = CenterPosition + VectorDirection * TargetRadius + LineDirection * TargetShiftOffset;

					Gizmos.color = Color.blue;
					Gizmos.DrawWireSphere(transform.TransformPoint(NewPosition), TargetRadius);
				}
			}
			return;
		}
	}
}
#endif