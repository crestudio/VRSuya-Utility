#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[AddComponentMenu("VRSuya/VRSuya Blendshape Viewer")]
	public class BlendshapeController : MonoBehaviour {

		public SkinnedMeshRenderer TargetSkinnedMeshRenderer = null;
		public Animator TargetAnimator = null;

		List<string> TargetBlendShapeNames = new List<string>();
		public Dictionary<string, int> BlendShapeList = new Dictionary<string, int>();
		readonly string[] dictHeadNames = new string[] { "Body", "Head", "Face" };

		void Start() {
			if (!TargetSkinnedMeshRenderer) TargetSkinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
			if (!TargetAnimator) TargetAnimator = this.transform.parent.GetComponent<Animator>();
			UpdateBlendshapeList();
		}

		/// <summary>Blendshape 리스트를 업데이트 합니다.</summary>
		public void UpdateBlendshapeList() {
			if (!TargetSkinnedMeshRenderer) TargetSkinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
			if (!TargetAnimator) TargetAnimator = this.transform.parent.GetComponent<Animator>();
			if (TargetSkinnedMeshRenderer && TargetAnimator) TargetBlendShapeNames = GetAnimationBlendshapeName(TargetAnimator);
			BlendShapeList = new Dictionary<string, int>();
			CreateBlendshapeList();
		}

		/// <summary>애니메이션에 존재하는 Blendshape 명으로 리스트를 작성합니다.</summary>
		void CreateBlendshapeList() {
			Mesh TargetMesh = TargetSkinnedMeshRenderer.sharedMesh;
			int BlendShapeCount = TargetMesh.blendShapeCount;
			for (int Index = 0; Index < BlendShapeCount; Index++) {
				if (TargetBlendShapeNames.Exists(AnimationBlendShapeName => TargetMesh.GetBlendShapeName(Index) == AnimationBlendShapeName)) {
					BlendShapeList.Add(TargetMesh.GetBlendShapeName(Index), Index);
				}
			}
		}

		/// <summary>모든 AnimatorController의 Blendshape 이름 리스트를 반환합니다.</summary>
		/// <returns>AnimatorController의 Blendshape 이름 리스트</returns>
		List<string> GetAnimationBlendshapeName(Animator TargetAnimator) {
			List<string> newBlendshapeName = new List<string>();
			if (TargetAnimator) {
				AnimationClip[] AllAnimationClips = GetAnimationClips((AnimatorController)TargetAnimator.runtimeAnimatorController);
				foreach (AnimationClip TargetAnimationClip in AllAnimationClips) {
					foreach (EditorCurveBinding TargetBinding in AnimationUtility.GetCurveBindings(TargetAnimationClip)) {
						if (Array.Exists(dictHeadNames, HeadMeshName => TargetBinding.path == HeadMeshName)) {
							if (TargetBinding.type == typeof(SkinnedMeshRenderer)) {
								string BlendshapeName = TargetBinding.propertyName.Remove(0, 11);
								if (!newBlendshapeName.Contains(BlendshapeName)) {
									newBlendshapeName.Add(BlendshapeName);
								}
							}
						}
					}
				}
			}
			newBlendshapeName = newBlendshapeName.Distinct().ToList();
			newBlendshapeName.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));
			return newBlendshapeName;
		}

		/// <summary>모든 AnimatorController의 AnimationClip 어레이를 반환합니다.</summary>
		/// <returns>AnimatorController의 AnimationClip 어레이</returns>
		AnimationClip[] GetAnimationClips(AnimatorController TargetAnimatorController) {
			List<AnimatorStateMachine> RootStateMachines = TargetAnimatorController.layers.Select(AnimationLayer => AnimationLayer.stateMachine).ToList();
			List<AnimatorStateMachine> AllStateMachines = new List<AnimatorStateMachine>();
			List<AnimatorState> AllAnimatorState = new List<AnimatorState>();
			List<AnimationClip> AllAnimationClips = new List<AnimationClip>();
			foreach (AnimatorStateMachine SubStateMachine in RootStateMachines) {
				AllStateMachines.AddRange(GetAllStateMachines(SubStateMachine));
			}
			foreach (AnimatorStateMachine SubStateMachine in AllStateMachines) {
				AllAnimatorState.AddRange(GetAllStates(SubStateMachine));
			}
			if (AllAnimatorState.Count > 0) {
				List<Motion> AllMotion = AllAnimatorState.Select(State => State.motion).ToList();
				foreach (Motion SubMotion in AllMotion) {
					AllAnimationClips.AddRange(GetAnimationClips(SubMotion));
				}
			}
			AllAnimationClips = AllAnimationClips.Distinct().ToList();
			AllAnimationClips.Sort((a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
			return AllAnimationClips.ToArray();
		}

		/// <summary>모든 State 어레이를 반환합니다.</summary>
		/// <returns>State 어레이</returns>
		AnimatorState[] GetAllStates(AnimatorStateMachine TargetStateMachine) {
			AnimatorState[] States = TargetStateMachine.states.Select(ExistChildState => ExistChildState.state).ToArray();
			if (TargetStateMachine.stateMachines.Length > 0) {
				foreach (var TargetChildStatetMachine in TargetStateMachine.stateMachines) {
					States = States.Concat(GetAllStates(TargetChildStatetMachine.stateMachine)).ToArray();
				}
			}
			return States;
		}

		/// <summary>모든 StateMachine 어레이를 반환합니다.</summary>
		/// <returns>StateMachine 어레이</returns>
		AnimatorStateMachine[] GetAllStateMachines(AnimatorStateMachine TargetStateMachine) {
			AnimatorStateMachine[] StateMachines = new AnimatorStateMachine[] { TargetStateMachine };
			if (TargetStateMachine.stateMachines.Length > 0) {
				foreach (var TargetChildStateMachine in TargetStateMachine.stateMachines) {
					StateMachines = StateMachines.Concat(GetAllStateMachines(TargetChildStateMachine.stateMachine)).ToArray();
				}
			}
			return StateMachines;
		}

		/// <summary>모든 AnimationClip 어레이를 반환합니다.</summary>
		/// <returns>AnimationClip 어레이</returns>
		AnimationClip[] GetAnimationClips(Motion TargetMotion) {
			AnimationClip[] MotionAnimationClips = new AnimationClip[0];
			if (TargetMotion is AnimationClip) {
				MotionAnimationClips = MotionAnimationClips.Concat(new AnimationClip[] { (AnimationClip)TargetMotion }).ToArray();
			} else if (TargetMotion is BlendTree ChildBlendTree) {
				foreach (ChildMotion ChildMotion in ChildBlendTree.children) {
					MotionAnimationClips = MotionAnimationClips.Concat(GetAnimationClips(ChildMotion.motion)).ToArray();
				}
			}
			return MotionAnimationClips;
		}
	}
}
#endif