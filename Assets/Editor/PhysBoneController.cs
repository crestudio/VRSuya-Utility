﻿#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using VRC.SDK3.Dynamics.PhysBone.Components;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class PhysBoneController : EditorWindow {

		private static string UndoGroupName = "VRSuya PhysBoneController";
		private static int UndoGroupIndex;

		private static readonly Dictionary<HumanBodyBones, HumanBodyBones> BoneColliderPair = new Dictionary<HumanBodyBones, HumanBodyBones> {
			{ HumanBodyBones.Hips, HumanBodyBones.Spine },
			{ HumanBodyBones.Spine, HumanBodyBones.Chest },
			{ HumanBodyBones.Chest, HumanBodyBones.Neck },
			{ HumanBodyBones.Head, HumanBodyBones.LastBone },
			{ HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm },
			{ HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand },
			{ HumanBodyBones.LeftHand, HumanBodyBones.LastBone },
			{ HumanBodyBones.LeftIndexDistal, HumanBodyBones.LastBone },
			{ HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm },
			{ HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand },
			{ HumanBodyBones.RightHand, HumanBodyBones.LastBone },
			{ HumanBodyBones.RightIndexDistal, HumanBodyBones.LastBone },
			{ HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg },
			{ HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg }
		};

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.0으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/1.0", priority = 1000)]
		public static void ChangePhysBoneVersionTo1_0() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.0");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.1으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/1.1", priority = 1000)]
		public static void ChangePhysBoneVersionTo1_1() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.1");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 버전을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/Debug Version", priority = 1100)]
		public static void DebugLogPhysBoneComponets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Current Version : " + TargetPhysBone.version);
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 닫습니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/FoldOut/Closed", priority = 1001)]
		public static void ClosePhysBoneFoldOut() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				bool IsDirty = false;
				Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
				if (TargetPhysBone.foldout_collision) { TargetPhysBone.foldout_collision = false; IsDirty = true; }
				if (TargetPhysBone.foldout_forces) { TargetPhysBone.foldout_forces = false; IsDirty = true; }
				if (TargetPhysBone.foldout_gizmos) { TargetPhysBone.foldout_gizmos = false; IsDirty = true; }
				if (TargetPhysBone.foldout_grabpose) { TargetPhysBone.foldout_grabpose = false; IsDirty = true; }
				if (TargetPhysBone.foldout_limits) { TargetPhysBone.foldout_limits = false; IsDirty = true; }
				if (TargetPhysBone.foldout_options) { TargetPhysBone.foldout_options = false; IsDirty = true; }
				if (TargetPhysBone.foldout_stretchsquish) { TargetPhysBone.foldout_stretchsquish = false; IsDirty = true; }
				if (TargetPhysBone.foldout_transforms) { TargetPhysBone.foldout_transforms = false; IsDirty = true; }
				if (IsDirty) EditorUtility.SetDirty(TargetPhysBone);
				Undo.CollapseUndoOperations(UndoGroupIndex);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Closed");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 엽니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/FoldOut/Opened", priority = 1000)]
		public static void OpenPhysBoneFoldOut() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				bool IsDirty = false;
				Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
				if (!TargetPhysBone.foldout_collision) { TargetPhysBone.foldout_collision = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_forces) { TargetPhysBone.foldout_forces = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_gizmos) { TargetPhysBone.foldout_gizmos = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_grabpose) { TargetPhysBone.foldout_grabpose = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_limits) { TargetPhysBone.foldout_limits = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_options) { TargetPhysBone.foldout_options = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_stretchsquish) { TargetPhysBone.foldout_stretchsquish = true; IsDirty = true; }
				if (!TargetPhysBone.foldout_transforms) { TargetPhysBone.foldout_transforms = true; IsDirty = true; }
				if (IsDirty) EditorUtility.SetDirty(TargetPhysBone);
				Undo.CollapseUndoOperations(UndoGroupIndex);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Opened");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 숨깁니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Gizmo/Hide", priority = 1001)]
		public static void HidePhysBoneGizmo() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.showGizmos) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.showGizmos = false;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Hidden");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 보이게 합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Gizmo/Show", priority = 1000)]
		public static void ShowPhysBoneGizmo() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!TargetPhysBone.showGizmos) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.showGizmos = true;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Show");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 All Motion으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Immobile/All Motion", priority = 1000)]
		public static void ChangePhysBoneImmobileToAllMotion() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to All Motion");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 World으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Immobile/World", priority = 1000)]
		public static void ChangePhysBoneImmobileToWorld() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to World");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/True", priority = 1000)]
		public static void ChangePhysBoneAnimatedToTrue() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!TargetPhysBone.isAnimated) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.isAnimated = true;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to True");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/False", priority = 1001)]
		public static void ChangePhysBoneAnimatedToFalse() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.isAnimated = false;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to False");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/Debug Animated", priority = 1100)]
		public static void DebugLogPhysBoneAnimateds() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Is Animated : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Is Animated : False");
				}
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/True", priority = 1000)]
		public static void ChangePhysBoneResetToTrue() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!TargetPhysBone.resetWhenDisabled) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.resetWhenDisabled = true;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to True");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/False", priority = 1001)]
		public static void ChangePhysBoneResetToFalse() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.resetWhenDisabled = false;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to False");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/Debug Reset", priority = 1100)]
		public static void DebugLogPhysBoneResets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Reset When Disabled : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Reset When Disabled : False");
				}
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Colliders 어레이를 제거합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Quest/Remove Colliders", priority = 1000)]
		public static void EmptyPhysBoneColliders() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.colliders.Count > 0) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.colliders = new List<VRC.Dynamics.VRCPhysBoneColliderBase> { };
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Empty All PhysBone Colliders List");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Parameter를 비웁니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Quest/Remove Parameter", priority = 1000)]
		public static void EmptyPhysBoneParameter() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!string.IsNullOrEmpty(TargetPhysBone.parameter)) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.parameter = "";
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log("[VRSuya] Empty All PhysBone Parameter");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 리스트를 가져옵니다.</summary>
		/// <returns>Scene에 존재하는 모든 PhysBone 컴포넌트 리스트</returns>
		private static List<VRCPhysBone> GetPhysBoneComponents() {
			return SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(gameObject => gameObject.GetComponentsInChildren<VRCPhysBone>(true)).ToList();
		}

		[MenuItem("Tools/VRSuya/Utility/PhysBone/Collider/Create Humanoid Collider", priority = 1000)]
		public static void CreateHumanoidCollider() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			if (AvatarInstance.GetVRCAvatarDescriptor()) {
				GameObject AvatarObject = AvatarInstance.GetVRCAvatarDescriptor().gameObject;
				Animator AvatarAnimator = AvatarObject.GetComponent<Animator>();
				if (AvatarAnimator) {
					UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
					foreach (var KeyPair in BoneColliderPair) {
						GameObject TargetGameObject = AvatarAnimator.GetBoneTransform(KeyPair.Key).gameObject;
						VRCPhysBoneCollider TargetPhysBoneCollider = GetOrCreateComponent<VRCPhysBoneCollider>(TargetGameObject);
						if (TargetPhysBoneCollider.shapeType != VRC.Dynamics.VRCPhysBoneColliderBase.ShapeType.Capsule) {
							Undo.RegisterCreatedObjectUndo(TargetPhysBoneCollider, UndoGroupName);
							TargetPhysBoneCollider.shapeType = VRC.Dynamics.VRCPhysBoneColliderBase.ShapeType.Capsule;
							EditorUtility.SetDirty(TargetPhysBoneCollider);
						}
						if (KeyPair.Value != HumanBodyBones.LastBone) {
							float Radius = TargetPhysBoneCollider.radius;
							float Distance = Vector3.Distance(AvatarAnimator.GetBoneTransform(KeyPair.Key).position, AvatarAnimator.GetBoneTransform(KeyPair.Value).position) + (Radius * 2f);
							if (KeyPair.Value == HumanBodyBones.Neck) {
								Vector3 MidPoint = (AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftShoulder).position + AvatarAnimator.GetBoneTransform(HumanBodyBones.RightShoulder).position) / 2f;
								Distance = Vector3.Distance(AvatarAnimator.GetBoneTransform(KeyPair.Key).position, MidPoint) + (Radius * 1.25f);
							}
							Vector3 Position = new Vector3(0f, (Distance / 2) - Radius, 0f);
							Undo.RegisterCreatedObjectUndo(TargetPhysBoneCollider, UndoGroupName);
							TargetPhysBoneCollider.height = Distance;
							TargetPhysBoneCollider.position = Position;
							EditorUtility.SetDirty(TargetPhysBoneCollider);
						} else {
							if (KeyPair.Key == HumanBodyBones.LeftHand || KeyPair.Key == HumanBodyBones.RightHand) {
								Transform ParentTransform = AvatarAnimator.GetBoneTransform(KeyPair.Key);
								Transform ChildTransform = (KeyPair.Key == HumanBodyBones.LeftHand) ? AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftMiddleProximal) : AvatarAnimator.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
								float Radius = TargetPhysBoneCollider.radius;
								float Distance = Vector3.Distance(ParentTransform.position, ChildTransform.position) + Radius;
								Vector3 Position = new Vector3(0f, (Distance / 2) - Radius, 0f);
								Undo.RegisterCreatedObjectUndo(TargetPhysBoneCollider, UndoGroupName);
								TargetPhysBoneCollider.height = Distance;
								TargetPhysBoneCollider.position = Position;
								EditorUtility.SetDirty(TargetPhysBoneCollider);
							} else if (KeyPair.Key == HumanBodyBones.LeftIndexDistal || KeyPair.Key == HumanBodyBones.RightIndexDistal) {
								Transform ParentTransform = AvatarAnimator.GetBoneTransform(KeyPair.Key);
								Transform ChildTransform = ParentTransform.GetChild(0);
								float Radius = TargetPhysBoneCollider.radius;
								float Distance = Vector3.Distance(ParentTransform.position, ChildTransform.position) + (Radius * 2f);
								Vector3 Position = new Vector3(0f, (Distance / 2) - Radius, 0f);
								Undo.RegisterCreatedObjectUndo(TargetPhysBoneCollider, UndoGroupName);
								TargetPhysBoneCollider.height = Distance;
								TargetPhysBoneCollider.position = Position;
								EditorUtility.SetDirty(TargetPhysBoneCollider);
							}
						}
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					Debug.Log("[VRSuya] Created Humanoid PhysBone Colliders");
				}
			}
			return;
		}
	}
}
#endif