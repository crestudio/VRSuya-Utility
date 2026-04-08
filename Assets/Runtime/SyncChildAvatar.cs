#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

using VRC.SDKBase;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 * Forked from curlune/VRCUtil ( https://github.com/curlune/VRCUtil )
 * Thanks to Dalgona.
 */

namespace VRSuya.Utility {

	[ExecuteInEditMode]
	public class SyncChildAvatar : EditorWindow {

		[MenuItem("Tools/VRSuya/Utility/Sync All Child Avatar Bone", priority = 1000)]
		static void SyncAllChildAvatar() {
            List<HumanBodyBones> HumanBodyBoneList = GetHumanBoneList();
            List<VRC_AvatarDescriptor> VRCAvatars = GetVRCAvatarList();
			foreach (var VRCAvatar in VRCAvatars) {
                Animator ParentAvatarAnimator = VRCAvatar.GetComponent<Animator>();
				if (!ParentAvatarAnimator) continue;
                Animator[] ChildAvatarAnimator = VRCAvatar.GetComponentsInChildren<Animator>(true);
				foreach (var ChildAnimator in ChildAvatarAnimator) {
					CreateConstraintComponents(ParentAvatarAnimator, ChildAnimator, HumanBodyBoneList);
				}
			}
			Debug.Log($"[VRSuya] Synced All Child Avatars");
		}

		static List<VRC_AvatarDescriptor> GetVRCAvatarList() {
			List<VRC_AvatarDescriptor> AllVRCAvatars = VRC.Tools.FindSceneObjectsOfTypeAll<VRC_AvatarDescriptor>().ToList();
            List<VRC_AvatarDescriptor> VRCAvatars = AllVRCAvatars.Where(Avatar => Avatar.gameObject.activeInHierarchy).ToList();
			return VRCAvatars;
		}

		static List<HumanBodyBones> GetHumanBoneList() {
			return Enum.GetValues(typeof(HumanBodyBones)).Cast<HumanBodyBones>().ToList();
		}

		static void CreateConstraintComponents(Animator ParentAnimator, Animator ChildAnimator, List<HumanBodyBones> HumanBodyBoneList) {
			if (ParentAnimator == ChildAnimator) return;
			foreach (HumanBodyBones Bone in HumanBodyBoneList) {
				if (Bone == HumanBodyBones.LastBone) continue;
                Transform ParentBoneTransform = ParentAnimator.GetBoneTransform(Bone);
                Transform ChildBoneTransform = ChildAnimator.GetBoneTransform(Bone);
				if (!ParentBoneTransform || !ChildBoneTransform) continue;

                RotationConstraint TargetRotationConstraint = GetOrCreateComponent<RotationConstraint>(ChildBoneTransform.gameObject);
				TargetRotationConstraint.AddSource(new ConstraintSource() { sourceTransform = ParentBoneTransform, weight = 1.0f });
				TargetRotationConstraint.constraintActive = true;

				if (Bone == HumanBodyBones.Hips) {
                    PositionConstraint TargetPositionConstraint = GetOrCreateComponent<PositionConstraint>(ChildBoneTransform.gameObject);
					TargetPositionConstraint.AddSource(new ConstraintSource() { sourceTransform = ParentBoneTransform, weight = 1.0f });
					TargetPositionConstraint.constraintActive = true;
				}
			}
		}

		static TargetComponent GetOrCreateComponent<TargetComponent>(GameObject TargetGameObject) where TargetComponent : Component {
            TargetComponent Component = TargetGameObject.GetComponent<TargetComponent>();
			if (!Component) Component = TargetGameObject.AddComponent<TargetComponent>();
			return Component;
		}
	}
}
#endif