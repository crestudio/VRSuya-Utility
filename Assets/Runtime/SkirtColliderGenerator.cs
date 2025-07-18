#if UNITY_EDITOR
using UnityEngine;

using VRC.SDK3.Dynamics.PhysBone.Components;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class SkirtColliderGenerator : MonoBehaviour {

		[Header("상단 원 포지션 (12시, 3시, 6시, 9시)")]
		public Vector3 TopCirclePoint_12 = new Vector3(0, 1, 0.5f);
		public Vector3 TopCirclePoint_3 = new Vector3(0.5f, 1, 0);
		public Vector3 TopCirclePoint_6 = new Vector3(0, 1, -0.5f);
		public Vector3 TopCirclePoint_9 = new Vector3(-0.5f, 1, 0);

		[Header("하단 원 포지션 (12시, 3시, 6시, 9시)")]
		public Vector3 BottomCirclePoint_12 = new Vector3(0, 0, 1f);
		public Vector3 BottomCirclePoint_3 = new Vector3(1f, 0, 0);
		public Vector3 BottomCirclePoint_6 = new Vector3(0, 0, -1f);
		public Vector3 BottomCirclePoint_9 = new Vector3(-1f, 0, 0);

		[Header("곡률")]
		[Range(-2f, 2f)]
		[Tooltip("0 = 선형 보간, 양수 = 바깥쪽으로 볼록, 음수 = 안쪽으로 오목")]
		public float TargetCurvature = 1f;

		[Header("설정")]
		[Range(4, 12)]
		public int ColliderCount = 8;

		[Range(0.01f, 1f)]
		public float TargetRadius = 0.1f;

		[Range(0.01f, 1f)]
		public float TargetHeight = 0.5f;

		public string ColliderNamePrefix = "SkirtCollider";

		private Vector3[] TopCircle;
		private Vector3[] BottomCircle;

		private static string UndoGroupName = "VRSuya SkirtColliderGenerator";
		private static int UndoGroupIndex;

		void Start() {
			TopCircle = new Vector3[] { TopCirclePoint_12, TopCirclePoint_3, TopCirclePoint_6, TopCirclePoint_9 };
			BottomCircle = new Vector3[] { BottomCirclePoint_12, BottomCirclePoint_3, BottomCirclePoint_6, BottomCirclePoint_9 };
			return;
		}

		[ContextMenu("Generate PhysBone Colliders")]
		public void GeneratePhysBoneColliders() {
			ClearExistingColliders();
			TopCircle = new Vector3[] { TopCirclePoint_12, TopCirclePoint_3, TopCirclePoint_6, TopCirclePoint_9 };
			BottomCircle = new Vector3[] { BottomCirclePoint_12, BottomCirclePoint_3, BottomCirclePoint_6, BottomCirclePoint_9 };
			float AngleStep = 360f / ColliderCount;
			for (int Step = 0; Step < ColliderCount; Step++) {
				float NewAngle = Step * AngleStep;
				Vector3 TopPosition = GetInterpolatedPosition(TopCircle, NewAngle);
				Vector3 BottomPosition = GetInterpolatedPosition(BottomCircle, NewAngle);
				Vector3 CenterPosition = (TopPosition + BottomPosition) * 0.5f;

				Vector3 VectorDirection = GetRadialDirection(NewAngle);
				Vector3 LineDirection = (TopPosition - BottomPosition).normalized;

				Vector3 NewPosition = CenterPosition + VectorDirection * TargetRadius;
				Quaternion NewRotation = Quaternion.LookRotation(Vector3.Cross(LineDirection, Vector3.up), LineDirection);

				GameObject NewGameObject = new GameObject($"{ColliderNamePrefix}_{Step + 1}");
				NewGameObject.transform.parent = this.transform;
				NewGameObject.transform.position = NewPosition;
				NewGameObject.transform.rotation = NewRotation;

				VRCPhysBoneCollider NewCollider = NewGameObject.AddComponent<VRCPhysBoneCollider>();
				NewCollider.shapeType = VRC.Dynamics.VRCPhysBoneColliderBase.ShapeType.Capsule;
				NewCollider.radius = TargetRadius;
				NewCollider.height = TargetHeight;
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

			return Vector3.Lerp(TargetCircle[CirclePoint1], TargetCircle[CirclePoint2], NewPoint);
		}

		private float ApplyCurvature(float TargetPoint, float Curvature) {
			if (Mathf.Abs(Curvature) < 0.001f) {
				return TargetPoint;
			}
			if (Curvature > 0) {
				return 1f - Mathf.Pow(1f - TargetPoint, 1f + Curvature);
			} else {
				return Mathf.Pow(TargetPoint, 1f - Curvature);
			}
		}

		private Vector3 GetRadialDirection(float TargetAngle) {
			float NewRadian = TargetAngle * Mathf.Deg2Rad;
			return new Vector3(Mathf.Sin(NewRadian), 0, Mathf.Cos(NewRadian));
		}

		private void ClearExistingColliders() {
			for (int Index = transform.childCount - 1; Index >= 0; Index--) {
				Transform ChildTransform = transform.GetChild(Index);
				if (ChildTransform.name.StartsWith(ColliderNamePrefix)) {
					if (Application.isPlaying) {
						Destroy(ChildTransform.gameObject);
					} else {
						DestroyImmediate(ChildTransform.gameObject);
					}
				}
			}
			return;
		}

		void OnDrawGizmos() {
			if (TopCircle == null || BottomCircle == null) return;

			Gizmos.color = Color.green;
			foreach (Vector3 PointPosition in TopCircle) {
				Gizmos.DrawWireSphere(transform.TransformPoint(PointPosition), 0.05f);
			}

			Gizmos.color = Color.red;
			foreach (Vector3 PointPosition in BottomCircle) {
				Gizmos.DrawWireSphere(transform.TransformPoint(PointPosition), 0.05f);
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
					Vector3 NewPosition = CenterPosition + VectorDirection * TargetRadius;

					Gizmos.color = Color.blue;
					Gizmos.DrawWireSphere(transform.TransformPoint(NewPosition), TargetRadius * 0.5f);
				}
			}
			return;
		}
	}
}
#endif