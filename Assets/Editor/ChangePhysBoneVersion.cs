#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using VRC.SDK3.Dynamics.PhysBone.Components;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class ChangePhysBoneVersion : EditorWindow {

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.0으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Version/1.0")]
		public static void ChangePhysBoneVersionTo1_0() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0) {
					TargetPhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.0");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone을 1.1으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Version/1.1")]
		public static void ChangePhysBoneVersionTo1_1() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1) {
					TargetPhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.1");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 버전을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Version/Debug Version")]
		public static void DebugLogPhysBoneComponets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Current Version : " + TargetPhysBone.version);
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 닫습니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/FoldOut/Closed")]
		public static void ClosePhysBoneFoldOut() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				TargetPhysBone.foldout_collision = false;
				TargetPhysBone.foldout_forces = false;
				TargetPhysBone.foldout_gizmos = false;
				TargetPhysBone.foldout_grabpose = false;
				TargetPhysBone.foldout_limits = false;
				TargetPhysBone.foldout_options = false;
				TargetPhysBone.foldout_stretchsquish = false;
				TargetPhysBone.foldout_transforms = false;
				EditorUtility.SetDirty(TargetPhysBone);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Closed");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 엽니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/FoldOut/Opened")]
		public static void OpenPhysBoneFoldOut() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				TargetPhysBone.foldout_collision = true;
				TargetPhysBone.foldout_forces = true;
				TargetPhysBone.foldout_gizmos = true;
				TargetPhysBone.foldout_grabpose = true;
				TargetPhysBone.foldout_limits = true;
				TargetPhysBone.foldout_options = true;
				TargetPhysBone.foldout_stretchsquish = true;
				TargetPhysBone.foldout_transforms = true;
				EditorUtility.SetDirty(TargetPhysBone);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Opened");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 숨깁니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Gizmo/Hide")]
		public static void HidePhysBoneGizmo() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.showGizmos != false) {
					TargetPhysBone.showGizmos = false;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Hidden");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 보이게 합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Gizmo/Show")]
		public static void ShowPhysBoneGizmo() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.showGizmos != true) {
					TargetPhysBone.showGizmos = true;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Show");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 All Motion으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Immobile/All Motion")]
		public static void ChangePhysBoneImmobileToAllMotion() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion) {
					TargetPhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to All Motion");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 World으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Immobile/World")]
		public static void ChangePhysBoneImmobileToWorld() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World) {
					TargetPhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to World");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Animated/True")]
		public static void ChangePhysBoneAnimatedToTrue() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated != true) {
					TargetPhysBone.isAnimated = true;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to True");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Animated/False")]
		public static void ChangePhysBoneAnimatedToFalse() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated != false) {
					TargetPhysBone.isAnimated = false;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to False");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Animated/Debug Animated")]
		public static void DebugLogPhysBoneAnimateds() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.isAnimated == true) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Is Animated : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Is Animated : False");
				}
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 참으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Reset/True")]
		public static void ChangePhysBoneResetToTrue() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled != true) {
					TargetPhysBone.resetWhenDisabled = true;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to True");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 거짓으로 변경합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Reset/False")]
		public static void ChangePhysBoneResetToFalse() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled != false) {
					TargetPhysBone.resetWhenDisabled = false;
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to False");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 Unity Console에 출력합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Reset/Debug Reset")]
		public static void DebugLogPhysBoneResets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.resetWhenDisabled == true) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Reset When Disabled : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + TargetPhysBone.name + " / Reset When Disabled : False");
				}
			}
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Colliders 어레이를 제거합니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Quest/Remove Colliders")]
		public static void EmptyPhysBoneColliders() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (TargetPhysBone.colliders.Count > 0) {
					TargetPhysBone.colliders = new List<VRC.Dynamics.VRCPhysBoneColliderBase> { };
					EditorUtility.SetDirty(TargetPhysBone);
				}
			}
			Debug.Log("[VRSuya] Empty All PhysBone Colliders List");
			return;
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 Parameter를 비웁니다.</summary>
		[MenuItem("Tools/VRSuya/PhysBone/Quest/Remove Parameter")]
		public static void EmptyPhysBoneParameter() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone TargetPhysBone in PhysBoneComponents) {
				if (!string.IsNullOrEmpty(TargetPhysBone.parameter)) {
					TargetPhysBone.parameter = "";
					EditorUtility.SetDirty(TargetPhysBone);
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
	}
}
#endif