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

		[MenuItem("Tools/VRSuya/PhysBone/Version/1.0")]
		/// <summary>Scene에 존재하는 모든 PhysBone을 1.0으로 변경합니다</summary>
		static void ChangePhysBoneVersionTo1_0() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0) {
					PhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_0;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.0");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Version/1.1")]
		/// <summary>Scene에 존재하는 모든 PhysBone을 1.1으로 변경합니다</summary>
		static void ChangePhysBoneVersionTo1_1() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.version != VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1) {
					PhysBone.version = VRC.Dynamics.VRCPhysBoneBase.Version.Version_1_1;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Version to 1.1");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Version/Debug Version")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 버전을 Unity Console에 출력합니다</summary>
		static void DebugLogPhysBoneComponets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + PhysBone.name + " / Current Version : " + PhysBone.version);
			}
		}

		[MenuItem("Tools/VRSuya/PhysBone/FoldOut/Closed")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 닫습니다</summary>
		static void ClosePhysBoneFoldOut() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				PhysBone.foldout_collision = false;
				PhysBone.foldout_forces = false;
				PhysBone.foldout_gizmos = false;
				PhysBone.foldout_grabpose = false;
				PhysBone.foldout_limits = false;
				PhysBone.foldout_options = false;
				PhysBone.foldout_stretchsquish = false;
				PhysBone.foldout_transforms = false;
				EditorUtility.SetDirty(PhysBone);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Closed");
		}

		[MenuItem("Tools/VRSuya/PhysBone/FoldOut/Opened")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 속성들을 모두 엽니다</summary>
		static void OpenPhysBoneFoldOut() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				PhysBone.foldout_collision = true;
				PhysBone.foldout_forces = true;
				PhysBone.foldout_gizmos = true;
				PhysBone.foldout_grabpose = true;
				PhysBone.foldout_limits = true;
				PhysBone.foldout_options = true;
				PhysBone.foldout_stretchsquish = true;
				PhysBone.foldout_transforms = true;
				EditorUtility.SetDirty(PhysBone);
			}
			Debug.Log("[VRSuya] Changed All PhysBone FoldOut to Opened");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Gizmo/Hide")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 숨깁니다</summary>
		static void HidePhysBoneGizmo() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.showGizmos != false) {
					PhysBone.showGizmos = false;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Hidden");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Gizmo/Show")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 기즈모를 보이게 합니다</summary>
		static void ShowPhysBoneGizmo() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.showGizmos != true) {
					PhysBone.showGizmos = true;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Gizmo to Show");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Immobile/All Motion")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 All Motion으로 변경합니다</summary>
		static void ChangePhysBoneImmobileToAllMotion() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion) {
					PhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.AllMotion;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to All Motion");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Immobile/World")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Immobile 타입을 World으로 변경합니다</summary>
		static void ChangePhysBoneImmobileToWorld() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.immobileType != VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World) {
					PhysBone.immobileType = VRC.Dynamics.VRCPhysBoneBase.ImmobileType.World;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Immobile to World");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Animated/True")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 참으로 변경합니다</summary>
		static void ChangePhysBoneAnimatedToTrue() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.isAnimated != true) {
					PhysBone.isAnimated = true;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to True");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Animated/False")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 거짓으로 변경합니다</summary>
		static void ChangePhysBoneAnimatedToFalse() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.isAnimated != false) {
					PhysBone.isAnimated = false;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Animated to False");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Animated/Debug Animated")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Is Animated 속성을 Unity Console에 출력합니다</summary>
		static void DebugLogPhysBoneAnimateds() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.isAnimated == true) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + PhysBone.name + " / Is Animated : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + PhysBone.name + " / Is Animated : False");
				}
			}
		}

		[MenuItem("Tools/VRSuya/PhysBone/Reset/True")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 참으로 변경합니다</summary>
		static void ChangePhysBoneResetToTrue() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.resetWhenDisabled != true) {
					PhysBone.resetWhenDisabled = true;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to True");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Reset/False")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 거짓으로 변경합니다</summary>
		static void ChangePhysBoneResetToFalse() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.resetWhenDisabled != false) {
					PhysBone.resetWhenDisabled = false;
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Changed All PhysBone Reset to False");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Reset/Debug Reset")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Reset When Disabled 속성을 Unity Console에 출력합니다</summary>
		static void DebugLogPhysBoneResets() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.resetWhenDisabled == true) {
					Debug.LogWarning("[VRSuya] PhysBone Parent GameObject Name : " + PhysBone.name + " / Reset When Disabled : True");
				} else {
					Debug.Log("[VRSuya] PhysBone Parent GameObject Name : " + PhysBone.name + " / Reset When Disabled : False");
				}
			}
		}

		[MenuItem("Tools/VRSuya/PhysBone/Quest/Remove Colliders")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Colliders 어레이를 제거합니다</summary>
		static void EmptyPhysBoneColliders() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (PhysBone.colliders.Count > 0) {
					PhysBone.colliders = new List<VRC.Dynamics.VRCPhysBoneColliderBase> { };
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Empty All PhysBone Colliders List");
		}

		[MenuItem("Tools/VRSuya/PhysBone/Quest/Remove Parameter")]
		/// <summary>Scene에 존재하는 모든 PhysBone의 Parameter를 비웁니다</summary>
		static void EmptyPhysBoneParameter() {
			List<VRCPhysBone> PhysBoneComponents = GetPhysBoneComponents();
			foreach (VRCPhysBone PhysBone in PhysBoneComponents) {
				if (!string.IsNullOrEmpty(PhysBone.parameter)) {
					PhysBone.parameter = "";
					EditorUtility.SetDirty(PhysBone);
				}
			}
			Debug.Log("[VRSuya] Empty All PhysBone Parameter");
		}

		/// <summary>Scene에 존재하는 모든 PhysBone의 리스트를 가져옵니다</summary>
		/// <returns>Scene에 존재하는 모든 PhysBone 컴포넌트 리스트</returns>
		static List<VRCPhysBone> GetPhysBoneComponents() {
			return SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(gameObject => gameObject.GetComponentsInChildren<VRCPhysBone>(true)).ToList();
		}
	}
}
#endif