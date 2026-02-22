#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using VRC.Dynamics;
using VRC.SDK3.Dynamics.PhysBone.Components;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class PhysBoneController : EditorWindow {

		static string UndoGroupName = "VRSuya PhysBoneController";
		static int UndoGroupIndex;

		static readonly Dictionary<HumanBodyBones, HumanBodyBones> BoneColliderPair = new Dictionary<HumanBodyBones, HumanBodyBones> {
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
		static readonly Dictionary<string, HumanBodyBones[]> PhysBoneColliderPair = new Dictionary<string, HumanBodyBones[]> {
			{ "Ear", new HumanBodyBones[0] },
			{ "Hair", new HumanBodyBones[] {
				HumanBodyBones.Head,
				HumanBodyBones.LeftLowerArm, HumanBodyBones.RightLowerArm,
				HumanBodyBones.LeftUpperArm, HumanBodyBones.RightUpperArm,
				HumanBodyBones.Hips, HumanBodyBones.Spine, HumanBodyBones.Chest } },
			{ "FrontHair", new HumanBodyBones[] { 
				HumanBodyBones.Head, 
				HumanBodyBones.LeftLowerArm, HumanBodyBones.RightLowerArm,
				HumanBodyBones.LeftUpperArm, HumanBodyBones.RightUpperArm } },
			{ "BackHair", new HumanBodyBones[] {
				HumanBodyBones.Head,
				HumanBodyBones.LeftLowerArm, HumanBodyBones.RightLowerArm,
				HumanBodyBones.LeftUpperArm, HumanBodyBones.RightUpperArm,
				HumanBodyBones.Hips, HumanBodyBones.Spine, HumanBodyBones.Chest } },
			{ "Cheek", new HumanBodyBones[0] },
			{ "Breast", new HumanBodyBones[0] },
			{ "Breast_L", new HumanBodyBones[0] },
			{ "Breast_R", new HumanBodyBones[0] },
			{ "ReverseBreast", new HumanBodyBones[0] },
			{ "Nipple", new HumanBodyBones[0] },
			{ "Skirt", new HumanBodyBones[] { 
				HumanBodyBones.Hips,
				HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg } },
			{ "Butt", new HumanBodyBones[0] },
			{ "Tail", new HumanBodyBones[] {
				HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg } },
		};

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.0으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/1.0", priority = 1000)]
		static void ChangePhysBoneVersionTo1_0() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Version to 1.0");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.1으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/1.1", priority = 1000)]
		static void ChangePhysBoneVersionTo1_1() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Version to 1.1");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 버전을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Version/Debug Version", priority = 1100)]
		static void DebugLogPhysBoneComponets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				Debug.Log($"[VRSuya] PhysBone Parent GameObject Name : {TargetPhysBone.name} / Current Version : {TargetPhysBone.version}");
			}
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 닫습니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/FoldOut/Closed", priority = 1001)]
		static void ClosePhysBoneFoldOut() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone FoldOut to Closed");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 엽니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/FoldOut/Opened", priority = 1000)]
		static void OpenPhysBoneFoldOut() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone FoldOut to Opened");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 숨깁니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Gizmo/Hide", priority = 1001)]
		static void HidePhysBoneGizmo() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Gizmo to Hidden");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 보이게 합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Gizmo/Show", priority = 1000)]
		static void ShowPhysBoneGizmo() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Gizmo to Show");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 All Motion으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Immobile/All Motion", priority = 1000)]
		static void ChangePhysBoneImmobileToAllMotion() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Immobile to All Motion");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 World으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Immobile/World", priority = 1000)]
		static void ChangePhysBoneImmobileToWorld() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Immobile to World");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/True", priority = 1000)]
		static void ChangePhysBoneAnimatedToTrue() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Animated to True");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/False", priority = 1001)]
		static void ChangePhysBoneAnimatedToFalse() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Animated to False");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Animated/Debug Animated", priority = 1100)]
		static void DebugLogPhysBoneAnimateds() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated) {
					Debug.LogWarning($"[VRSuya] PhysBone Parent GameObject Name : {TargetPhysBone.name} / Is Animated : True");
				} else {
					Debug.Log($"[VRSuya] PhysBone Parent GameObject Name : {TargetPhysBone.name} / Is Animated : False");
				}
			}
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/True", priority = 1000)]
		static void ChangePhysBoneResetToTrue() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Reset to True");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/False", priority = 1001)]
		static void ChangePhysBoneResetToFalse() {
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
			Debug.Log($"[VRSuya] Changed All PhysBone Reset to False");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Reset/Debug Reset", priority = 1100)]
		static void DebugLogPhysBoneResets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled) {
					Debug.LogWarning($"[VRSuya] PhysBone Parent GameObject Name : {TargetPhysBone.name} / Reset When Disabled : True");
				} else {
					Debug.Log($"[VRSuya] PhysBone Parent GameObject Name : {TargetPhysBone.name} / Reset When Disabled : False");
				}
			}
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Colliders 어레이를 제거합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Quest/Remove Colliders", priority = 1000)]
		static void EmptyPhysBoneColliders() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.colliders.Count > 0) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.colliders = new List<VRCPhysBoneColliderBase> { };
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log($"[VRSuya] Empty All PhysBone Colliders List");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Parameter를 비웁니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PhysBone/Quest/Remove Parameter", priority = 1000)]
		static void EmptyPhysBoneParameter() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!string.IsNullOrEmpty(TargetPhysBone.parameter)) {
					Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
					TargetPhysBone.parameter = string.Empty;
					EditorUtility.SetDirty(TargetPhysBone);
					Undo.CollapseUndoOperations(UndoGroupIndex);
				}
			}
			Debug.Log($"[VRSuya] Empty All PhysBone Parameter");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 리스트를 가져옵니다.</summary>
		/// <returns>Scene에 존재하는 모든 PhysBone 컴포넌트 리스트</returns>
		static List<VRCPhysBone> GetPhysBoneComponents() {
			return SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(gameObject => gameObject.GetComponentsInChildren<VRCPhysBone>(true)).ToList();
		}

		/// <summary>Scene에 존재하는 모든 PhysBone Collider의 리스트를 가져옵니다.</summary>
		/// <returns>Scene에 존재하는 모든 PhysBone Collider 컴포넌트 리스트</returns>
		static List<VRCPhysBoneColliderBase> GetPhysBoneColliderComponents() {
			return SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(gameObject => gameObject.GetComponentsInChildren<VRCPhysBoneColliderBase>(true)).ToList();
		}

		[MenuItem("Tools/VRSuya/Utility/PhysBone/Collider/Adjust Humanoid Collider", priority = 1000)]
		static void AdjustHumanoidCollider() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			if (AvatarInstance.GetVRCAvatarDescriptor()) {
				GameObject AvatarObject = AvatarInstance.GetVRCAvatarDescriptor().gameObject;
				Animator AvatarAnimator = AvatarObject.GetComponent<Animator>();
				if (AvatarAnimator) {
					UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
					foreach (var KeyPair in BoneColliderPair) {
						GameObject TargetGameObject = AvatarAnimator.GetBoneTransform(KeyPair.Key).gameObject;
						VRCPhysBoneCollider TargetPhysBoneCollider = GetOrCreateComponent<VRCPhysBoneCollider>(TargetGameObject);
						if (TargetPhysBoneCollider.shapeType != VRCPhysBoneColliderBase.ShapeType.Capsule) {
							Undo.RegisterCreatedObjectUndo(TargetPhysBoneCollider, UndoGroupName);
							TargetPhysBoneCollider.shapeType = VRCPhysBoneColliderBase.ShapeType.Capsule;
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
					Debug.Log($"[VRSuya] Created Humanoid PhysBone Colliders");
				}
			}
		}

		[MenuItem("Tools/VRSuya/Utility/PhysBone/Collider/Assign Humanoid Collider", priority = 1000)]
		static void AssignHumanoidCollider() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			if (AvatarInstance.GetVRCAvatarDescriptor()) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
				GameObject AvatarObject = AvatarInstance.GetVRCAvatarDescriptor().gameObject;
				Animator AvatarAnimator = AvatarObject.GetComponent<Animator>();
				List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
				List<VRCPhysBoneColliderBase> PhysBoneColliderComponets = GetPhysBoneColliderComponents();
				foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
					var KeyPair = PhysBoneColliderPair.FirstOrDefault(Item => Item.Key == TargetPhysBone.name);
					if (!KeyPair.Equals(default(KeyValuePair<string, HumanBodyBones[]>))) {
						List<VRCPhysBoneColliderBase> OriginalColliders = TargetPhysBone.colliders;
						List<VRCPhysBoneColliderBase> NewColliders = new List<VRCPhysBoneColliderBase>();
						foreach (HumanBodyBones TargetHumanBodyBone in KeyPair.Value) {
							bool HasCollider = false;
							Transform TargetTransform = AvatarAnimator.GetBoneTransform(TargetHumanBodyBone);
							if (TargetTransform) {
								VRCPhysBoneColliderBase TargetPhysBoneCollider = TargetTransform.GetComponent<VRCPhysBoneColliderBase>();
								if (TargetPhysBoneCollider) {
									NewColliders.Add(TargetPhysBoneCollider);
									HasCollider = true;
								}
							}
							if (!HasCollider) {
								NewColliders.Add(null);
								Debug.LogWarning($"[VRSuya] Not found {TargetHumanBodyBone.ToString()} collider assigned to {TargetPhysBone.name}");
							}
						}
						switch (KeyPair.Key) {
							case "BackHair":
							case "Hair":
								VRCPhysBoneColliderBase FloorPhysBoneCollider = PhysBoneColliderComponets.Find(Item => Item.name == "Floor");
								if (FloorPhysBoneCollider) {
									NewColliders.Add(FloorPhysBoneCollider);
								} else {
									NewColliders.Add(null);
									Debug.LogWarning($"[VRSuya] Not found Floor collider assigned to {TargetPhysBone.name}");
								}
								break;
							case "Breast":
							case "Breast_L":
							case "Breast_R":
								string TargetSide = TargetPhysBone.rootTransform.name.EndsWith("L") ? "R" : "L";
								VRCPhysBoneColliderBase BreastPhysBoneCollider = PhysBoneColliderComponets.Find(Item => Item.name.Contains("Breast") && Item.name.EndsWith(TargetSide));
								if (BreastPhysBoneCollider) {
									NewColliders.Add(BreastPhysBoneCollider);
								} else {
									NewColliders.Add(null);
									Debug.LogWarning($"[VRSuya] Not found Breast collider assigned to {TargetPhysBone.name}");
								}
								break;
							case "Tail":
								VRCPhysBoneColliderBase TailPhysBoneCollider = PhysBoneColliderComponets.Find(Item => Item.name == "Tail");
								if (TailPhysBoneCollider) {
									NewColliders.Add(TailPhysBoneCollider);
								} else {
									NewColliders.Add(null);
									Debug.LogWarning($"[VRSuya] Not found Tail collider assigned to {TargetPhysBone.name}");
								}
								break;
						}
						if (!OriginalColliders.Equals(NewColliders)) {
							Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
							TargetPhysBone.colliders = NewColliders;
							EditorUtility.SetDirty(TargetPhysBone);
							Undo.CollapseUndoOperations(UndoGroupIndex);
						}
					}
				}
				Debug.Log($"[VRSuya] Assign All PhysBone Colliders");
			}
		}

		[MenuItem("Tools/VRSuya/Utility/PhysBone/Collider/Remove Hand Collider", priority = 1000)]
		static void RemoveHandCollider() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			if (AvatarInstance.GetVRCAvatarDescriptor()) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
				GameObject AvatarObject = AvatarInstance.GetVRCAvatarDescriptor().gameObject;
				Animator AvatarAnimator = AvatarObject.GetComponent<Animator>();
				List<HumanBodyBones> HandBones = new List<HumanBodyBones> {
					HumanBodyBones.LeftHand, HumanBodyBones.RightHand,
					HumanBodyBones.LeftThumbProximal, HumanBodyBones.RightThumbProximal,
					HumanBodyBones.LeftThumbIntermediate, HumanBodyBones.RightThumbIntermediate,
					HumanBodyBones.LeftThumbDistal, HumanBodyBones.RightThumbDistal,
					HumanBodyBones.LeftIndexProximal, HumanBodyBones.RightIndexProximal,
					HumanBodyBones.LeftIndexIntermediate, HumanBodyBones.RightIndexIntermediate,
					HumanBodyBones.LeftIndexDistal, HumanBodyBones.RightIndexDistal,
					HumanBodyBones.LeftMiddleProximal, HumanBodyBones.RightMiddleProximal,
					HumanBodyBones.LeftMiddleIntermediate, HumanBodyBones.RightMiddleIntermediate,
					HumanBodyBones.LeftMiddleDistal, HumanBodyBones.RightMiddleDistal,
					HumanBodyBones.LeftRingProximal, HumanBodyBones.RightRingProximal,
					HumanBodyBones.LeftRingIntermediate, HumanBodyBones.RightRingIntermediate,
					HumanBodyBones.LeftRingDistal, HumanBodyBones.RightRingDistal,
					HumanBodyBones.LeftLittleProximal, HumanBodyBones.RightLittleProximal,
					HumanBodyBones.LeftLittleIntermediate, HumanBodyBones.RightLittleIntermediate,
					HumanBodyBones.LeftLittleDistal, HumanBodyBones.RightLittleDistal
				};
				List<Transform> HandTransforms = HandBones.Select(Item => AvatarAnimator.GetBoneTransform(Item)).ToList();
				List <VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
				List<VRCPhysBoneColliderBase> PhysBoneColliderComponets = GetPhysBoneColliderComponents();
				List<VRCPhysBoneColliderBase> HandColliderCompoents = PhysBoneColliderComponets.Where(Item => HandTransforms.Contains(Item.rootTransform) || HandTransforms.Contains(Item.transform)).ToList();
				foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
					List<VRCPhysBoneColliderBase> OriginalColliders = TargetPhysBone.colliders;
					List<VRCPhysBoneColliderBase> NewColliders = OriginalColliders.Where(Item => !HandColliderCompoents.Contains(Item)).ToList();
					if (!OriginalColliders.Equals(NewColliders)) {
						Undo.RegisterCreatedObjectUndo(TargetPhysBone, UndoGroupName);
						TargetPhysBone.colliders = NewColliders;
						EditorUtility.SetDirty(TargetPhysBone);
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Remove All Hand PhysBone Colliders");
			}
		}
	}
}
#endif