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

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class SyncChildAvatar : EditorWindow {

		[MenuItem("Tools/VRSuya/Utility/Sync All Child Avatar Bone", priority = 1000)]
		public static void SyncAllChildAvatar() {
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

		/// <summary>Scene에서 활성화 상태인 VRC AvatarDescriptor 컴포넌트를 가지고 있는 아바타 목록을 반환합니다.</summary>
		/// <returns>활성화 상태인 VRC 아바타 목록</returns>
		static List<VRC_AvatarDescriptor> GetVRCAvatarList() {
			List<VRC_AvatarDescriptor> AllVRCAvatars = VRC.Tools.FindSceneObjectsOfTypeAll<VRC_AvatarDescriptor>().ToList();
            List<VRC_AvatarDescriptor> VRCAvatars = AllVRCAvatars.Where(Avatar => Avatar.gameObject.activeInHierarchy).ToList();
			return VRCAvatars;
		}

		/// <summary>Unity의 HumanBodyBones 유형의 모든 본 Enum을 반환합니다.</summary>
		/// <returns>HumanBodyBones의 모든 본 Enum</returns>
		static List<HumanBodyBones> GetHumanBoneList() {
			return Enum.GetValues(typeof(HumanBodyBones)).Cast<HumanBodyBones>().ToList();
		}

		/// <summary>요청한 부모 애니메이터와 자식 애니메이터를 휴머노이드 본 목록에 맞춰, 회전 제약과 엉덩이 본에 한정해서 위치 제약 컴포넌트를 추가하고 연결합니다.</summary>
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

		/// <summary>요청한 유형의 컴포넌트가 존재하는지 확인하고 존재하지 않는다면 생성해서 반환합니다.</summary>
		/// <returns>요청한 유형 컴포넌트</returns>
		static TargetComponent GetOrCreateComponent<TargetComponent>(GameObject TargetGameObject) where TargetComponent : Component {
            TargetComponent Component = TargetGameObject.GetComponent<TargetComponent>();
			if (!Component) Component = TargetGameObject.AddComponent<TargetComponent>();
			return Component;
		}
	}
}
#endif