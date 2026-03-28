#if UNITY_EDITOR
using System.Linq;

using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

using VRSuya.Core;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class AnimatorControllerController : EditorWindow {

		[MenuItem("Assets/VRSuya/Animator/Write Defaults On", true)]
		static bool ValidateControllerOn() {
			return IsAnimatorController(Selection.objects);
		}

		[MenuItem("Assets/VRSuya/Animator/Write Defaults Off", true)]
		static bool ValidateControllerOff() {
			return IsAnimatorController(Selection.objects);
		}

		/// <summary>AnimatorController에 존재하는 모든 State의 Write Defaults을 On으로 변경합니다</summary>
		[MenuItem("Assets/VRSuya/Animator/Write Defaults On", priority = 1000)]
		static void RequestAnimatorWriteDefaultsOn() {
			if (Selection.objects.Length > 0) {
				Asset AssetInstance = new Asset();
				int ModifiedCount = 0;
				try {
					for (int Index = 0; Index < Selection.objects.Length; Index++) {
						if (!Selection.objects[Index]) continue;
						EditorUtility.DisplayProgressBar("Cleaning Broken YAML Formatting",
							$"Processing : {Selection.objects[Index].name}",
							(float)Index / Selection.objects.Length);
						if (Selection.objects[Index] is not AnimatorController) continue;
						AnimatorController TargetAnimator = Selection.objects[Index] as AnimatorController;
						if (TargetAnimator.name.EndsWith("Original")) continue;
						if (ModifyWriteDefaults(TargetAnimator, true)) {
							ModifiedCount++;
						}
					}
				} finally {
					EditorUtility.ClearProgressBar();
					AssetDatabase.Refresh();
				}
				Debug.Log($"[VRSuya] Modified write defaults on in {ModifiedCount} animator controllers");
			}
		}

		/// <summary>AnimatorController에 존재하는 모든 State의 Write Defaults을 Off으로 변경합니다</summary>
		[MenuItem("Assets/VRSuya/Animator/Write Defaults Off", priority = 1000)]
		static void RequestAnimatorWriteDefaultsOff() {
			if (Selection.objects.Length > 0) {
				Asset AssetInstance = new Asset();
				int ModifiedCount = 0;
				try {
					for (int Index = 0; Index < Selection.objects.Length; Index++) {
						if (!Selection.objects[Index]) continue;
						EditorUtility.DisplayProgressBar("Cleaning Broken YAML Formatting",
							$"Processing : {Selection.objects[Index].name}",
							(float)Index / Selection.objects.Length);
						if (Selection.objects[Index] is not AnimatorController) continue;
						AnimatorController TargetAnimator = Selection.objects[Index] as AnimatorController;
						if (TargetAnimator.name.EndsWith("Original")) continue;
						if (ModifyWriteDefaults(TargetAnimator, false)) {
							ModifiedCount++;
						}
					}
				} finally {
					EditorUtility.ClearProgressBar();
					AssetDatabase.Refresh();
				}
				Debug.Log($"[VRSuya] Modified write defaults off in {ModifiedCount} animator controllers");
			}
		}

		public static bool ModifyWriteDefaults(AnimatorController TargetAnimator, bool TargetWriteDefaults) {
			VRSuya.Core.Animator AnimatorInstance = new VRSuya.Core.Animator();
			AnimatorState[] AllAnimatorStates = AnimatorInstance.GetAllAnimatorStates(TargetAnimator);
			bool IsModified = false;
			foreach (AnimatorState TargetState in AllAnimatorStates) {
				if (TargetState.writeDefaultValues != TargetWriteDefaults) {
					TargetState.writeDefaultValues = TargetWriteDefaults;
					IsModified = true;
				}
			}
			return IsModified;
		}

		static bool IsAnimatorController(Object[] TargetObjects) {
			return TargetObjects
				.Select(Item => AssetDatabase.GetAssetPath(Item))
				.Select(Item => Item.EndsWith(".controller"))
				.Contains(true);
		}
	}
}
#endif